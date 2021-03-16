using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Employees;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace NorthwindApi.Data.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EfDataContext _efDataContext;
        protected readonly DbSet<Employee> DbSet;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }
        public EmployeesRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Employees;
        }

        public async Task<Employee> FindById(Guid id)
        {
           var employee = await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return employee;
        }

        public async Task<Employee> FindOne(Expression<Func<Employee, bool>> SpecExpression)
        {
            var employee = await DbSet.FindAsync(SpecExpression);
            return employee;
        }

        public async Task<IEnumerable<Employee>> FindList(Expression<Func<Employee, bool>> SpecExpression)
        {
            var employee = await DbSet.Where(SpecExpression).ToListAsync();
            return employee;
        }

        public async Task Add(Employee entity)
        {
           await DbSet.AddAsync(entity);
        }

        public void Remove(Employee entity)
        {
             DbSet.Remove(entity);
        }

        public async Task<bool> ChechkEmployee(Guid id)
        {
           if(await DbSet.AnyAsync(x=>x.Id==id))
            {
                return true;
            }
            return false;
        }

        public void Update(Employee employee)
        {
            DbSet.Update(employee);
        }
    }
}
