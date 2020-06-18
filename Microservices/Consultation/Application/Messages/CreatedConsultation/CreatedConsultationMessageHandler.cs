using Consultation.Domain.Entities;
using Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Rabbit.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Application.Messages.CreatedConsultation
{
    public class CreatedConsultationMessageHandler : IEventHandler<CreatedConsultationMessage>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly DbContext _dbContext;

        public CreatedConsultationMessageHandler(DbContext dbContext, IGenericRepository<Booking> bookingRepository)
        {
            this._dbContext = dbContext;
            this._bookingRepository = bookingRepository;
        }
        public async Task Handle(CreatedConsultationMessage message)
        {         
            var booking = new Booking
            {
                description = message.Description
            };
            await this._bookingRepository.CreateAsync(booking);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
