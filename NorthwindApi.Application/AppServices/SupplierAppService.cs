using AutoMapper;
using FluentValidation.Results;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Commands.SuppliersCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class SupplierAppService : ISupplierAppService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private IMapper _mapper;
        private readonly IElasticSearchService _elasticSearchService;

        public SupplierAppService(IMediatorHandler mediatorHandler, IMapper mapper
            , IElasticSearchService elasticSearchService)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _elasticSearchService = elasticSearchService;
        }

        public async Task<ValidationResult> AddSupplier(SupplierViewModel supplierViewModel)
        {
           var addCommand =  _mapper.Map<SupplierAddCommand>(supplierViewModel);
           return await _mediatorHandler.SendCommand<SupplierAddCommand>(addCommand);
        }

        public async Task<IEnumerable<SupplierViewModel>> GetAll()
        {
           var data = await _elasticSearchService.SimpleSearchAsync<SupplierViewModel>("supplierevent",
                new Nest.SearchDescriptor<SupplierViewModel>().Query(x => x.MatchAll()).From(0)
                .Size(2000));
           return  data.Documents;
        }

        public async Task<SupplierViewModel> GetById(Guid id)
        {
            var data = await _elasticSearchService.GetId<SupplierViewModel>("supplierevent", id);
            return data;
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new SupplierRemoveCommand(id);
            return await _mediatorHandler.SendCommand<SupplierRemoveCommand>(removeCommand);
        }

        public async Task<ValidationResult> UpdateSupplier(SupplierViewModel supplierViewModel)
        {
            var updateCommand = _mapper.Map<SupplierUpdateCommand>(supplierViewModel);
            return await _mediatorHandler.SendCommand<SupplierUpdateCommand>(updateCommand);
        }
    }
}
