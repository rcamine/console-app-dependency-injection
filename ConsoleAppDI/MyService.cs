using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    public class MyService : IMyService
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MyService> _logger;

        //This will be taken from ServiceCollection from Program.cs
        public MyService(IRepository repository, 
            IConfiguration configuration, 
            ILogger<MyService> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task RunTaskAsync()
        {
            var somethingFromConfigFile = _configuration.GetValue<string>("Whatever");
            _logger.LogInformation("Printing result from config file: {0}", somethingFromConfigFile);
            await _repository.DoSomethingAsync();
        }
    }
}