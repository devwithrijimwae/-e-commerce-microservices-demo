using eCommerce.SharedLibary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Frame.Data;
using ProductApi.Frame.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Frame.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddFrameService(this IServiceCollection services,IConfiguration config)
        {
            //Add database connectivity
            //Add authentication scheme
           SharedServiceContainer.AddSharedServices<ProductDbContext>(services, config, config["MySerilog:FineName"]!);

            //Create Dependency Injection
            services.AddScoped<IProduct, ProductRepository>();
            return services;
        }

        public static IApplicationBuilder UseFramePolcies(this IApplicationBuilder app)
        {
            //Register middle such as :
            //Global Exception : handles external errors.
            //Listen to Only Api Getway: blocks all outsider calls
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }

    }
}
