using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Shippers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.ShipperCommands
{
    public class ShipperCommandHandler : BaseCommandHandler,
        IRequestHandler<ShipperAddCommand, ValidationResult>,
        IRequestHandler<ShipperUpdateCommand, ValidationResult>,
        IRequestHandler<ShipperRemoveCommand, ValidationResult>
    {

        public readonly IShippersRepository _shippersRepository;

        public ShipperCommandHandler(IShippersRepository shippersRepository)
        {
            _shippersRepository = shippersRepository;
        }

        public async Task<ValidationResult> Handle(ShipperAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return validationResult;

            var addShipper = new Shipper(new Guid(),request.CompanyName,request.Phone);

            await _shippersRepository.Add(addShipper);

            return await Commit(_shippersRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(ShipperUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return validationResult;

            var checkShipper = await _shippersRepository.FindById(request.Id);
            if (checkShipper==null)
            {
                validationResult.Errors.Add(new ValidationFailure("", "Invalid Shipper Id"));
                return validationResult;
            }

            var updateShipper = new Shipper(request.Id, request.CompanyName, request.Phone);

            _shippersRepository.Update(updateShipper);

            return await Commit(_shippersRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(ShipperRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var shipper = await _shippersRepository.FindById(request.Id);

            if (shipper == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Shipper not found."));
                return request.ValidationResult;
            }

            // TODO Here will Add to Domain Event....

            _shippersRepository.Remove(shipper);

            return await Commit(_shippersRepository.UnitOfWork);
        }
    }
}
