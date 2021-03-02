
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain
{
    public interface IEventSourceRepository:IDisposable
    {
        Task SaveAsync<T>(Entity aggregate) where T : Entity;

        Task<T> LoadAsync<T>(Guid aggregateId) where T : Entity, new();
    }
}
