using System.Threading.Tasks;

namespace Medium.ReplacingIfElse.Application.CommandHandlers {
    // Note: I've purposely not implemented an IQueryHandlerAsync<TQuery>.
    //       It's easy, so I'll let you figure out how to do this by yourself.
    
    /// <summary>
    ///     Handle a command sent to the <see cref="CommandDispatcher"/>. <br />
    ///     This interface should only be applied once to a class.
    /// </summary>
    /// <typeparam name="TCommand">The type the handler should be invoked on.</typeparam>
    public interface ICommandHandlerAsync<in TCommand> where TCommand : class {
        Task HandleAsync(TCommand command);
    }
}