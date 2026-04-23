using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.Repositories;
using eCommerce.SharedLibary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationApi.Frame.DependencyInjection
{
   public static class ServiceContainer
    {
        public static IServiceCollection AddFrameService(this IServiceCollection services, IConfiguration config)
        {
            //Add database connectivity
            // JWT Add Authentication Scheme
            SharedServiceContainer.AddSharedServices<AuthenticationDbContext>(services, config, config["MySerilog:FileName"]!);

            //Create Dependency Injection
            services.AddScoped<IUser, UserRepository>();
          
            return services;

        }
        public static IApplicationBuilder UserFramePolicy(this IApplicationBuilder app)
        {
            //Register middleware such as:
            //Global Exception : Handle external errors.
            //Listen Only to Api Getway : Block all outsiders call
            SharedServiceContainer.UseSharedPolicies(app);
          
            return app;
        }
    }
}
