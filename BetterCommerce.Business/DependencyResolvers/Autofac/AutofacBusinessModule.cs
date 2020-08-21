using Autofac;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Business.Concrete;
using BetterCommerce.Core.Security.Jwt;
using Microsoft.Extensions.Configuration;

namespace BetterCommerce.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        }
    }
}