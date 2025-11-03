using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.SharedLibrary.DependencyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            services.AddDbContext<TContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

            return services;
        }

        public static void ApplyMigrations<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
        }
    }
}
