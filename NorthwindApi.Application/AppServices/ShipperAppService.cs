using AutoMapper;
using FluentValidation.Results;
using NorthwindApi.Application.Commands.ShipperCommands;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Shippers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class ShipperAppService : IShipperAppService
    {
        private IMapper _mapper;
        private readonly IShippersRepository _shippersRepository;
        private IMediatorHandler _mediatorHandler;

        public ShipperAppService(IMapper mapper, IShippersRepository shippersRepository, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _shippersRepository = shippersRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CommandResponse> AddShipper(ShipperViewModel shipperViewModel)
        {
            var shipperAddCommand = _mapper.Map<ShipperAddCommand>(shipperViewModel);
            return await _mediatorHandler.SendCommand<ShipperAddCommand>(shipperAddCommand);
        }

        public async Task<IEnumerable<ShipperViewModel>> GetAll()
        {
            return _mapper.Map<List<ShipperViewModel>>(await _shippersRepository.GetAll());
        }

        public async Task<ShipperViewModel> GetById(Guid id)
        {
           var shipper = await _shippersRepository.FindById(id);
            return _mapper.Map<ShipperViewModel>(shipper);
        }

        public async Task<CommandResponse> Remove(Guid id)
        {
            var shipperRemoveCommand = new ShipperRemoveCommand(id);
            return await _mediatorHandler.SendCommand<ShipperRemoveCommand>(shipperRemoveCommand);
        }

        public async Task<CommandResponse> UpdateShipper(ShipperViewModel shipperViewModel)
        {
            var shipperUpdateCommand = _mapper.Map<ShipperUpdateCommand>(shipperViewModel);
            return await _mediatorHandler.SendCommand<ShipperUpdateCommand>(shipperUpdateCommand);
        }
    }
}
