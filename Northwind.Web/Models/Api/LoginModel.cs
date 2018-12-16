using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.Models.Api {
    public class LoginModel {
        [Required]
        public string UserName;
        [Required]
        public string Password;
        public bool RememberMe;
    }
}