using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;


namespace StatlerWaldorfCorp.LocationService
{
    public class Startup
    {
        public static string[] Args { get; set; } = new string[]{};
        private ILogger logger;
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json", optional:true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);

            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;
            this.loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();

            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var transient = true;
            if(Configuration.GetSection("transient") != null)
            {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }

            if(transient)
            {
                logger.LogInformation("Using transient location record repository");
                services.AddScoped<ILocationRecordRepositary, InMMemoryLocationRecordRepository>();

            }
            else
            {
                // var connectionString = Configuration.GetSection("postgres:cstr").Value;
                // services.AddEntintyFrameworkNpgsql().AddDebContext<LocationDBcontext>

                logger.LogInformation("LocationDBContext not setup yet using Transient");
                services.AddScoped<ILocationRecordRepositary, InMMemoryLocationRecordRepository>();

            }

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}