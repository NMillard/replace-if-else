using System;

namespace Medium.ReplacingIfElse.Domain {

    public class User {
        private Guid id;

        public User(Guid id, string email) {
            this.id = id;
            Email = email;
            
            // Let's just assume the email is also the username
            // when a new user is instantiated.
            Username = email;
        }

        public string Id => id.ToString();
        
        // We could have made an Email class
        // and username class, given that
        // we'd need some domain logic.
        public string Email { get; private set; }
        public string Username { get; private set; }

        public Address Address { get; private set; }

        public void ChangeUsername(string newUsername) {
            if (string.IsNullOrEmpty(newUsername)) return;
            Username = newUsername;
        }
        
        public void ChangeEmail(string email) {
            if (string.IsNullOrEmpty(email)) return;
            // Do some checks and whatnot
            
            this.Email = email;
        }

        public void ChangeAddress(Address address) {
            if (address is null) return;
            this.Address = address;
        }
    }
}