using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class CustomerViewModel
    {
        

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The CompanyName is Required.")]
        [StringLength(maximumLength: 40, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 40")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "The ContactName is Required.")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 30")]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "The Contact Title is Required.")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 30")]
        [DisplayName("Contact Title")]
        public string ContactTitle { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 30")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is Required.")]
        [StringLength(maximumLength: 60, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 60")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 15")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "The Postal Code is Required.")]
        [StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 10")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "The Country is Required.")]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 15")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "The Phone is Required.")]
        [StringLength(maximumLength: 24, MinimumLength = 9, ErrorMessage = "Length must be between 9 to 24")]
        [DisplayName("Phone")]
        public string Phone { get; set; }
    }
}
