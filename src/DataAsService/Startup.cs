using System.IO;
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
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;

namespace DataAsService
{
    /// <summary>
    /// The Startup
    /// </summary>
    public class Startup
    {
        private const string ConnectionStringSectionName = "AgvanceDatabase";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
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

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
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

            // Add the detail information for the API.
            services.ConfigureSwaggerGen(options =>
            {
                options?.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Cognos Data Service",
                    Description = "Web API that provides the data in a format that Cognos would accept",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "MyName", Email = "a@a.com", Url = "http://url.com" },
                    License = new License { Name = "Use under LICX", Url = "http://url.com" }
                });
                
                //Set the comments path for the swagger json and ui.
                options?.IncludeXmlComments(Path.Combine(PlatformServices.Default?.Application?.ApplicationBasePath, "DataAsService.xml"));
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
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
