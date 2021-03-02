using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class ShipperViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The CompanyName is Required.")]
        [StringLength(maximumLength: 40, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 40")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Phone is Required.")]
        [StringLength(maximumLength: 24, MinimumLength = 9, ErrorMessage = "Length must be between 2 to 24")]
        [DisplayName("Phone")]
        public string Phone { get; set; }
    }
}
