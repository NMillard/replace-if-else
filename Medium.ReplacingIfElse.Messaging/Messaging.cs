using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Medium.ReplacingIfElse.Application.Interfaces.Messaging;

namespace Medium.ReplacingIfElse.Messaging {
    public class Messaging : IMessaging {
        private readonly string connectionString;

        public Messaging(string connectionString) {
            this.connectionString = connectionString;
        }
        
        /// <summary>
        /// The object value will get serialized into json. <br />
        /// The value is of type object because we don't really
        /// care about concrete types at this point.
        /// </summary>
        public async Task SendAsync(object value, string queueName) {
            var client = new QueueClient(connectionString, queueName);
            await client.CreateIfNotExistsAsync();
            
            string json = JsonSerializer.Serialize(value);
            await client.SendMessageAsync(json);
        }
    }
}