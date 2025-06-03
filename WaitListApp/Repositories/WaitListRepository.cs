using Npgsql;
using System;
using System.Collections.Generic;
using WaitListApp.Models;

namespace WaitListApp.Repositories
{
    public class WaitlistRepository
    {
        private string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin@eagle;Database=waitListApp";

        public List<WaitListModel> GetAll()
        {
            var waitlist = new List<WaitListModel>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT id, name, email, phoneno, status FROM waitlist";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        waitlist.Add(new WaitListModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            PhoneNo = reader.GetString(3),
                            Status = reader.GetString(4)
                        });
                    }
                }
            }

            return waitlist;
        }

        public void Add(WaitListModel entry)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO waitlist (name, email, phoneno, status) VALUES (@name, @email, @phone, @status)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", entry.Name);
                    cmd.Parameters.AddWithValue("email", entry.Email);
                    cmd.Parameters.AddWithValue("phone", entry.PhoneNo);
                    cmd.Parameters.AddWithValue("status", entry.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(WaitListModel entry)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE waitlist SET name=@name, email=@email, phoneno=@phone, status=@status WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", entry.Name);
                    cmd.Parameters.AddWithValue("email", entry.Email);
                    cmd.Parameters.AddWithValue("phone", entry.PhoneNo);
                    cmd.Parameters.AddWithValue("status", entry.Status);
                    cmd.Parameters.AddWithValue("id", entry.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM waitlist WHERE id = @id";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }

}
