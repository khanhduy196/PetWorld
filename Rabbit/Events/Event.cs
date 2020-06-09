using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Events
{
    public abstract class Event
    {
        public DateTime CreatedDate { get; protected set; }
        public string RoutingKey { get; set; }
        protected Event()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
