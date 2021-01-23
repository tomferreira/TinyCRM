using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TinyCRM.Domain.Entities.NaturalPerson;

namespace TinyCRM.Application.ViewModels.NaturalPerson
{
    public class NaturalPersonViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$")]
        public string IdDocument { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }

        [Required]
        public GenderType? Gender { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
