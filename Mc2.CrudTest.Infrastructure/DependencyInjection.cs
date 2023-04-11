﻿using AutoMapper;
using Mc2.CrudTest.Application.Common.Error;
using Mc2.CrudTest.Application.Common.Interface;
using Mc2.CrudTest.Application.Services;
using Mc2.CrudTest.Application.Services.Interface;
using Mc2.CrudTest.Infrastructure.LogCapture;
using Mc2.CrudTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mc2.CrudTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("AppDatabase"),
               b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)), ServiceLifetime.Scoped);
            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IErrorMessageLog, ErrorMessageLog>();
            //services.AddScoped<IConfigurationExtension, ConfigurationExtensions>();
            //services.AddScoped<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
