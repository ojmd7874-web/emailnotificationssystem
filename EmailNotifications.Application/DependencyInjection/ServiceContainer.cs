using EmailNotifications.Application.Interfaces;
using EmailNotifications.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            return services;
        }
    }
}
