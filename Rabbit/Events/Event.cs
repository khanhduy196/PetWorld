using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Events
{
    public abstract class Event
    {
        public DateTime CreatedDate { get; protected set; }

        protected Event()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
