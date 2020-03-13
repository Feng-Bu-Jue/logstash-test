using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace LogStashExemple.API.Infra.Configurations.Extensions.Services
{
    public static class GlobalizationConfigurationExtension
    {
        public static IServiceCollection GlobalizationConfiguration(this IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            return services;
        }
    }
}