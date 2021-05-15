using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFactoryService<IService, string>, FactoryService<IService, string>>();
            services.AddFactory<IService, string>(new Dictionary<string, Type>
            {
                { nameof(ServiceA), typeof(ServiceA) },
                { nameof(ServiceB), typeof(ServiceB) },
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void AddFactory<TService, TKey>(this IServiceCollection services, Dictionary<TKey, Type> implementations)
            where TService : class
             where TKey : class

        {
            foreach (var item in implementations)
            {
                services.AddTransient(item.Value);
            }
            services.AddSingleton<Func<TKey, TService>>(x => (key) =>
            {
                Type type = implementations[key];
                return x.GetService(type) as TService;
            });
        }
    }
}
