using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rabbit.Bus;
using Rabbit.Commands;
using Rabbit.Events;
using Rabbit.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Infrastructure
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly List<Type> _eventTypes;
        private readonly RabbitMQConfiguration _rabbitMQConfiguration;
        public RabbitMQBus(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMQConfiguration> options)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _rabbitMQConfiguration = options.Value;
        }


        public void Publish<T>(T @event) where T : BaseEvent
        {
            // stop if routing is empty
            if (string.IsNullOrEmpty(@event.RoutingKey)) return;

            var factory = new ConnectionFactory() { HostName = _rabbitMQConfiguration.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // message
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                // get event name, remove "Event" from object Name
                var eventName = typeof(T).Name.Substring(0, typeof(T).Name.Length - 5);
                // headers, containing event name
                IBasicProperties basicProperties = channel.CreateBasicProperties();
                var headers = new Dictionary<string, object>();            
                headers.Add("EventName", eventName);
                basicProperties.Headers = headers;

                // init exchange if it does not exist
                if (!string.IsNullOrEmpty(@event.ExchangeName)){
                    channel.ExchangeDeclare(@event.ExchangeName, @event.ExchangeType, true);
                }

                // publish event
                if (string.IsNullOrEmpty(@event.ExchangeName)) // default exchange name
                {
                    channel.BasicPublish("", @event.RoutingKey, basicProperties, body);
                }
                else
                {
                    channel.BasicPublish(@event.ExchangeName, @event.RoutingKey, basicProperties, body);
                }
             
            }
        }

        //public Task SendCommand<T>(T command) where T : BaseCommand
        //{
        //    return this._mediator.Send(command);
        //}

        public void Subscribe<T, TH>()
            where T : BaseMessage
            where TH : IEventHandler<T>
        {
            // get event name, remove "Message" from object Name
            var eventName = typeof(T).Name.Substring(0, typeof(T).Name.Length - 7);
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
            }
            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : BaseMessage
        {
            var messageObject = Activator.CreateInstance(typeof(T));
            var queueName = (string)typeof(T).GetProperty("QueueName").GetValue(messageObject);
            var exchangeName = (string)typeof(T).GetProperty("ExchangeName").GetValue(messageObject);
            var routingKey = (string)typeof(T).GetProperty("RoutingKey").GetValue(messageObject);

            // stop if queueName is empty
            if (string.IsNullOrEmpty(queueName)) return;

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQConfiguration.HostName,
                DispatchConsumersAsync = true
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // init queue if it does not exist
                channel.QueueDeclare(queueName, true, false, false, null);

                // bind queue with exchange
                if (!string.IsNullOrEmpty(exchangeName))
                {
                    channel.QueueBind(queueName, exchangeName, routingKey);
                }
               
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;

                channel.BasicConsume(queueName, true, consumer);
            }

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = Encoding.Default.GetString(e.BasicProperties.Headers["EventName"] as byte[]);
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;
                        // create type for message of event
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == (eventName + "Message"));
                        // create object for message of event
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        // create type of event handler
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        await(Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
    }
}
