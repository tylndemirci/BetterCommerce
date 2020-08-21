using System.ComponentModel.DataAnnotations;

namespace BetterCommerce.Entity.Entities
{
    public class Address
    {
        [Required] public string UserId { get; set; }
        [Required] public string AddressBar { get; set; }
        [Required] public string Country { get; set; }
        [Required] public string City { get; set; }
        [Required] public string District { get; set; }    
        [Required] public string AddressTitle { get; set; }
        [Required] public string Phone { get; set; }
    }
}