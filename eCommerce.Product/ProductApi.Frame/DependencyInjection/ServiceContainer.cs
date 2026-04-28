using eCommerce.SharedLibary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Frame.Data;
using ProductApi.Frame.Repositories;

namespace ProductApi.Frame.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddFrameService(this IServiceCollection services,IConfiguration config)
        {
            //Add database connectivity
            //Add authentication scheme
           SharedServiceContainer.AddSharedServices<ProductDbContext>(services, config, config["MySerilog:FileName"]!);

            //Create Dependency Injection
            services.AddScoped<IProduct, ProductRepository>();
            
            return services;
        }

        public static IApplicationBuilder UseFramePolcy(this IApplicationBuilder app)
        {
            //Register middle such as :
            //Global Exception : handles external errors.
            //Listen to Only Api Getway: blocks all outsider calls
            SharedServiceContainer.UseSharedPolicies(app);
           
            
            return app;
        }

    }
}
