using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> FindById(Guid id);
        Task<Product> FindOne(Expression<Func<Product, bool>> SpecExpression);
        Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> SpecExpression);
        Task Add(Product entity);
        void Remove(Product entity);

        void Update(Product entity);
    }
}
