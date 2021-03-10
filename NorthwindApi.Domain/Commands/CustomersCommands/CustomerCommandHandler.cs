using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Events.CustomersEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.CustomersCommands
{
    public class CustomerCommandHandler : BaseCommandHandler,
        IRequestHandler<CustomerAddCommand, CommandResponse>,
        IRequestHandler<CustomerUpdateCommand, CommandResponse>,
        IRequestHandler<CustomerRemoveCommand, CommandResponse>
    {

        private readonly ICustomersRepository _customersRepository;
        private readonly IEventSourceRepository _eventStoreRepository; 
        public CustomerCommandHandler(ICustomersRepository customersRepository, IEventSourceRepository eventStoreRepository)
        {
            _customersRepository = customersRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<CommandResponse> Handle(CustomerAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;
            var customer = new Customer(Guid.NewGuid(), request.CompanyName,request.ContactName,request.ContactTitle,
               request.Email, request.Address,request.City, request.PostalCode, request.Country,request.Phone);

            var checkEmail = await _customersRepository.GetByEmail(customer.Email);
            if (checkEmail!= null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("Email", "E-Mail Alreadt Exist"));
                return request.CommandResponse;
            }

            // TODO Here will Add to Domain Event....

            customer.Apply(new CustomerAddEvent(customer.Id, customer.CompanyName, customer.ContactName, customer.ContactTitle,
               customer.Email, customer.Address, customer.City, customer.PostalCode, customer.Country, customer.Phone));

            // Create Db
            _customersRepository.Add(customer);

            // Create Event Store
            await _eventStoreRepository.SaveAsync<Customer>(customer);

            // Commit
            return await Commit(_customersRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;
            var customer = new Customer(request.Id, request.CompanyName, request.ContactName, request.ContactTitle,
               request.Email, request.Address, request.City, request.PostalCode, request.Country, request.Phone);

            var customerData = await _customersRepository.GetByEmail(customer.Email);
            if (customerData != null&& customerData.Id!=request.Id)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("Email", "E-Mail Alreadt Exist"));
                return request.CommandResponse;
            }

            // TODO Here will Add to Domain Event....
            customer.Apply(new CustomerUpdateEvent(customer.Id, customer.CompanyName, customer.ContactName, customer.ContactTitle,
               customer.Email, customer.Address, customer.City, customer.PostalCode, customer.Country, customer.Phone));

            _customersRepository.Update(customer);

            await _eventStoreRepository.SaveAsync<Customer>(customer);

            return await Commit(_customersRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(CustomerRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var customer = await _customersRepository.FindById(request.Id);

            if(customer==null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Customer not found."));
                return request.CommandResponse;
            }

            // TODO Here will Add to Domain Event....
            customer.Apply(new CustomerRemoveEvent(customer.Id));

            _customersRepository.Remove(customer);

            await _eventStoreRepository.SaveAsync<Customer>(customer);

            return await Commit(_customersRepository.UnitOfWork);
        }
    }
}
