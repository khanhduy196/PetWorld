using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Messages
{
    public abstract class BaseMessage
    {
        public string QueueName { get; protected set; }
        public string ExchangeName { get; protected set; }
        public string RoutingKey { get; protected set; }

        public DateTime CreatedDate { get;}
        protected BaseMessage()
        {

        }
    }
}
