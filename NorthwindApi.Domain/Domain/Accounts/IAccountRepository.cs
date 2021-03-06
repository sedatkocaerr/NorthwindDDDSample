using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Accounts
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> FindById(Guid id);
        Task<Account> FindOne(Expression<Func<Account, bool>> SpecExpression);
        Task<IEnumerable<Account>> Find(Expression<Func<Account, bool>> SpecExpression);
        Task<bool> EmailExists(string EmailAdress);
        Task Add(Account entity);
        void Remove(Account entity);
        void Update(Account entity);
    }
}
