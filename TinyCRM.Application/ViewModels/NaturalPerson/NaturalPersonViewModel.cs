using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TinyCRM.Domain.Entities.NaturalPerson;

namespace TinyCRM.Application.ViewModels.NaturalPerson
{
    public class NaturalPersonViewModel
    {
        public int Id { get; set; }

        [Display(Name = "NaturalPerson.Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        
        [Display(Name = "NaturalPerson.IdDocument")]
        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "{0} format invalid")]
        public string IdDocument { get; set; }

        [Display(Name = "NaturalPerson.Birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "NaturalPerson.Gender")]
        [Required(ErrorMessage = "{0} is required")]
        public GenderType? Gender { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Address.ZipCode")]
        public string ZipCode { get; set; }

        [Display(Name = "Address.Country")]
        public string Country { get; set; }

        [Display(Name = "Address.State")]
        public string State { get; set; }

        [Display(Name = "Address.City")]
        public string City { get; set; }

        [Display(Name = "Address.AddressLine1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address.AddressLine2")]
        public string AddressLine2 { get; set; }
    }
}
