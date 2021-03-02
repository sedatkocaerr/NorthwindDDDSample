using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
        void Rollback();
    }
}
