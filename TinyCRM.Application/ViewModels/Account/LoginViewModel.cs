using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required, StringLength(80)]
        public string UserName { get; set; }

        [Required, StringLength(150), DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
