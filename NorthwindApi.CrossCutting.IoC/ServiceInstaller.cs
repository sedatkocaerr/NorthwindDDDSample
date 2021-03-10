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
using NorthwindApi.Domain.Core.Command;
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
            services.AddScoped<IRequestHandler<CustomerAddCommand, CommandResponse>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<CustomerUpdateCommand, CommandResponse>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<CustomerRemoveCommand, CommandResponse>, CustomerCommandHandler>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();

            // Employee Commands, Application service,and repository
            services.AddScoped<IEmployeeAppService, EmployeeAppService>();
            services.AddScoped<IRequestHandler<EmployeeAddCommand, CommandResponse>, EmployeesCommandHandler>();
            services.AddScoped<IRequestHandler<EmployeeUpdateCommand, CommandResponse>, EmployeesCommandHandler>();
            services.AddScoped<IRequestHandler<EmployeeRemoveCommand, CommandResponse>, EmployeesCommandHandler>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();


            // Save order app service , order repository , order commands
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IRequestHandler<OrderAddCommand, CommandResponse>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderUpdateCommand, CommandResponse>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderRemoveCommand, CommandResponse>, OrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Save Product app service , Product repository , Product commands
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IRequestHandler<ProductAddCommand, CommandResponse>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<ProductUpdateCommand, CommandResponse>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<ProductRemoveCommand, CommandResponse>, ProductCommandHandler>();
            services.AddScoped<IProductRepository, ProductRepository>();


            // Save Shipper
            services.AddScoped<IShipperAppService, ShipperAppService>();
            services.AddScoped<IRequestHandler<ShipperAddCommand, CommandResponse>, ShipperCommandHandler>();
            services.AddScoped<IRequestHandler<ShipperUpdateCommand, CommandResponse>, ShipperCommandHandler>();
            services.AddScoped<IRequestHandler<ShipperRemoveCommand, CommandResponse>, ShipperCommandHandler>();
            services.AddScoped<IShippersRepository, ShippersRepository>();


            // Save Category
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IRequestHandler<CategoryAddCommand, CommandResponse>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryUpdateCommand, CommandResponse>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryRemoveCommand, CommandResponse>, CategoryCommandHandler>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            // Save Supplier
            services.AddScoped<ISupplierAppService, SupplierAppService>();
            services.AddScoped<IRequestHandler<SupplierAddCommand, CommandResponse>, SupplierCommandHandler>();
            services.AddScoped<IRequestHandler<SupplierUpdateCommand, CommandResponse>, SupplierCommandHandler>();
            services.AddScoped<IRequestHandler<SupplierRemoveCommand, CommandResponse>, SupplierCommandHandler>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();


            // Save Category
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IRequestHandler<CategoryAddCommand, CommandResponse>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryUpdateCommand, CommandResponse>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<CategoryRemoveCommand, CommandResponse>, CategoryCommandHandler>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            // Save Account
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IRequestHandler<AccountRegisterCommand, CommandResponse>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<AccountUpdateCommand, CommandResponse>, AccountCommandHandler>();
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
