using AutoMapper;
using FluentValidation.Results;
using MediatR;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Commands.CustomersCommands;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Data.Mediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using System.Linq;

namespace NorthwindApi.Application.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private IMapper _mapper;
        //private ICustomersRepository _customersRepository;
        private IElasticSearchService _elasticSearchService;
        private IMediatorHandler _mediator;
        public CustomerAppService(IElasticSearchService elasticSearchService, IMediatorHandler mediator
            , IMapper mapper)
        {
            _elasticSearchService = elasticSearchService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ValidationResult> AddCustomer(CustomerViewModel customerViewModel)
        {
           var addCustomerCommand = _mapper.Map<CustomerAddCommand>(customerViewModel);
           return await _mediator.SendCommand(addCustomerCommand);
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            var customerlist = await _elasticSearchService.SimpleSearchAsync("customerevent",
                new Nest.SearchDescriptor<CustomerViewModel>().Query(x => x.MatchAll()).From(0)
                .Size(2000));

            return customerlist.Documents.ToList();
        }

        public async Task<CustomerViewModel> GetById(Guid id)
        {
           var customer = await _elasticSearchService.GetId<CustomerViewModel>("customerevent",id);
            return customer;
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new CustomerRemoveCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> UpdateCustomer(CustomerViewModel customerViewModel)
        {
            var addCustomerCommand = _mapper.Map<CustomerUpdateCommand>(customerViewModel);
            return await _mediator.SendCommand(addCustomerCommand);
        }
    }
}
