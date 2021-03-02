using NorthwindApi.Domain.Domain.Products.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Products
{
    public class ProductRules
    {
        public static void CheckUnitPrice(decimal UnitPrice)
        {
            if(UnitPrice<=0)
            {
                throw new InvalidProductPriceException();
            }
        }

        public static void CheckUnitsInStock(double UnitsInStock)
        {
            if (UnitsInStock<=0)
            {
                throw new InvalidProductUnitsInStockException();
            }
        }

    }
}
