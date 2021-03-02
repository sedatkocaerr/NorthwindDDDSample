using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfDataContext _efDataContext;
        private DbSet<Order> DbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }
        public OrderRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Orders;
        }

        public async Task<Order> FindById(Guid id)
        {
           return await DbSet.AsNoTracking().FirstAsync(x=>x.Id==id);
        }

        public async Task<Order> FindOne(Expression<Func<Order, bool>> SpecExpression)
        {
            return await  DbSet.FindAsync(SpecExpression);

        }

        public async Task<IEnumerable<Order>> Find(Expression<Func<Order, bool>> SpecExpression)
        {
            var data = await DbSet.Where(SpecExpression).ToListAsync();
            return data;
        }

        public async Task Add(Order entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Remove(Order entity)
        {
             DbSet.Remove(entity);
        }

        public void Update(Order entity)
        {
            DbSet.Update(entity);
        }
    }
}
