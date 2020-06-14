using Consultation.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consultation.Microservice.Infrastructure
{
    public class ConsultationDbContext : DbContext
    {
        public ConsultationDbContext(DbContextOptions<ConsultationDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Pet
            builder.Entity<Booking>().Property(p => p.Id).HasDefaultValueSql("newId()");
            builder.Entity<Booking>().Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            #endregion
        }
    }
}
