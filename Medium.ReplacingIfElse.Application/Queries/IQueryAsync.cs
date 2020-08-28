using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.Queries {

    public interface IQueryAsync<TReturn> {
        public Task<IAsyncEnumerable<User>> ExecuteAsync();
    }
    
    public interface IQueryAsync<in TInput, TReturn> {
        public Task<TReturn> ExecuteAsync(TInput input);
    }
}