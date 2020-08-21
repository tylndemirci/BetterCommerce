using BetterCommerce.Core.Identity;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetterCommerce.DataAccess
{
    public class BetterCommerceContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BetterCommerceContext(DbContextOptions<BetterCommerceContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        
    }
}