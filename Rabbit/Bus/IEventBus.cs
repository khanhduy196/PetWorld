using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rabbit.Commands;
using Rabbit.Events;

namespace Rabbit.Bus
{
    public interface IEventBus
    {
        //Task SendCommand<T>(T command) where T : BaseCommand;
        void Publish<T>(T @event) where T : Event;
        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
