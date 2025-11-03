using EmailNotifications.Application.Interfaces;
using EmailNotifications.Infrastructure.Data;
using EmailNotifications.Infrastructure.Repositories;
using EmailNotifications.SharedLibrary.DependencyInjection;
using EmailNotifications.SharedLibrary.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            SharedServiceContainer.AddDbContext<ApplicationDbContext>(services, configuration);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.ApplyMigrations<ApplicationDbContext>(app);
            return app;
        }
    }
}
