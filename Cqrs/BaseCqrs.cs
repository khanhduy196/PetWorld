using MediatR;
using Rabbit.Commands;
using Rabbit.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs
{
    public class BaseCqrs : IBaseCqrs
    {
        private readonly IMediator _mediator;
        public BaseCqrs(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommand(BaseCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task<TResult> SendQuery<TResult>(BaseQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(query, cancellationToken);
        }
    }
}
