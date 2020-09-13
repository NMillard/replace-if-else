using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace Medium.ReplacingIfElse.Messaging { 
    public class NullMessaging : IMessaging {
        private readonly ILogger<NullMessaging> logger;

        public NullMessaging(ILogger<NullMessaging> logger) {
            this.logger = logger;
        }
        
        public Task SendAsync(object value, string queueName) {
            logger.LogInformation($"Received {value}");
            logger.LogInformation($"{nameof(SendAsync)} invoked. No message queued.");
            return Task.CompletedTask;
        }
    }
}