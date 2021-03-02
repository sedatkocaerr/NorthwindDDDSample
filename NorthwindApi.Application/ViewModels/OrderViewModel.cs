using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        [Required]
        public Guid EmployeeID { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        [Required(ErrorMessage = "Ship Name is Required.")]
        [StringLength(maximumLength: 40, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 40")]
        [DisplayName("Ship Name")]
        public string ShipName { get; set; }

        [Required(ErrorMessage = "Ship Address is Required.")]
        [StringLength(maximumLength: 60, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 60")]
        [DisplayName("Ship Address")]
        public string ShipAddress { get; set; }

        [Required(ErrorMessage = "Ship City is Required.")]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 15")]
        [DisplayName("Ship City")]
        public string ShipCity { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 10")]
        [DisplayName("Ship Postal Code")]
        public string ShipPostalCode { get; set; }

        [Required(ErrorMessage = "Ship Country is Required.")]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 15")]
        [DisplayName("Ship Country")]
        public string ShipCountry { get; set; }

        [Required]
        public Guid ShipVia { get; set; }

    }
}
