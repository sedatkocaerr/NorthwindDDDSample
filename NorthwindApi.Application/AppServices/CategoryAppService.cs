using AutoMapper;
using FluentValidation.Results;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Commands.CategoriesCommands;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryAppService(IMapper mapper, IMediatorHandler mediatorHandler, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _categoryRepository = categoryRepository;
        }

        public async Task<CommandResponse> AddCategory(CategoryViewModel categoryViewModel)
        {
           var addCommand = _mapper.Map<CategoryAddCommand>(categoryViewModel);
            return await _mediatorHandler.SendCommand<CategoryAddCommand>(addCommand);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll());
        }

        public async Task<CategoryViewModel> GetById(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.FindById(id));
        }

        public async Task<CommandResponse> Remove(Guid id)
        {
            var removeCommand = new CategoryRemoveCommand(id);
            return await _mediatorHandler.SendCommand<CategoryRemoveCommand>(removeCommand);
        }

        public async Task<CommandResponse> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var updateCommand = _mapper.Map<CategoryUpdateCommand>(categoryViewModel);
            return await _mediatorHandler.SendCommand<CategoryUpdateCommand>(updateCommand);
        }
    }
}
