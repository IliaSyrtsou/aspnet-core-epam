using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.Models.Api {
    public class RegisterModel {
        [Required]
        public string UserName;
        [Required]
        public string Email;
        [Required]
        public string Password;
    }
}