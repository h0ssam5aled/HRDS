using System.ComponentModel.DataAnnotations;

namespace HRDS.Web.Areas.Security.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}