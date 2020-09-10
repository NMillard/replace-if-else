using System.Threading.Tasks;

namespace Medium.ReplacingIfElse.Application.Interfaces.Messaging {
    public interface IMessaging {
        public Task SendAsync(object value, string queueName);
    }
}