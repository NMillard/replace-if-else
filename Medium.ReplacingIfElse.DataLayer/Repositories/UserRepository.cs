using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Interfaces.Repositories;
using Medium.ReplacingIfElse.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medium.ReplacingIfElse.DataLayer.Repositories {
    internal class UserRepository : IUserRepository {
        private readonly DataStore dataStore;

        public UserRepository(DataStore dataStore) {
            this.dataStore = dataStore;
        }
        
        public async Task<IAsyncEnumerable<User>> GetUsersAsync() {
            return dataStore.Users.AsAsyncEnumerable();
        }

        public async Task<User?> FindByEmailAsync(string email) {
            return await dataStore.Users.SingleOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> UpdateAsync(User user) {
             dataStore.Users.Update(user);
             try {
                 await dataStore.SaveChangesAsync();
                 return true;
             } catch (DbUpdateException) {
                 return false;
             }
        }
    }
}