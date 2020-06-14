using Rabbit.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Microservice.Application.Messages.CreatedConsultation
{
    public class CreatedConsultationMessage : BaseMessage
    {
        public string Description { get; set; }
        public CreatedConsultationMessage()
        {
            this.QueueName = "Consultation.CreatedConsultation";
        }
    }
}
