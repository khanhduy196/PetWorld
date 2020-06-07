using Microsoft.EntityFrameworkCore;
using Consultation.Microservice.Domain;

namespace Consultation.Microservice.Domain
{
    public class ConsultationDbContext : DbContext
    {
        public ConsultationDbContext(DbContextOptions<ConsultationDbContext> options) : base(options) { }

        public DbSet<Consultation.Microservice.Domain.Consultation> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Consultation
            builder.Entity<Consultation>().Property(p => p.Id).HasDefaultValueSql("newId()");
            builder.Entity<Consultation>().Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            #endregion
        }
    }
}
