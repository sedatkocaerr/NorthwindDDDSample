using NorthwindApi.Domain.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NorthwindApi.Domain.Core.Repository
{
    public interface IRepository<TEntity>
      where TEntity : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        
    }
}
