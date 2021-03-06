using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NorthwindApi.Data.MappingsEntity;
using NorthwindApi.Domain.Core.Repository;
using NorthwindApi.Domain.Domain.Accounts;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Domain.OrderDetails;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Ef
{
    public class EfDataContext : DbContext, IUnitOfWork
    {

       
        public EfDataContext(DbContextOptions<EfDataContext> options) : base(options)
        {

        }

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new OrderDetailsMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ShipperMap());
            modelBuilder.ApplyConfiguration(new SupplierMap());
            modelBuilder.ApplyConfiguration(new AccountMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Account> Account { get; set; }

        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;
            if(success)
            {
                return true;
            }

            return false;
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
