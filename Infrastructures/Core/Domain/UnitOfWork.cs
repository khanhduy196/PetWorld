using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public interface IUnitOfWork
    {
        Task SaveChangeAsync();
    }
    class UnitOfWork<Context> : IUnitOfWork where Context : DbContext
    {
        public async Task SaveChangeAsync()
        {
            throw new Exception();
        }
    }
}
