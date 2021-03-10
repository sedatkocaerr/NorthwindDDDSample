using AutoMapper;
using FluentValidation.Results;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Commands.ProductsCommands;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class ProductAppService : IProductAppService
    {
        private IElasticSearchService _elasticSearchService;
        private IMediatorHandler _mediatorHandler;
        private IMapper _mapper;

        public ProductAppService(IElasticSearchService elasticSearchService,
            IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _elasticSearchService = elasticSearchService;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<CommandResponse> AddProduct(ProductViewModel productViewModel)
        {
            var addProductCommand = _mapper.Map<ProductAddCommand>(productViewModel);
            return await _mediatorHandler.SendCommand<ProductAddCommand>(addProductCommand);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
           var data = await _elasticSearchService.SimpleSearchAsync<ProductViewModel>("productevent",
                new Nest.SearchDescriptor<ProductViewModel>().Query(x => x.MatchAll()).From(0)
                .Size(2000));
            return data.Documents;
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
           return  await _elasticSearchService.GetId<ProductViewModel>("productevent", id);
        }

        public async Task<CommandResponse> Remove(Guid id)
        {
            var removecommand = new ProductRemoveCommand(id);
           return await _mediatorHandler.SendCommand<ProductRemoveCommand>(removecommand);
        }

        public async Task<CommandResponse> UpdateProduct(ProductViewModel productViewModel)
        {
            var updatecommand = _mapper.Map<ProductUpdateCommand>(productViewModel);
            return await _mediatorHandler.SendCommand<ProductUpdateCommand>(updatecommand);
        }
    }
}
