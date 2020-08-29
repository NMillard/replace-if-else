using System.Threading.Tasks;

namespace Medium.ReplacingIfElse.Application.CommandHandlers {
    public interface ICommandHandlerAsync<TCommand> where TCommand : class {
        Task HandleAsync(TCommand command);
    }
}