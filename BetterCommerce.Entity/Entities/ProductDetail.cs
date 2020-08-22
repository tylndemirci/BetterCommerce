using System.ComponentModel.DataAnnotations;

namespace BetterCommerce.Entity.Entities
{
    public class ProductDetail: BaseEntity
    {
        [Required] public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
    }
}