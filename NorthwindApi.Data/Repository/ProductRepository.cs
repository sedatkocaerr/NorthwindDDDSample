using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EfDataContext _efDataContext;

        private  DbSet<Product> DbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }
        public ProductRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Products;
        }

        public async Task<Product> FindById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstAsync(x=>x.Id == id);
        }

        public async Task<Product> FindOne(Expression<Func<Product, bool>> SpecExpression)
        {
           return await DbSet.FindAsync(SpecExpression);
        }

        public async Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> SpecExpression)
        {
            return await DbSet.Where(SpecExpression).ToListAsync();
        }

        public async Task Add(Product entity)
        {
           await DbSet.AddAsync(entity);
        }

        public void Remove(Product entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(Product entity)
        {
            DbSet.Update(entity);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {

            return await DbSet.ToListAsync();
        }
    }
}
