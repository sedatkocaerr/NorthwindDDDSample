using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> FindById(Guid id);
        Task<Order> FindOne(Expression<Func<Order, bool>> SpecExpression);
        Task<IEnumerable<Order>> Find(Expression<Func<Order, bool>> SpecExpression);
        Task Add(Order entity);
        void Remove(Order entity);
        void Update(Order entity);
    }
}
