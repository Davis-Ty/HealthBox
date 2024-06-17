using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace SQLiteConnecn
{   
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

        public class Connect
    {
        private readonly string _connectionString;

        public Connect(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
        }

        public void InsertUser(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO user (FirstName, LastName, Username, Email, Password)
                VALUES ($firstName, $lastName, $username, $email, $password)";
            command.Parameters.AddWithValue("$firstName", user.FirstName);
            command.Parameters.AddWithValue("$lastName", user.LastName);
            command.Parameters.AddWithValue("$username", user.Username);
            command.Parameters.AddWithValue("$email", user.Email);
            command.Parameters.AddWithValue("$password", user.Password);

            command.ExecuteNonQuery();
        }
    }
}