using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleAppDI
{
    public static class Program
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            SetupConfiguration();
            var serviceCollection = ConfigureServices();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Get from ServiceCollection the instance of MyService (that implements IMyService)
            var myService = _serviceProvider.GetService<IMyService>();

            // Run my task
            await myService.RunTaskAsync();
        }

        public static void SetupConfiguration()
        {
            //Setup appsettings configurations
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                // The file has to have <CopyToOutputDirectory>Always</CopyToOutputDirectory> on .csproj
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services
                //Building default console logging so I can use with ILogger
                //Also setting minimum log level to Debug so we can see it being called by Repository as well
                .AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug)) 
                .AddSingleton(_configuration) // Adding my configuration from SetupConfiguration()
                //Add to ServiceCollection all my services, repositories, etc.
                .AddTransient<IMyService, MyService>() 
                .AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
