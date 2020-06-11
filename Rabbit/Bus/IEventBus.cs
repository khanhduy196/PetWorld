using Rabbit.Events;
using Rabbit.Messages;

namespace Rabbit.Bus
{
    public interface IEventBus
    {
        //Task SendCommand<T>(T command) where T : BaseCommand;
        void Publish<T>(T @event) where T : BaseEvent;
        void Subscribe<T, TH>()
            where T : BaseMessage
            where TH : IEventHandler<T>;
    }
}
