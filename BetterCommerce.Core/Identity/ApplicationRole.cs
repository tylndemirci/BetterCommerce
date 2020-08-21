using System;
using System.Collections.Generic;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;

namespace BetterCommerce.Core.Identity
{
    public class ApplicationRole: IdentityRole
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}