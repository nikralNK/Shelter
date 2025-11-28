using System;
using System.Collections.Generic;
using Npgsql;
using ShelterApp.Database;
using ShelterApp.Models;

namespace ShelterApp.Repositories
{
    public class AnimalRepository
    {
        public List<Animal> GetAll()
        {
            var animals = new List<Animal>();
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM Animal ORDER BY Id", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        animals.Add(MapToAnimal(reader));
                    }
                }
            }
            return animals;
        }

        public Animal GetById(int id)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM Animal WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapToAnimal(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Animal> GetFiltered(string type, string gender, string size)
        {
            var animals = new List<Animal>();
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Animal WHERE 1=1";

                if (!string.IsNullOrEmpty(type) && type != "Все")
                {
                    query += " AND Type = @type";
                }
                if (!string.IsNullOrEmpty(gender) && gender != "Все")
                {
                    query += " AND Gender = @gender";
                }
                if (!string.IsNullOrEmpty(size) && size != "Все")
                {
                    query += " AND Size = @size";
                }

                query += " ORDER BY Id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(type) && type != "Все")
                        cmd.Parameters.AddWithValue("@type", type);
                    if (!string.IsNullOrEmpty(gender) && gender != "Все")
                        cmd.Parameters.AddWithValue("@gender", gender);
                    if (!string.IsNullOrEmpty(size) && size != "Все")
                        cmd.Parameters.AddWithValue("@size", size);

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

        public void Add(Animal animal)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Animal (Name, Type, Breed, DateOfBirth, Id_Enclosure, Id_Guardian,
                             CurrentStatus, Gender, Size, Temperament, Photo1, Photo2, Photo3)
                             VALUES (@name, @type, @breed, @dob, @enclosure, @guardian, @status, @gender,
                             @size, @temperament, @photo1, @photo2, @photo3)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", animal.Name);
                    cmd.Parameters.AddWithValue("@type", (object)animal.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@breed", (object)animal.Breed ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dob", animal.DateOfBirth);
                    cmd.Parameters.AddWithValue("@enclosure", (object)animal.IdEnclosure ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@guardian", (object)animal.IdGuardian ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", (object)animal.CurrentStatus ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@gender", (object)animal.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@size", (object)animal.Size ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@temperament", (object)animal.Temperament ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@photo1", (object)animal.Photo1 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@photo2", (object)animal.Photo2 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@photo3", (object)animal.Photo3 ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Animal animal)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Animal SET Name = @name, Type = @type, Breed = @breed, DateOfBirth = @dob,
                             Id_Enclosure = @enclosure, Id_Guardian = @guardian, CurrentStatus = @status,
                             Gender = @gender, Size = @size, Temperament = @temperament,
                             Photo1 = @photo1, Photo2 = @photo2, Photo3 = @photo3
                             WHERE Id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", animal.Name);
                    cmd.Parameters.AddWithValue("@type", (object)animal.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@breed", (object)animal.Breed ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dob", animal.DateOfBirth);
                    cmd.Parameters.AddWithValue("@enclosure", (object)animal.IdEnclosure ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@guardian", (object)animal.IdGuardian ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", (object)animal.CurrentStatus ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@gender", (object)animal.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@size", (object)animal.Size ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@temperament", (object)animal.Temperament ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@photo1", (object)animal.Photo1 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@photo2", (object)animal.Photo2 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@photo3", (object)animal.Photo3 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", animal.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM Animal WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Animal MapToAnimal(NpgsqlDataReader reader)
        {
            return new Animal
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Type = reader.IsDBNull(2) ? null : reader.GetString(2),
                Breed = reader.IsDBNull(3) ? null : reader.GetString(3),
                DateOfBirth = reader.IsDBNull(4) ? DateTime.Now : reader.GetDateTime(4),
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
