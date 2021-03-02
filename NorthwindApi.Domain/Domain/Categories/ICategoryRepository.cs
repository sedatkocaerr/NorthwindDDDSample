using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Categories
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task<Category> FindById(Guid id);
        Task<Category> FindOne(Expression<Func<Category, bool>> SpecExpression);
        Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> SpecExpression);
        Task<IEnumerable<Category>> GetAll();
        Task Add(Category entity);
        void Remove(Category entity);
        void Update(Category entity);
    }
}
