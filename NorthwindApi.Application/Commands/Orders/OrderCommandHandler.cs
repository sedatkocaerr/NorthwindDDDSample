using FluentValidation.Results;
using MediatR;
using NorthwindApi.Application.Commands.Handler;
using NorthwindApi.Domain;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Events.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Commands.Orders
{
    public class OrderCommandHandler : BaseCommandHandler,
        
        IRequestHandler<OrderAddCommand, CommandResponse>,
        IRequestHandler<OrderRemoveCommand, CommandResponse>,
        IRequestHandler<OrderUpdateCommand, CommandResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeesRepository _employeeRepository;
        private readonly ICustomersRepository _customerRepository;
        private readonly IEventSourceRepository _eventStoreRepository;

        public OrderCommandHandler(IOrderRepository orderRepository,
            IEmployeesRepository employeeRepository, ICustomersRepository
            customerRepository, IEventSourceRepository eventStoreRepository) 
        {
            _orderRepository = orderRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<CommandResponse> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;
            var checkCustomer = await _customerRepository.FindById(request.CustomerID);

            var checkEmployee = await _employeeRepository.FindById(request.EmployeeID);

            if (checkCustomer == null|| checkEmployee==null)
            {
                if (checkCustomer == null)
                {
                    request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Customer ID"));
                }
                else
                {
                    request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Employee ID"));
                }
                return request.CommandResponse;
            }
           
            var order = new Order(Guid.NewGuid(), checkCustomer,checkEmployee,request.RequiredDate,request.ShipName,
                request.ShipAddress,request.ShipCity,request.ShipPostalCode,request.ShipCountry,request.ShipVia);

            order.Apply(new OrderAddEvent(order.Id, order.CustomerID, order.EmployeeID, order.OrderDate,
                order.RequiredDate, order.ShippedDate, order.ShipName, order.ShipAddress, order.ShipCity,
                order.ShipPostalCode, order.ShipCountry,order.ShipVia));

            await _orderRepository.Add(order);

            await _eventStoreRepository.SaveAsync<Order>(order);

            return await Commit(_orderRepository.UnitOfWork);

        }

        public async Task<CommandResponse> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var orderData = await _orderRepository.FindById(request.Id);
            Customer checkCustomer = null;
            Employee checkEmployee = null;
            if (orderData==null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Order Id"));
                return request.CommandResponse;
            }
            else
            {
               
                checkCustomer = await _customerRepository.FindById(request.CustomerID);
                if (checkCustomer == null)
                {
                    request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Customer ID"));
                    return request.CommandResponse;
                }
               
                checkEmployee = await _employeeRepository.FindById(request.EmployeeID);
                if (checkEmployee == null)
                {
                    request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Employee ID"));
                    return request.CommandResponse;
                }
            }

            var order = new Order(request.Id, checkCustomer, checkEmployee, request.RequiredDate, request.ShipName,
                request.ShipAddress, request.ShipCity, request.ShipPostalCode, request.ShipCountry,request.ShipVia);

            order.Apply(new OrderAddEvent(order.Id, order.CustomerID, order.EmployeeID, order.OrderDate,
                order.RequiredDate, order.ShippedDate, order.ShipName, order.ShipAddress, order.ShipCity,
                order.ShipPostalCode, order.ShipCountry, order.ShipVia));

             _orderRepository.Update(order);

            await _eventStoreRepository.SaveAsync<Order>(order);

            return await Commit(_orderRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(OrderRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var order = await _orderRepository.FindById(request.Id);

            if (order == null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Order not found."));
                return request.CommandResponse;
            }

            // TODO Here will Add to Domain Event....

            order.Apply(new OrderRemoveEvent(order.Id));

            _orderRepository.Remove(order);

            await _eventStoreRepository.SaveAsync<Order>(order);

            return await Commit(_customerRepository.UnitOfWork);
        }

       
    }
}
