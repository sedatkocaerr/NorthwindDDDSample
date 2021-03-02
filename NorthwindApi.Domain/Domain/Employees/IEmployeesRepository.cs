using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Employees
{
    public interface IEmployeesRepository : IRepository<Employee>
    {
        Task<Employee> FindById(Guid id);
        Task<bool> ChechkEmployee(Guid id);
        Task<Employee> FindOne(Expression<Func<Employee, bool>> SpecExpression);
        Task<IEnumerable<Employee>> FindList(Expression<Func<Employee, bool>> SpecExpression);
        Task  Add(Employee employee);
        void  Remove(Employee employee);

        void Update(Employee employee);
    }
}
