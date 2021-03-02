using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Name is Required.")]
        [StringLength(maximumLength: 40, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 10")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("SupplierID")]
        public Guid SupplierID { get; set; }

        [Required]
        [DisplayName("CategoryID")]
        public Guid CategoryID { get; set; }

        [Required(ErrorMessage = "Quantity Per Unit is Required.")]
        [StringLength(maximumLength:20,MinimumLength = 1, ErrorMessage = "Quantity Per Unit Length must be Minimum 1 maximum 20")]
        [DisplayName("Quantity Per Unit")]
        public string QuantityPerUnit { get;  set; }

        [Required(ErrorMessage = "UnitPrice is Required.")]
        public decimal UnitPrice { get;  set; }

        [Required(ErrorMessage = "Units In Stock is Required.")]
        public double UnitsInStock { get;  set; }
    }
}
