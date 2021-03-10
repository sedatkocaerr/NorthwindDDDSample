using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.CategoriesCommands
{
    public class CategoryCommandHandler : BaseCommandHandler,
        IRequestHandler<CategoryAddCommand, CommandResponse>,
        IRequestHandler<CategoryRemoveCommand, CommandResponse>,
        IRequestHandler<CategoryUpdateCommand, CommandResponse>
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        

        public async Task<CommandResponse> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var addCategory = new Category(Guid.NewGuid(), request.Name, request.Description, request.Picture);

            await _categoryRepository.Add(addCategory);

            return await Commit(_categoryRepository.UnitOfWork,addCategory.Id);
        }

        public async Task<CommandResponse> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var updateCategory = new Category(request.Id, request.Name, request.Description, request.Picture);

            var checkCategory = await _categoryRepository.FindById(updateCategory.Id);

            if (checkCategory==null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("", "Invalid Category Id"));
                return request.CommandResponse;
            }

            _categoryRepository.Update(updateCategory);

            return await Commit(_categoryRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var categoryCheck = await _categoryRepository.FindById(request.Id);

            if (categoryCheck is null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("", "The category doesn't exists."));
                return request.CommandResponse;
            }

            _categoryRepository.Remove(categoryCheck);

            return await Commit(_categoryRepository.UnitOfWork);

        }

    }
}
