using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Shippers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class ShippersRepository : IShippersRepository
    {
        private readonly EfDataContext _efDataContext;
        private DbSet<Shipper> DbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }
        public ShippersRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Shippers;
        }

        public async Task<Shipper> FindById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstAsync(x=>x.Id == id);
        }

        public async Task<Shipper> FindOne(Expression<Func<Shipper, bool>> SpecExpression)
        {
            return await DbSet.FindAsync(SpecExpression);
        }

        public async Task<IEnumerable<Shipper>> Find(Expression<Func<Shipper, bool>> SpecExpression)
        {
            return await DbSet.Where(SpecExpression).ToListAsync();
        }

        public async Task Add(Shipper entity)
        {
           await DbSet.AddAsync(entity);
        }

        public void Remove(Shipper entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(Shipper entity)
        {
            DbSet.Update(entity);
        }

        public async Task<IEnumerable<Shipper>> GetAll()
        {
           return await DbSet.ToListAsync();
        }
    }
}
