using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class CategoryRepository : ICategoryRepository,
        IDisposable
    {
        private readonly EfDataContext _efDataContext;

        private readonly DbSet<Category> DbSet;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }

        public CategoryRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Categories;
        }

        public async Task<Category> FindById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> FindOne(Expression<Func<Category, bool>> SpecExpression)
        {
            return await DbSet.FindAsync(SpecExpression);
        }

        public async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> SpecExpression)
        {
            return await DbSet.Where(SpecExpression).ToListAsync();
        }

        public async Task Add(Category entity)
        {
           await DbSet.AddAsync(entity);
        }

        public void Remove(Category entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(Category entity)
        {
            DbSet.Update(entity);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {

          return await  DbSet.ToListAsync();
        }

        public void Dispose()
        {
            _efDataContext.Dispose();
        }
    }
}
