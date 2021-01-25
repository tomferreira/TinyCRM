using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "{0} is required"), StringLength(80)]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required"), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
