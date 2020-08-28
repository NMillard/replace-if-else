using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.interfaces.repositories;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.Queries.Users {
    public class GetUsers : IQueryAsync<IEnumerable<User>> {
        private readonly IUserRepository repository;

        public GetUsers(IUserRepository repository) {
            this.repository = repository;
        }

        public async Task<IAsyncEnumerable<User>> ExecuteAsync() {
            return await repository.GetUsers();
        }
    }
}