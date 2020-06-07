using Rabbit.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Microservice.Domain.Events
{
    public class CreateConsultationEvent : Event
    {
        public string description { get; set; }
    }
}
