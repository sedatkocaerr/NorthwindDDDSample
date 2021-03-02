using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class OrderDetailViewModel
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Order ID")]
        public Guid OrderID { get;  set; }

        [Required]
        [DisplayName("Product Id")]
        public Guid ProductId { get;  set; }

        [Required(ErrorMessage = "UnitPrice is Required.")]
        [DisplayName("UnitPrice")]
        public double UnitPrice { get;  set; }

        [Required(ErrorMessage = "Quantity is Required.")]
        [DisplayName("Quantity")]
        public short Quantity { get;  set; }

        [DisplayName("DisCount")]
        public double DisCount { get;  set; }
    }
}
