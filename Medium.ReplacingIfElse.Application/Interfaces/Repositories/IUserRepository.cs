using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.Interfaces.Repositories {
    public interface IUserRepository {
        Task<IAsyncEnumerable<User>> GetUsersAsync();
        Task<User?> FindByEmailAsync(string email);
        Task<bool> UpdateAsync(User user);
    }
}