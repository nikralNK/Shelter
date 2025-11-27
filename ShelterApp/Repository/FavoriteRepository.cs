using System;
using System.Collections.Generic;
using Npgsql;
using ShelterApp.Database;
using ShelterApp.Models;

namespace ShelterApp.Repository
{
    public class FavoriteRepository
    {
        public List<Animal> GetFavoritesByUserId(int userId)
        {
            var animals = new List<Animal>();
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"SELECT a.* FROM Animal a
                             INNER JOIN Favorites f ON a.Id = f.Id_Animal
                             WHERE f.Id_User = @userId
                             ORDER BY f.AddedAt DESC";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            animals.Add(MapToAnimal(reader));
                        }
                    }
                }
            }
            return animals;
        }

        public void Add(int userId, int animalId)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Favorites (Id_User, Id_Animal) VALUES (@userId, @animalId)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@animalId", animalId);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (PostgresException)
                    {
                    }
                }
            }
        }

        public void Remove(int userId, int animalId)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Favorites WHERE Id_User = @userId AND Id_Animal = @animalId";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@animalId", animalId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsFavorite(int userId, int animalId)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = "SELECT COUNT(*) FROM Favorites WHERE Id_User = @userId AND Id_Animal = @animalId";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@animalId", animalId);
                    var count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private Animal MapToAnimal(NpgsqlDataReader reader)
        {
            return new Animal
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Type = reader.GetString(2),
                Breed = reader.IsDBNull(3) ? null : reader.GetString(3),
                DateOfBirth = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                IdEnclosure = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                IdGuardian = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                CurrentStatus = reader.IsDBNull(7) ? null : reader.GetString(7),
                Gender = reader.IsDBNull(8) ? null : reader.GetString(8),
                Size = reader.IsDBNull(9) ? null : reader.GetString(9),
                Temperament = reader.IsDBNull(10) ? null : reader.GetString(10),
                Photo1 = reader.IsDBNull(11) ? null : reader.GetString(11),
                Photo2 = reader.IsDBNull(12) ? null : reader.GetString(12),
                Photo3 = reader.IsDBNull(13) ? null : reader.GetString(13)
            };
        }
    }
}
