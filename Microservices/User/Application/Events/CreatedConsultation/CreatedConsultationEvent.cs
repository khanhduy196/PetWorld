using Rabbit.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Application.Events.CreatedConsultation
{
    public class CreatedConsultationEvent : BaseEvent
    {
        public string Description { get; set; }
    }
}
