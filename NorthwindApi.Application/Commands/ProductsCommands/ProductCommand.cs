using MediatR;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Command;
namespace NorthwindApi.Application.Commands.ProductsCommands
{
    public class ProductCommand:Command, IRequest<Product>
    {
        public Guid Id { get; protected set; }
        public string ProductName { get; protected set; }
        public Guid SupplierID { get; protected set; }
        public Guid CategoryID { get; protected set; }
        public string QuantityPerUnit { get; protected set; }
        public decimal UnitPrice { get; protected set; }
        public double UnitsInStock { get; protected set; }
    }
}
