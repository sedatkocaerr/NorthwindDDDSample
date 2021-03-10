using AutoMapper;
using FluentValidation.Results;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Commands.EmployeesCommands;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class EmployeeAppService : IEmployeeAppService
    {
        private IElasticSearchService _elasticSearchService;
        private IMediatorHandler _mediatorHandler;
        private IMapper _mapper;

        public EmployeeAppService(IElasticSearchService elasticSearchService, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _elasticSearchService = elasticSearchService;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<CommandResponse> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employeeCommand= _mapper.Map<EmployeeAddCommand>(employeeViewModel);
            var res = await _mediatorHandler.SendCommand<EmployeeAddCommand>(employeeCommand);
            return res;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAll()
        {
          var res =  await _elasticSearchService.SimpleSearchAsync("employeeevent",
                new Nest.SearchDescriptor<EmployeeViewModel>().Query(x => x.MatchAll()).From(0)
                .Size(2000));
            return res.Documents.ToList();
        }

        public async Task<EmployeeViewModel> GetById(Guid id)
        {
            var res = await _elasticSearchService.GetId<EmployeeViewModel>("employeeevent", id);
            return res;
        }

        public async Task<CommandResponse> Remove(Guid id)
        {
            var employeeRemoveCommand = new EmployeeRemoveCommand(id);
            var res = await _mediatorHandler.SendCommand<EmployeeRemoveCommand>(employeeRemoveCommand);
            return res;
        }

        public async Task<CommandResponse> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var employeeRemoveCommand = _mapper.Map<EmployeeUpdateCommand>(employeeViewModel);
            var res = await _mediatorHandler.SendCommand<EmployeeUpdateCommand>(employeeRemoveCommand);
            return res;
        }
    }
}
