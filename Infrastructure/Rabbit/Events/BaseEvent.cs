using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Events
{
    public abstract class BaseEvent
    {
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }

        public DateTime CreatedDate { get; protected set; }
        protected BaseEvent()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
