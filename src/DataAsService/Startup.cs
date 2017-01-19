using DataAsService.DAL.Configuration;
using DataAsService.DAL.Configuration.Interfaces;
using DataAsService.DAL.Repositories;
using DataAsService.DAL.Repositories.Interfaces;
using DataAsService.Services;
using DataAsService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAsService
{
    public class Startup
    {
        private const string ConnectionStringSectionName = "AgvanceDatabase";

        public Startup(IHostingEnvironment env)
        {
            if (env == null)
            {
                return;
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder?.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add Singletons
            services.AddSingleton<IApplicationContextService>(new ApplicationContextService
            {
                ConnectionString = Configuration.GetConnectionString(ConnectionStringSectionName)
            });

            //Add factories
            services.AddTransient<IConnectionFactory, SqlConnectionFactory>();

            //Add Repositories
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IFinanceRepository, FinanceRepository>();
            services.AddTransient<ISalesRepository, SalesRepository>();

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }
    }
}
