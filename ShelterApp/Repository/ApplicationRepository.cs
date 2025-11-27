using System;
using System.Collections.Generic;
using Npgsql;
using ShelterApp.Database;
using ShelterApp.Models;

namespace ShelterApp.Repository
{
    public class ApplicationRepository
    {
        public List<Models.Application> GetByUserId(int userId)
        {
            var applications = new List<Models.Application>();
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"SELECT a.* FROM Application a
                             INNER JOIN Users u ON a.Id_Guardian = u.Id_Guardian
                             WHERE u.Id = @userId
                             ORDER BY a.SubmissionDate DESC";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applications.Add(MapToApplication(reader));
                        }
                    }
                }
            }
            return applications;
        }

        public List<Models.Application> GetAll()
        {
            var applications = new List<Models.Application>();
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM Application ORDER BY SubmissionDate DESC", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        applications.Add(MapToApplication(reader));
                    }
                }
            }
            return applications;
        }

        public void Add(Models.Application application)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Application (Id_Guardian, Id_Animal, SubmissionDate, ApplicationStatus, Id_Employee)
                             VALUES (@guardian, @animal, @date, @status, @employee)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@guardian", application.IdGuardian);
                    cmd.Parameters.AddWithValue("@animal", application.IdAnimal);
                    cmd.Parameters.AddWithValue("@date", application.SubmissionDate);
                    cmd.Parameters.AddWithValue("@status", application.ApplicationStatus ?? "В рассмотрении");
                    cmd.Parameters.AddWithValue("@employee", (object)application.IdEmployee ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStatus(int id, string status)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var query = "UPDATE Application SET ApplicationStatus = @status WHERE Id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Models.Application MapToApplication(NpgsqlDataReader reader)
        {
            return new Models.Application
            {
                Id = reader.GetInt32(0),
                IdGuardian = reader.GetInt32(1),
                IdAnimal = reader.GetInt32(2),
                SubmissionDate = reader.GetDateTime(3),
                ApplicationStatus = reader.IsDBNull(4) ? "В рассмотрении" : reader.GetString(4),
                IdEmployee = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
            };
        }
    }
}
