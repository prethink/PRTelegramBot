using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Extensions
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection AddBotHandlers(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var types = ReflectionUtils.FindClassesWithInstanceMethods();
            foreach ( var type in types) 
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}
