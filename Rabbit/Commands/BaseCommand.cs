using MediatR;
using System;
namespace Rabbit.Commands
{
    public abstract class BaseCommand : IRequest<bool>
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        protected BaseCommand()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }
    }
}
