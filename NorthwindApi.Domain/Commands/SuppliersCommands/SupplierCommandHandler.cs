using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Suppliers;
using NorthwindApi.Domain.Events.SupplierEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.SuppliersCommands
{
    public class SupplierCommandHandler : BaseCommandHandler,
        IRequestHandler<SupplierAddCommand, CommandResponse>,
        IRequestHandler<SupplierUpdateCommand, CommandResponse>,
        IRequestHandler<SupplierRemoveCommand, CommandResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IEventSourceRepository _eventSourceRepository;
        public SupplierCommandHandler(ISupplierRepository supplierRepository, IEventSourceRepository eventSourceRepository)
        {
            _supplierRepository = supplierRepository;
            _eventSourceRepository = eventSourceRepository;
        }

        public async Task<CommandResponse> Handle(SupplierAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var supplier = new Supplier(Guid.NewGuid(), request.CompanyName, request.ContactName, request.ContactTitle,
                request.Adress, request.City, request.Country, request.Phone);

            supplier.Apply(new SupplierAddEvent(supplier.Id, supplier.CompanyName, supplier.ContactName, supplier.ContactTitle,
                supplier.Adress, supplier.City, supplier.Country, supplier.Phone));

             await _supplierRepository.Add(supplier);

             await _eventSourceRepository.SaveAsync<Supplier>(supplier);

            return await Commit(_supplierRepository.UnitOfWork, supplier.Id);
        }

        public async Task<CommandResponse> Handle(SupplierUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var checkSupplier = await _supplierRepository.FindById(request.Id);
            if (checkSupplier==null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "Invalid Supplier Id"));
                return request.CommandResponse;
            }

            var supplier = new Supplier(request.Id, request.CompanyName, request.ContactName, request.ContactTitle,
                request.Adress, request.City, request.Country, request.Phone);

            supplier.Apply(new SupplierUpdateEvent(supplier.Id, supplier.CompanyName, supplier.ContactName, supplier.ContactTitle,
                supplier.Adress, supplier.City, supplier.Country, supplier.Phone));

             _supplierRepository.Update(supplier);

            await _eventSourceRepository.SaveAsync<Supplier>(supplier);

            return await Commit(_supplierRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(SupplierRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var supplier = await _supplierRepository.FindById(request.Id);

            if (supplier == null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Supplier not found."));
                return request.CommandResponse;
            }

            // TODO Here will Add to Domain Event....
            supplier.Apply(new SupplierRemoveEvent(supplier.Id));

            _supplierRepository.Remove(supplier);

            await _eventSourceRepository.SaveAsync<Supplier>(supplier);

            return await Commit(_supplierRepository.UnitOfWork);
        }
    }
}
