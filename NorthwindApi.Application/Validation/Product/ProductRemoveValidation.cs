using NorthwindApi.Application.Commands.ProductsCommands;

namespace NorthwindApi.Domain.Validation.Product
{
    public class ProductRemoveValidation:ProductValidation<ProductRemoveCommand>
    {
        public ProductRemoveValidation()
        {
            ValidateId();
        }
    }
}
