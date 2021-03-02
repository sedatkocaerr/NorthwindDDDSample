using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Events.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.Orders
{
    public class OrderCommandHandler : BaseCommandHandler,
        
        IRequestHandler<OrderAddCommand, ValidationResult>,
        IRequestHandler<OrderRemoveCommand, ValidationResult>,
        IRequestHandler<OrderUpdateCommand, ValidationResult>
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

        public async Task<ValidationResult> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var checkCustomer = await _customerRepository.FindById(request.CustomerID);

            var checkEmployee = await _employeeRepository.FindById(request.EmployeeID);

            if (checkCustomer == null|| checkEmployee==null)
            {
                if (checkCustomer == null)
                {
                    request.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Customer ID"));
                }
                else
                {
                    request.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Employee ID"));
                }
                return  request.ValidationResult;
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

        public async Task<ValidationResult> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var orderData = await _orderRepository.FindById(request.Id);
            Customer checkCustomer = null;
            Employee checkEmployee = null;
            if (orderData==null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Order Id"));
                return request.ValidationResult;
            }
            else
            {
               
                checkCustomer = await _customerRepository.FindById(request.CustomerID);
                if (checkCustomer == null)
                {
                    request.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Customer ID"));
                    return request.ValidationResult;
                }
               
                checkEmployee = await _employeeRepository.FindById(request.EmployeeID);
                if (checkEmployee == null)
                {
                    request.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Employee ID"));
                    return request.ValidationResult;
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

        public async Task<ValidationResult> Handle(OrderRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var order = await _orderRepository.FindById(request.Id);

            if (order == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Order not found."));
                return request.ValidationResult;
            }

            // TODO Here will Add to Domain Event....

            order.Apply(new OrderRemoveEvent(order.Id));

            _orderRepository.Remove(order);

            await _eventStoreRepository.SaveAsync<Order>(order);

            return await Commit(_customerRepository.UnitOfWork);
        }

       
    }
}
