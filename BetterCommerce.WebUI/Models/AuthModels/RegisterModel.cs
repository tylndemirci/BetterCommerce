using System.ComponentModel.DataAnnotations;

namespace BetterCommerce.WebUI.Models.AuthModels
{
    public class RegisterModel
    {
        

        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string CheckPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }
}