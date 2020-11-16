using Autofac;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Business.Concrete;
using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.Business.Structure.Concrete;
using BetterCommerce.Core.Security.Jwt;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.DataAccess.Concrete;
using BetterCommerce.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace BetterCommerce.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<EfBaseDal<Category>>().As<IBaseDal<Category>>();
            builder.RegisterType<UnifOfWork>().As<IUnitOfWork>();
            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<EfBaseDal<Order>>().As<IBaseDal<Order>>();
            builder.RegisterType<EfBaseDal<OrderLine>>().As<IBaseDal<OrderLine>>();
            builder.RegisterType<ProductDetailManager>().As<IProductDetailService>();
            builder.RegisterType<EfBaseDal<ProductDetail>>().As<IBaseDal<ProductDetail>>();
            builder.RegisterType<ProductImageManager>().As<IProductImageService>();
            builder.RegisterType<EfBaseDal<ProductImage>>().As<IBaseDal<ProductImage>>();
            builder.RegisterType<EfBaseDal<Product>>().As<IBaseDal<Product>>();
            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<BusinessService>().As<IBusinessService>();





        }
    }
}