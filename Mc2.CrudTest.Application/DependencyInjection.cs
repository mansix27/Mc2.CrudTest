using Mc2.CrudTest.Application.Common.Error;
using Mc2.CrudTest.Application.Services;
using Mc2.CrudTest.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mc2.CrudTest.Application
{
    public static class DependencyInjectin
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}