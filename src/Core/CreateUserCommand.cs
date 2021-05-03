using System;
using System.Threading.Tasks;

namespace Core {
    public class CreateUserCommand {
        public Task<bool> ExecuteAsync(string username) {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("Must have a value");
            if (username.Length > 50) throw new ArgumentException("Cannot exceed 50 characters");

            var user = new User(username);
            // save to database

            return Task.FromResult(true);
        }

        public Task<bool> ExecuteAsync(Username username) {
            var user = new User(username);
            // save to database

            return Task.FromResult(true);
        }
    }
}