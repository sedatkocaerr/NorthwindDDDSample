using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.OrderDetails
{
    public class OrderDetail : Entity
    {
        protected OrderDetail()
        {
        }
        public Guid OrderID { get; private set; }
        public Guid ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public short Quantity { get; private set; }
        public double DisCount { get; private set; }

        public Order Order { get; private set; }

        public Product Product { get; private set; }

        public  OrderDetail  (Guid id,Product product, Order order, double unitPrice,
            short quantity, double disCount=0)
        {
            TypeDataCheck.checkId(id);
            Id = id;

            TypeDataCheck.IsNull(product);
            SetProductId(product.Id);

            TypeDataCheck.IsNull(order);
            SetOrderId(order.Id);

            SetUnitPrice(unitPrice);
            SetQuantity(quantity);
            SetDisCount(disCount);
        }

        public void SetDisCount(double disCount)
        {
            if(disCount>0)
            {
                DisCount= disCount;
            }
            
        }

        public void SetQuantity(short quantity)
        {
            OrderDetailsRules.CheckOrderDetailQuantity(quantity);
            Quantity = quantity;
        }

        public void SetUnitPrice(double unitPrice)
        {
            OrderDetailsRules.CheckOrderDetailUnitPrice(unitPrice);
            UnitPrice = unitPrice;
        }

        public void SetProductId(Guid productId)
        {
            TypeDataCheck.checkId(productId);
            ProductId = productId;
        }

        public void SetOrderId(Guid orderId)
        {
            TypeDataCheck.checkId(orderId);
            OrderID = orderId;
        }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
