using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Company Name is Required.")]
        [StringLength(maximumLength: 40, ErrorMessage = "Length must be maximum 40")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "The Contact Name is Required.")]
        [StringLength(maximumLength: 30, ErrorMessage = "Length must be max 30")]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "The Contact Title is Required.")]
        [StringLength(maximumLength: 30, ErrorMessage = "Length must be Max 30")]
        [DisplayName("Contact Title")]
        public string ContactTitle { get; set; }

        [Required(ErrorMessage = "The Adress is Required.")]
        [StringLength(maximumLength: 60, ErrorMessage = "Length must be Max 60")]
        [DisplayName("Adress")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "The City is Required.")]
        [StringLength(maximumLength: 15, ErrorMessage = "Length must be Max 15")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "The Country is Required.")]
        [StringLength(maximumLength: 15, ErrorMessage = "Length must be Max 15")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "The Phone is Required.")]
        [StringLength(maximumLength: 24, MinimumLength =9, ErrorMessage = "Length must be Min 9 Max 24 size")]
        [DisplayName("Phone")]
        public string Phone { get; set; }
    }
}
