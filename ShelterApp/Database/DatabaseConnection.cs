using System;
using Npgsql;

namespace ShelterApp.Database
{
    public class DatabaseConnection
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=shelter_db;Username=postgres;Password=postgres";

        public static void SetConnectionString(string connString)
        {
            connectionString = connString;
        }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
