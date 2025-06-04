using Npgsql;
using System;
using System.Collections.Generic;
using WaitListApp.Models;
using WaitListApp.Data;

namespace WaitListApp.Repositories
{
    public class WaitlistRepository
    {
        public List<WaitListModel> GetAll()
        {
            var waitlist = new List<WaitListModel>();

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, name, email, phoneno, status, intime, outtime, date FROM waitlist ORDER BY id";

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
                            Status = reader.GetString(4),
                            InTime = reader.GetTimeSpan(5),
                            OutTime = reader.IsDBNull(6) ? (TimeSpan?)null : reader.GetTimeSpan(6),
                            Date = reader.GetDateTime(7)
                        });
                    }
                }
            }

            return waitlist;
        }

        public void Add(WaitListModel entry)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                // ✅ Set current time and date ONLY for add
                var now = DateTime.Now;
                entry.InTime = now.TimeOfDay;
                entry.Date = now.Date;

                string query = @"INSERT INTO waitlist (name, email, phoneno, status, intime, outtime, date) 
                         VALUES (@name, @email, @phone, @status, @intime, @outtime, @date)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", entry.Name);
                    cmd.Parameters.AddWithValue("email", entry.Email);
                    cmd.Parameters.AddWithValue("phone", entry.PhoneNo);
                    cmd.Parameters.AddWithValue("status", entry.Status);
                    cmd.Parameters.AddWithValue("intime", entry.InTime);
                    cmd.Parameters.AddWithValue("outtime", entry.OutTime.HasValue ? (object)entry.OutTime.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("date", entry.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(WaitListModel updated)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    // Always update these fields
                    cmd.CommandText = @"
                UPDATE Waitlist
                SET Name = @Name,
                    Email = @Email,
                    PhoneNo = @PhoneNo,
                    Status = @Status,
                    InTime = @InTime,
                    Date = @Date
                WHERE Id = @Id;
            ";

                    cmd.Parameters.AddWithValue("@Name", updated.Name);
                    cmd.Parameters.AddWithValue("@Email", updated.Email);
                    cmd.Parameters.AddWithValue("@PhoneNo", updated.PhoneNo);
                    cmd.Parameters.AddWithValue("@Status", updated.Status);
                    cmd.Parameters.AddWithValue("@InTime", updated.InTime);
                    cmd.Parameters.AddWithValue("@Date", updated.Date);
                    cmd.Parameters.AddWithValue("@Id", updated.Id);

                    cmd.ExecuteNonQuery();
                }

                // Now handle OutTime conditionally
                bool shouldUpdateOutTime =
                    (updated.OriginalStatus == "Waiting" &&
                        (updated.Status == "Completed" || updated.Status == "Cancelled")) ||
                    updated.OutTime != updated.OriginalOutTime;

                if (shouldUpdateOutTime)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    UPDATE Waitlist
                    SET OutTime = @OutTime
                    WHERE Id = @Id;
                ";

                        cmd.Parameters.AddWithValue("@OutTime", updated.OutTime ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Id", updated.Id);
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }


        public void Delete(int id)
        {
            using (var conn = DbConnection.GetConnection())
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
