using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace VendorService.Infra.CrossCutting.ApiConfig
{
    public static class ApiConfiguration
    {
        public static void AddWebApiConfiguration(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {

                options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                options.SupportedCultures = new[]
                {
                    new CultureInfo("pt-BR"),
                };

                options.SupportedUICultures = new[]
                {
                    new CultureInfo("pt-BR")
                };
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
