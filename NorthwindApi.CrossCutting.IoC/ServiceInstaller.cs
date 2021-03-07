using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NorthwindApi.Application.AppServices;
using NorthwindApi.Application.Authentication.Abstract;
using NorthwindApi.Application.Authentication.Concrete;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Data.Ef;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Data.Repository;
using NorthwindApi.Data.Repository.EventSource;
using NorthwindApi.Domain;
using NorthwindApi.Domain.Commands.AccountCommands;
using NorthwindApi.Domain.Commands.CategoriesCommands;
using NorthwindApi.Domain.Commands.CustomersCommands;
using NorthwindApi.Domain.Commands.EmployeesCommands;
using NorthwindApi.Domain.Commands.Orders;
using NorthwindApi.Domain.Commands.ProductsCommands;
using NorthwindApi.Domain.Commands.ShipperCommands;
using NorthwindApi.Domain.Commands.SuppliersCommands;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Accounts;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Domain.OrderDetails;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Domain.Suppliers;

namespace NorthwindApi.CrossCutting.IoC
{
    public static class ServiceInstaller
    {
        public static void RegisterService(IServiceCollection services)
        {

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Customer Commands, Application service,and repository
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IRequestHandler<CustomerAddCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<CustomerUpdateCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<CustomerRemoveCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();

            // Employee Commands, Application service,and repository
            services.AddScoped<IEmployeeAppService, EmployeeAppService>();
            services.AddScoped<IRequestHandler<EmployeeAddCommand, ValidationResult>, EmployeesCommandHandler>();
            services.AddScoped<IRequestHandler<EmployeeUpdateCommand, ValidationResult>, EmployeesCommandHandler>();
            services.AddScoped<IRequestHandler<EmployeeRemoveCommand, ValidationResult>, EmployeesCommandHandler>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();


            // Save order app service , order repository , order commands
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IRequestHandler<OrderAddCommand, ValidationResult>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderUpdateCommand, ValidationResult>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderRemoveCommand, ValidationResult>, OrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Save Product app service , Product repository , Product commands
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IRequestHandler<ProductAddCommand, ValidationResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<ProductUpdateCommand, ValidationResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<ProductRemoveCommand, ValidationResult>, ProductCommandHandler>();
            services.AddScoped<IProductRepository, ProductRepository>();


            // Save Shipper
            services.AddScoped<IShipperAppService, ShipperAppService>();
            services.AddScoped<IRequestHandler<ShipperAddCommand, ValidationResult>, ShipperCommandHandler>();
            services.AddScoped<IRequestHandler<ShipperUpdateCommand, ValidationResult>, ShipperCommandHandler>();
            services.AddScoped<IRequestHandler<ShipperRemoveCommand, ValidationResult>, ShipperCommandHandler>();
            services.AddScoped<IShippersRepository, ShippersRepository>();


            // Save Category
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IRequestHandler<CategoryAddCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryUpdateCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryRemoveCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            // Save Supplier
            services.AddScoped<ISupplierAppService, SupplierAppService>();
            services.AddScoped<IRequestHandler<SupplierAddCommand, ValidationResult>, SupplierCommandHandler>();
            services.AddScoped<IRequestHandler<SupplierUpdateCommand, ValidationResult>, SupplierCommandHandler>();
            services.AddScoped<IRequestHandler<SupplierRemoveCommand, ValidationResult>, SupplierCommandHandler>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();


            // Save Category
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IRequestHandler<CategoryAddCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryUpdateCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryRemoveCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            // Save Account
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IRequestHandler<AccountRegisterCommand, ValidationResult>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<AccountUpdateCommand, ValidationResult>, AccountCommandHandler>();
            services.AddScoped<IAccountRepository, AccountRepository>();


            //  Token Service
            services.AddScoped<IUserTokenAppService, UserTokenService>();

            // Save Database
            services.AddScoped<EfDataContext>();


            // Media Store (Event Store)
            services.AddScoped<IEventSourceRepository,EventSourceRepository>();
        }
    }
}
