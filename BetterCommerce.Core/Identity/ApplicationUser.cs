using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;

namespace BetterCommerce.Core.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}