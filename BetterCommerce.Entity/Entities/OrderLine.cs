using System.ComponentModel.DataAnnotations;
using BetterCommerce.Entity.Enums;

namespace BetterCommerce.Entity.Entities
{
    public class OrderLine : BaseEntity
    {
        [Required] public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required] public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public double Price { get; set; }
        
        [Required] public EnumOrderLineStatus Status { get; set; }
    }
}