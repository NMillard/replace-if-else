using System.Threading.Tasks;

namespace Medium.ReplacingIfElse.Application.Commands {
    public interface ICommandAsync<TInput> {
        public Task ExecuteAsync(TInput input);
    }
}