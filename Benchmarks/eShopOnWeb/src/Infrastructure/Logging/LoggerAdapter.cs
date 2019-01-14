using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger; // @issue@I02
        public LoggerAdapter(ILoggerFactory loggerFactory) // @issue@I02
        {
            _logger = loggerFactory.CreateLogger<T>(); // @issue@I02
        }

        public void LogWarning(string message, params object[] args) // @issue@I02
        {
            _logger.LogWarning(message, args); // @issue@I02
        }

        public void LogInformation(string message, params object[] args) // @issue@I02
        {
            _logger.LogInformation(message, args); // @issue@I02
        }
    }
}
