using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels.AccountViewModels
{
    public class AccountViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The SurName is Required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        [DisplayName("SurName")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100")]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
