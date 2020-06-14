using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Queries
{
    public abstract class BaseQuery<T> : IRequest<T>
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        
        public BaseQuery()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }
    }
}
