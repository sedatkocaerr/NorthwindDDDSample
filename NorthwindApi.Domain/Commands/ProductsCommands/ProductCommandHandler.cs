using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Domain.Suppliers;
using NorthwindApi.Domain.Events.ProductsEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.ProductsCommands
{
    public class ProductCommandHandler : BaseCommandHandler,
        IRequestHandler<ProductAddCommand, ValidationResult>,
        IRequestHandler<ProductUpdateCommand, ValidationResult>,
        IRequestHandler<ProductRemoveCommand, ValidationResult>
    {

        private readonly IProductRepository _productRepository;
        private readonly IEventSourceRepository _eventSourceRepository;
        private readonly ISupplierRepository  _supplierRepository;
        private readonly ICategoryRepository  _categoryRepository;

        public ProductCommandHandler(IProductRepository productRepository, IEventSourceRepository eventSourceRepository,
            ISupplierRepository supplierRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _eventSourceRepository = eventSourceRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ValidationResult> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            Category checkCategory = null;
            Supplier checkSupplier = null;

            checkCategory = await _categoryRepository.FindById(request.CategoryID);
            // check supllier Id

            checkSupplier = await _supplierRepository.FindById(request.SupplierID);
            // Check category Id

            //anything is null
            if (checkCategory==null || checkSupplier==null)
            {
                if (checkCategory==null)
                {
                    validationResult.Errors.Add(new ValidationFailure("", "Invalid Category Id"));
                    return validationResult;
                }
                validationResult.Errors.Add(new ValidationFailure("", "Invalid Supplier Id"));
                return validationResult;
            }

            var product = new Product(Guid.NewGuid(), checkCategory, checkSupplier, request.ProductName, request.QuantityPerUnit,
                request.UnitPrice, request.UnitsInStock);

            product.Apply(new ProductAddEvent(product.Id, product.ProductName, product.SupplierID, product.CategoryID
                , product.QuantityPerUnit, product.UnitPrice, product.UnitsInStock));

            await _productRepository.Add(product);

            await _eventSourceRepository.SaveAsync<Product>(product);

            return await Commit(_productRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            Category checkCategory = null;
            Supplier checkSupplier = null;

            var productCheck = await _productRepository.FindById(request.Id);

            if (productCheck == null)
            {
                validationResult.Errors.Add(new ValidationFailure("", "Invalid Product Id"));
                return validationResult;
            }

            checkCategory = await _categoryRepository.FindById(request.CategoryID);
            // check supllier Id

            checkSupplier = await _supplierRepository.FindById(request.SupplierID);
            // Check category Id

            //anything is null
            if (checkCategory == null || checkSupplier == null)
            {
                if (checkCategory == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("", "Invalid Category Id"));
                    return validationResult;
                }
                validationResult.Errors.Add(new ValidationFailure("", "Invalid Supplier Id"));
                return validationResult;
            }

            var product = new Product(request.Id, checkCategory, checkSupplier, request.ProductName, request.QuantityPerUnit,
                request.UnitPrice, request.UnitsInStock);

            product.Apply(new ProductUpdateEvent(product.Id, product.ProductName, product.SupplierID, product.CategoryID
                , product.QuantityPerUnit, product.UnitPrice, product.UnitsInStock));

             _productRepository.Update(product);

            await _eventSourceRepository.SaveAsync<Product>(product);

            return await Commit(_productRepository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var product = await _productRepository.FindById(request.Id);

            if (product == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "Product not found."));
                return request.ValidationResult;
            }

            // TODO Here will Add to Domain Event....
            product.Apply(new ProductRemoveEvent(product.Id));

            _productRepository.Remove(product);

            await _eventSourceRepository.SaveAsync<Product>(product);

            return await Commit(_productRepository.UnitOfWork);
        }
    }
}
