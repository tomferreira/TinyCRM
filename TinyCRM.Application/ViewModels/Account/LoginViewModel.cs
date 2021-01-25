using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "{0} is required"), StringLength(80)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required"), StringLength(150), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
