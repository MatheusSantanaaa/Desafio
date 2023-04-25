﻿using Desafio.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SystemContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
