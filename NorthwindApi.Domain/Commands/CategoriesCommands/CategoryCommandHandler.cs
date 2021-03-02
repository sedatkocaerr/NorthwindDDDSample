using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.CategoriesCommands
{
    public class CategoryCommandHandler : BaseCommandHandler,
        IRequestHandler<CategoryAddCommand,ValidationResult>,
        IRequestHandler<CategoryRemoveCommand, ValidationResult>,
        IRequestHandler<CategoryUpdateCommand, ValidationResult>
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        

        public async Task<ValidationResult> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var addCategory = new Category(Guid.NewGuid(), request.Name, request.Description, request.Picture);

            await _categoryRepository.Add(addCategory);

            return await Commit(_categoryRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var updateCategory = new Category(request.Id, request.Name, request.Description, request.Picture);

            var checkCategory = await _categoryRepository.FindById(updateCategory.Id);

            if (checkCategory==null)
            {
                validationResult.Errors.Add(new ValidationFailure("", "Invalid Category Id"));
                return validationResult;
            }

            _categoryRepository.Update(updateCategory);

            return await Commit(_categoryRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var categoryCheck = await _categoryRepository.FindById(request.Id);

            if (categoryCheck is null)
            {
                validationResult.Errors.Add(new ValidationFailure("", "The category doesn't exists."));
                return validationResult;
            }

            _categoryRepository.Remove(categoryCheck);

            return await Commit(_categoryRepository.UnitOfWork);

        }

    }
}
