using MediatR;
using Rabbit.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Microservice.Application.Events.CreatedConsultation;

namespace User.Microservice.Application.Commands.CreatePet
{
    public class CreatedPetCommandHandler : IRequestHandler<CreatedPetCommand, bool>
    {
        private readonly IEventBus _bus;

        public CreatedPetCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreatedPetCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new CreatedConsultationEvent { 
                RoutingKey = "Consultation.CreatedConsultation",
                Description = "demo"
            });

            return Task.FromResult(true);
        }
    }

}
