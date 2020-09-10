using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Hosting;

namespace Medium.ReplacingIfElse.WebClient.BackgroundServices {
    /// <summary>
    /// Simulate some service listening to the marketing queue.
    /// </summary>
    public class MarketingSimulationBackgroundService : IHostedService {
        private readonly QueueClient client;

        public MarketingSimulationBackgroundService(string connectionString) {
            client = new QueueClient(connectionString, "marketing");
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                Response<QueueMessage[]> messages = await client.ReceiveMessagesAsync(cancellationToken);
                
                foreach (QueueMessage message in messages.Value) {
                    Console.WriteLine(message.MessageText);
                    await client.DeleteMessageAsync(message.MessageId, message.PopReceipt, cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            Console.WriteLine("Marketing service stopping...");
            return Task.CompletedTask;
        }
    }
}