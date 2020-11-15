using System.Linq;
using BetterCommerce.AdminUI.Models.Products;
using BetterCommerce.Business.Abstract;
using BetterCommerce.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Controllers
{
    public class ProductController : BaseController
    {
       
        public IActionResult ListProducts()
        {
         // var products= _businessService.Product.GetProductList();
         // if (products.Data==null)
         // {
         //     HttpContext.Response.Redirect("/ErrorPage");
         // }
         // var returnModel = products.Data!.Select(x=> new ListProductsModel(x));
         return View();
        }

       
    }
}