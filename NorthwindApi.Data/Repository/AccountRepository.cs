using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository
{
    public class AccountRepository : IAccountRepository,
        IDisposable
    {

        private readonly EfDataContext _efDataContext;

        private readonly DbSet<Account> DbSet;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _efDataContext;
            }
        }

        public AccountRepository(EfDataContext context)
        {
            _efDataContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _efDataContext.Account;
        }

        public async Task Add(Account entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task<bool> EmailExists(string EmailAdress)
        {
            if (await DbSet.AsNoTracking().Where(x => x.Email == EmailAdress).FirstOrDefaultAsync()!=null)
                return true;
            return false;
        }

        public async Task<IEnumerable<Account>> Find(Expression<Func<Account, bool>> SpecExpression)
        {
           return await DbSet.AsNoTracking().Where(SpecExpression).ToListAsync();
        }

        public async Task<Account> FindById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x=>x.Id== id);
        }

        public async Task<Account> FindOne(Expression<Func<Account, bool>> SpecExpression)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(SpecExpression);
        }

        public void Remove(Account entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(Account entity)
        {
            DbSet.Update(entity);
        }

        public void Dispose()
        {
            _efDataContext.Dispose();
        }
    }
}
