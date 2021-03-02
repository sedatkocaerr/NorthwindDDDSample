using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is Required.")]
        [StringLength(maximumLength: 10, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 10")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required.")]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 20")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Title is Required.")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 30")]
        [DisplayName("Title")]
        public string Title { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Address is Required.")]
        [StringLength(maximumLength: 60, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 60")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 15")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is Required.")]
        [StringLength(maximumLength: 10, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 10")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is Required.")]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 15")]
        [DisplayName("Country")]
        public string Country { get; set; }

        
    }
}
