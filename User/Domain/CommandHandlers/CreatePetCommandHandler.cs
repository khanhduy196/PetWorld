using Core.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rabbit.Bus;
using System.Threading;
using System.Threading.Tasks;
using User.Microservice.Domain.Commands;
using User.Microservice.Domain.Entities;
using User.Microservice.Domain.Events;

namespace User.Microservice.Domain.CommandHandlers
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, bool>
    {
        private readonly IEventBus _bus;
        private readonly IGenericRepository<Pet> _petRepository;
        private readonly DbContext _dbContext;
        public CreatePetCommandHandler(IEventBus bus, IGenericRepository<Pet> petRepository, DbContext dbContext)
        {
            this._bus = bus;
            this._petRepository = petRepository;
            this._dbContext = dbContext;
        }
        public async Task<bool> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = new Pet
            {
                Name = request.Name
            };
            await this._petRepository.CreateAsync(pet);
            //publish event to RabbitMQ
            _bus.Publish(new CreateConsultationEvent { 
                description = "demo"
            });

            this._dbContext.SaveChanges();

            return await Task.FromResult(true);
        }
    }
}
