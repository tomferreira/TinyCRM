using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.ViewModels.LegalPerson
{
    public class LegalPersonViewModel
    {
        public int Id { get; set; }

        [Display(Name = "LegalPerson.CompanyName")]
        [Required(ErrorMessage = "{0} is required")]
        public string CompanyName { get; set; }

        [Display(Name = "LegalPerson.TradeName")]
        public string TradeName { get; set; }

        [Display(Name = "LegalPerson.IdDocument")]
        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$", ErrorMessage = "{0} format invalid")]
        public string IdDocument { get; set; }

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
