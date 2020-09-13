using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Medium.ReplacingIfElse.WebClient.BackgroundServices {
    /// <summary>
    /// Simulate some service listening to the marketing queue.
    /// </summary>
    public class MarketingSimulationBackgroundService : IHostedService {
        private readonly ILogger<MarketingSimulationBackgroundService> logger;
        private readonly QueueClient client;

        public MarketingSimulationBackgroundService(string connectionString, ILogger<MarketingSimulationBackgroundService> logger) {
            this.logger = logger;
            if (string.IsNullOrEmpty(connectionString)) {
                client = null;
                return;
            }
            
            client = new QueueClient(connectionString, "marketing");
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            if (client is null) {
                logger.LogInformation("will not start. Provide a value for AzureStorageAccount:default.");
                return; // Don't start listening if there's no connection string provided.
            }

            logger.LogInformation("Starting simulation marketing queue listener service.");
            while (!cancellationToken.IsCancellationRequested) {
                Response<QueueMessage[]> messages = await client.ReceiveMessagesAsync(cancellationToken);
                
                foreach (QueueMessage message in messages.Value) {
                    logger.LogInformation(message.MessageText);
                    await client.DeleteMessageAsync(message.MessageId, message.PopReceipt, cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            logger.LogInformation("Marketing service stopping...");
            return Task.CompletedTask;
        }
    }
}