using System;

namespace Core {
    public class Username {
        public Username(string username) {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("Must have a value");
            if (username.Length > 50) throw new ArgumentException("Cannot exceed 50 characters");

            Value = username;
        }

        public string Value { get; }

        public static implicit operator Username(string value) => new Username(value);
    }
}