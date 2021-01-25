using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TinyCRM.Application.ViewModels.Account
{
    public class ProfileViewModel
    {
        [Display(Name = "CurrentIdiom")]
        [Required(ErrorMessage = "{0} is required")]
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}
