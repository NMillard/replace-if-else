using System;
using Medium.ReplacingIfElse.Application.Interfaces.Repositories;
using Medium.ReplacingIfElse.DataLayer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.DataLayer.DependencyInjection {
    public static class ServiceInjector {
        public static IServiceCollection AddApplicationPersistence(this IServiceCollection services, string connectionString) {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("Connection string to database must be set.");

            services.AddDbContext<DataStore>(config => config.UseSqlServer(connectionString));
            
            // Test if connection works
            var datastore = services.BuildServiceProvider().GetRequiredService<DataStore>();
            try {
                datastore.Database.CanConnect();
            } catch (SqlException) {
                throw new InvalidOperationException("Cannot connect to database with the provided connection string.");
            }
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            // Notice UserRepository is internal and cannot be accessed from outside this project.
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}