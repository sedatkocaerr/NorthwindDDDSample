using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NorthwindApi.Domain.Core.Domain;

namespace NorthwindApi.Domain.Domain.Categories
{
    public class Category:Entity, IAggregateRoot
    {
        protected Category()
        {
            _products = new List<Product>();
        }

        private List<Product> _products;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; set; }

        public IEnumerable<Product> Products
        {
            get { return new ReadOnlyCollection<Product>(_products); }
        }

        public Category (Guid id,string name, string description = "", string picture="")
        {
            TypeDataCheck.checkId(id);
            TypeDataCheck.IsNullOrEmpty(name);

            SetId(id);
            SetName(name);
            Description = description;
            Picture = picture;
        }
        public virtual void Add(Product product)
        {
            _products.Add(product);
            //TODO: check product whether exists in another category and change product
        }

        public void SetName(string name)
        {
            TypeDataCheck.IsNullOrEmpty(name);
            Name = name;
        }

        public void SetId(Guid id)
        {
            TypeDataCheck.checkId(id);
            Id = id;
        }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
