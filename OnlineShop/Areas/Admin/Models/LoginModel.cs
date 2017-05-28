using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}