using eCommerce.SharedLibary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using OrderApi.Application.Interfaces;
using OrderApi.Frame.Data;
using OrderApi.Frame.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Frame.DependencyInJection
{
   public static class ServiceContainer
    {
        public static IServiceCollection AddFrameService(this IServiceCollection services, IConfiguration config)
        {
            //Add Database Connectivity
            //Add authentication scheme
            SharedServiceContainer.AddSharedServices<OrderDbContext>(services, config,config["Myserilog:FileName"]!);
            //Create Dependency InJection
            services.AddScoped<IOrder, OrderRepository>();
            return services;
        }

        public static IApplicationBuilder UserFramePolicy(this ApplicationBuilder app)
        {
            //Register middleWare such as:
            //Global Exception -> handle external errors 
            // ListenToApiGate Only -> block all outsider calls
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }

    }
}
