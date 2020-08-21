using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Controllers
{
    public class ProductController : BaseController
    {
        
        public IActionResult ListProducts()
        {
         var products= _businessService.Product.GetAllProducts();
         if (products.Data==null)
         {
             HttpContext.Response.Redirect("/ErrorPage");
         }
         //var returnModel = new ProductListModel(products);
         return View();
        }

        public ProductController(IBusinessService businessService, IUnitOfWork unitOfWork) : base(businessService, unitOfWork)
        {
        }
    }
}