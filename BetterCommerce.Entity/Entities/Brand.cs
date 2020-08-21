namespace BetterCommerce.Entity.Entities
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}