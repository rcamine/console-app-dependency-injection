using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleAppDI
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger)
        {
            _logger = logger;
        }

        public async Task DoSomethingAsync()
        {
            _logger.LogDebug("Method DoSomethingAsync() was called.");
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}