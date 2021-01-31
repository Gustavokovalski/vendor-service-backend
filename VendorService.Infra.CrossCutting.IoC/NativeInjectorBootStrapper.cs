using Microsoft.Extensions.DependencyInjection;
using VendorService.Application.Services.Interfaces;
using VendorService.Application.Validators;
using VendorService.Domain.Repositories;
using VendorService.Infra.Data.Repositories;

namespace VendorService.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();

            services.AddScoped<LoginModelValidator, LoginModelValidator>();
            services.AddScoped<UserRegisterModelValidator, UserRegisterModelValidator>();
            services.AddScoped<ProductModelValidator, ProductModelValidator>();
            services.AddScoped<SalesOrderModelValidator, SalesOrderModelValidator>();

        }
    }
}
