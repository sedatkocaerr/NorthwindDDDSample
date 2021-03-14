using FluentValidation.Results;
using MediatR;
using NorthwindApi.Application.Commands.Handler;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Shippers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Commands.ShipperCommands
{
    public class ShipperCommandHandler : BaseCommandHandler,
        IRequestHandler<ShipperAddCommand, CommandResponse>,
        IRequestHandler<ShipperUpdateCommand, CommandResponse>,
        IRequestHandler<ShipperRemoveCommand, CommandResponse>
    {

        public readonly IShippersRepository _shippersRepository;

        public ShipperCommandHandler(IShippersRepository shippersRepository)
        {
            _shippersRepository = shippersRepository;
        }

        public async Task<CommandResponse> Handle(ShipperAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var addShipper = new Shipper(new Guid(),request.CompanyName,request.Phone);

            await _shippersRepository.Add(addShipper);

            return await Commit(_shippersRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(ShipperUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var checkShipper = await _shippersRepository.FindById(request.Id);
            if (checkShipper==null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("", "Invalid Shipper Id"));
                return request.CommandResponse;
            }

            var updateShipper = new Shipper(request.Id, request.CompanyName, request.Phone);

            _shippersRepository.Update(updateShipper);

            return await Commit(_shippersRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(ShipperRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var shipper = await _shippersRepository.FindById(request.Id);

            if (shipper == null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Shipper not found."));
                return request.CommandResponse;
            }

            // TODO Here will Add to Domain Event....

            _shippersRepository.Remove(shipper);

            return await Commit(_shippersRepository.UnitOfWork);
        }
    }
}
