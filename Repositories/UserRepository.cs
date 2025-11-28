using System;
using Npgsql;
using ShelterApp.Database;
using ShelterApp.Models;

namespace ShelterApp.Repositories
{
    public class UserRepository
    {
        public User GetByUsername(string username)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM Users WHERE Username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2),
                                Email = reader.GetString(3),
                                FullName = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Role = reader.IsDBNull(5) ? "User" : reader.GetString(5),
                                Photo = reader.IsDBNull(6) ? null : reader.GetString(6),
                                IdGuardian = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                                CreatedAt = reader.IsDBNull(8) ? DateTime.Now : reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Add(User user)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Users (Username, PasswordHash, Email, FullName, Role, Photo, Id_Guardian)
                             VALUES (@username, @passwordHash, @email, @fullName, @role, @photo, @guardian)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@fullName", (object)user.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@role", user.Role ?? "User");
                    cmd.Parameters.AddWithValue("@photo", (object)user.Photo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@guardian", (object)user.IdGuardian ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool UsernameExists(string username)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool EmailExists(string email)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @email", conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    var count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void UpdatePassword(string username, string passwordHash)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = "UPDATE Users SET PasswordHash = @passwordHash WHERE Username = @username";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
