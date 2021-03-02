using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Suppliers
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> FindById(Guid id);
        Task<Supplier> FindOne(Expression<Func<Supplier, bool>> SpecExpression);
        Task<IEnumerable<Supplier>> Find(Expression<Func<Supplier, bool>> SpecExpression);
        Task Add(Supplier entity);
        void Remove(Supplier entity);
        void Update(Supplier entity);
    }
}
