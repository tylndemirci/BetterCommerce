namespace BetterCommerce.Entity.Dtos
{
    public class UserForChangePassword
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordToCheck { get; set; }
        
    }
}