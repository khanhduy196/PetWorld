using Consultation.Microservice.Domain.Entities;
using Consultation.Microservice.Domain.Events;
using Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Rabbit.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Microservice.Domain.EventHandlers
{
    public class CreateConsultationEventHandler : IEventHandler<CreateConsultationEvent>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly DbContext _dbContext;
        public CreateConsultationEventHandler(IGenericRepository<Booking> bookingRepository, DbContext dbContext)
        {
            this._bookingRepository = bookingRepository;
            this._dbContext = dbContext;
        }

        public async Task Handle(CreateConsultationEvent @event)
        {
            var booking = new Booking
            {
                description = @event.description
            };

            await this._bookingRepository.CreateAsync(booking);

            await this._dbContext.SaveChangesAsync();
        }
    }
}
