using Microsoft.EntityFrameworkCore;
using User.Microservice.Domain.Entities;

namespace User.Microservice.Domain
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Pet
            builder.Entity<Pet>().Property(p => p.Id).HasDefaultValueSql("newId()");
            builder.Entity<Pet>().Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            #endregion
        }
    }
}
