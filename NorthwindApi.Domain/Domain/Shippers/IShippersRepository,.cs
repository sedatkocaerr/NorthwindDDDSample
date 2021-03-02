using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Domain.Shippers
{
    public interface IShippersRepository : IRepository<Shipper>
    {
        Task<Shipper> FindById(Guid id);
        Task<Shipper> FindOne(Expression<Func<Shipper, bool>> SpecExpression);
        Task<IEnumerable<Shipper>> Find(Expression<Func<Shipper, bool>> SpecExpression);

        Task<IEnumerable<Shipper>> GetAll();

        Task Add(Shipper entity);
        void Remove(Shipper entity);
        void Update(Shipper entity);
    }
}
