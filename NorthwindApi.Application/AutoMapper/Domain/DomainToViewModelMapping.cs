using AutoMapper;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Commands.CustomersCommands;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Domain.OrderDetails;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Events.CustomersEvents;
using NorthwindApi.Domain.Events.EmployeesEvents;
using NorthwindApi.Domain.Events.Orders;
using NorthwindApi.Domain.Events.ProductsEvents;
using NorthwindApi.Domain.Events.SupplierEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.AutoMapper
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<OrderDetail, OrderDetailViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Shipper, ShipperViewModel>();

            // Event Mapping 
            CreateMap<CustomerAddEvent, CustomerViewModel>();
            CreateMap<CustomerUpdateEvent, CustomerViewModel>();
            CreateMap<CustomerRemoveEvent, CustomerViewModel>();

            CreateMap<EmployeeAddEvent, EmployeeViewModel>();
            CreateMap<EmployeeUpdateEvent, EmployeeViewModel>();
            CreateMap<EmployeeRemoveEvent, EmployeeViewModel>();

            CreateMap<OrderAddEvent, OrderViewModel>();
            CreateMap<OrderUpdateEvent, OrderViewModel>();
            CreateMap<OrderRemoveEvent, OrderViewModel>();

            CreateMap<ProductAddEvent, ProductViewModel>();
            CreateMap<ProductUpdateEvent, ProductViewModel>();
            CreateMap<ProductRemoveEvent, ProductViewModel>();

            CreateMap<SupplierAddEvent, SupplierViewModel>();
            CreateMap<SupplierUpdateEvent, SupplierViewModel>();
            CreateMap<SupplierRemoveEvent, SupplierViewModel>();
        }
    }
}
