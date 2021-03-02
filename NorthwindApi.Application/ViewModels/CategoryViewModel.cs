using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Category Name is Required.")]
        [StringLength(maximumLength: 15, ErrorMessage = "Length must be 15 lenght size")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        
        [StringLength(maximumLength: 100, ErrorMessage = "Length max 100 lenght size")]
        [DisplayName("Category Description")]
        public string Description { get; set; }

        [StringLength(maximumLength: 255, ErrorMessage = "Length Max 15 lenght size")]
        [DisplayName("Category Picture")]
        public string Picture { get; set; }
    }
}
