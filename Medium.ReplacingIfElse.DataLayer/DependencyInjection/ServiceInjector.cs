using System;
using Medium.ReplacingIfElse.Application.Interfaces.repositories;
using Medium.ReplacingIfElse.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.DataLayer.DependencyInjection {
    public static class ServiceInjector {
        public static IServiceCollection AddApplicationPersistence(this IServiceCollection services, string connectionString) {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException(connectionString);

            services.AddDbContext<DataStore>(config => config.UseSqlServer(connectionString));
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}