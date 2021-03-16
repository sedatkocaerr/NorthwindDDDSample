using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit(IDbContextTransaction transaction);
        Task<bool> Commit();
        bool HasActiveTransaction();
        IDbContextTransaction GetCurrentTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void Rollback();
    }
}
