using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required, StringLength(80)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
