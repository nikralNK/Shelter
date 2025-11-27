using System;
using Npgsql;
using ShelterApp.Database;
using ShelterApp.Models;

namespace ShelterApp.Repository
{
    public class GuardianRepository
    {
        public Guardian GetById(int id)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM Guardian WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapToGuardian(reader);
                        }
                    }
                }
            }
            return null;
        }

        public int Add(Guardian guardian)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Guardian (Name, Number, Email, Address, GuardianshipDay)
                             VALUES (@name, @number, @email, @address, @day)
                             RETURNING Id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", guardian.Name);
                    cmd.Parameters.AddWithValue("@number", (object)guardian.Number ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", (object)guardian.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", (object)guardian.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@day", (object)guardian.GuardianshipDay ?? DBNull.Value);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Guardian guardian)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Guardian SET Name = @name, Number = @number, Email = @email,
                             Address = @address, GuardianshipDay = @day
                             WHERE Id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", guardian.Name);
                    cmd.Parameters.AddWithValue("@number", (object)guardian.Number ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", (object)guardian.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", (object)guardian.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@day", (object)guardian.GuardianshipDay ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", guardian.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Guardian MapToGuardian(NpgsqlDataReader reader)
        {
            return new Guardian
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Number = reader.IsDBNull(2) ? null : reader.GetString(2),
                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                GuardianshipDay = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
            };
        }
    }
}
