using Rabbit.Commands;
using Rabbit.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs
{
    public interface IBaseCqrs
    {
        Task SendCommand(BaseCommand command, CancellationToken cancellationToken = default);
        Task<TResult> SendQuery<TResult>(BaseQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}
