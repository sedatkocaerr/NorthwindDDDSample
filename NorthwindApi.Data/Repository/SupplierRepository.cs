using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly EfDataContext _efDataContext;
        private DbSet<Supplier> DbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }
        public SupplierRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Suppliers;
        }

        public async Task<Supplier> FindById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Supplier> FindOne(Expression<Func<Supplier, bool>> SpecExpression)
        {
            return await DbSet.FindAsync(SpecExpression);
        }

        public async Task<IEnumerable<Supplier>> Find(Expression<Func<Supplier, bool>> SpecExpression)
        {
            return await DbSet.Where(SpecExpression).ToListAsync();
        }

        public async Task Add(Supplier entity)
        {
            await DbSet.AddAsync(entity);
        }

        public  void Remove(Supplier entity)
        {
            DbSet.Remove(entity);
        }

        public  void Update(Supplier entity)
        {
            DbSet.Update(entity);
        }
    }
}
