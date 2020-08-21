using System.Linq;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.AdminUI.Models.Products
{
    public class ListProductsModel
    {
        public ListProductsModel(Product product)
        {
            Star = product.Star;
            Price = product.Price;
            Name = product.Name;
            Brand = product.Brand;
            CategoryName = product.Category.Name;
            IsStock = product.IsStock;
            IsInSale = product.IsInSale;
            ImageUrl = product.Images.FirstOrDefault()?.ImageUrl;
        }
        public int Star { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        public bool IsStock { get; set; }
        public bool IsInSale { get; set; }
        public string ImageUrl { get; set; }

    }
}