using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.ViewModels.LegalPerson
{
    public class LegalPersonViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string TradeName { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$")]
        public string IdDocument { get; set; }

        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
