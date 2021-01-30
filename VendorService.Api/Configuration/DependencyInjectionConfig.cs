using Microsoft.Extensions.DependencyInjection;
using System;
using VendorService.Infra.CrossCutting.IoC;

namespace VendorService.Api
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
