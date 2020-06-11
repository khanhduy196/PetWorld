using Rabbit.Events;
using Rabbit.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Bus
{
    public interface IEventHandler<T> : IEventHandler where T : BaseMessage
    {
        Task Handle(T @message);
    }

    public interface IEventHandler
    {

    }
}
