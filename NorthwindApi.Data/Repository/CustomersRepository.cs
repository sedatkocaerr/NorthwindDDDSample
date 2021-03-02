using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly EfDataContext _efDataContext;
        protected readonly DbSet<Customer> DbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
               return _efDataContext;
            }
        }

        public CustomersRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Customer;
        }
        public void Add(Customer entity)
        {
            DbSet.Add(entity);
        }

        public async Task<IEnumerable<Customer>> FindList(Expression<Func<Customer, bool>> SpecExpression)
        {
            return  await DbSet.Where(SpecExpression).ToListAsync();
        }

        public async Task<Customer> FindById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<Customer> FindOne(Expression<Func<Customer, bool>> SpecExpression)
        {
            return await DbSet.FindAsync(SpecExpression);
        }

        public void Remove(Customer entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Update(Customer customer)
        {
            DbSet.Update(customer);
        }
    }
}
