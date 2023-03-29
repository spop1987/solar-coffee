using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using SolarCoffee.Data;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Services.Services;
using SolarCoffee.Services.IServices;
using SolarCoffee.Data.Translators;
using SolarCoffee.Data.DataAccess;

namespace SolarCoffee.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services.AddControllers().AddNewtonsoftJson(opts => {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            if(_env.IsDevelopment()){
                Console.WriteLine("--> Using SqlServer DB");
                ConfigureDb(services);
            }else{
                ConfigureDb(services);
                Console.WriteLine("--> Using InMem Db");
                services.AddDbContext<SolarDbContext>(opt => opt.UseInMemoryDatabase("InMen"));
            }

            services.AddTransient<IProductService, ProductService>();
            // services.AddTransient<ICustomerService, CustomerService>();
            // services.AddTransient<IInventoryService, InventoryService>();
            // services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IToEntityTranslator, ToEntityTranslator>();
            services.AddTransient<IToDtoTranslator, ToDtoTranslator>();
            services.AddTransient<ICommands, Commands>();
            services.AddTransient<IQueries, Queries>();
        }

        protected virtual void ConfigureDb(IServiceCollection services)
        {
            services.AddDbContext<SolarDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Solar.dev"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors(
                builder => builder
                    .WithOrigins(
                        "http://localhost:8080", 
                        "http://localhost:8081", 
                        "http://localhost:8082")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
