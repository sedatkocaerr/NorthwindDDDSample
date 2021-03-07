using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.Authentication.Request
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "Email is Required.")]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Length must be between 10 to 100")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 100")]
        public string Password { get; set; }
    }
}
