using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Customers
{
    public interface ICustomersRepository : IRepository<Customer>
    {
        Task<Customer> FindById(Guid id);
        Task<Customer> FindOne(Expression<Func<Customer, bool>> SpecExpression);
        Task<Customer> GetByEmail(string email);
        Task<IEnumerable<Customer>> GetAll();
        Task<IEnumerable<Customer>> FindList(Expression<Func<Customer, bool>> SpecExpression);

        void Update(Customer customer);
        void Add(Customer entity);
        void Remove(Customer entity);
    }
}
