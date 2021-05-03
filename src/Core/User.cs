namespace Core {
    public class User {
        private readonly Username username;

        public User(Username username) {
            this.username = username;
        }

        public string Username => username.Value;
    }
}