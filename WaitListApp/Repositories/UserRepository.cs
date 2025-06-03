using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaitListApp.Data;
using Npgsql;
namespace WaitListApp.Repositories
{
    internal class UserRepository
    {
        public bool IsRegistered(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            var conn = Data.DbConnection.GetConnection();
            conn.Open();

            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
            var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            var result = (long)cmd.ExecuteScalar(); // returns count

            if (result > 0)
            {
                string query2 = "UPDATE users SET isloggedin = TRUE WHERE username = @username";
                var cmd2 = new NpgsqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("username", username);
                cmd2.ExecuteNonQuery();

                Task.Delay(TimeSpan.FromMinutes(1)).ContinueWith(_ =>
                {
                    var conn2 = Data.DbConnection.GetConnection();
                    conn2.Open();
                    string logoutQuery = "UPDATE users SET isloggedin = FALSE WHERE username = @username";
                    var logoutCmd = new NpgsqlCommand(logoutQuery, conn2);
                    logoutCmd.Parameters.AddWithValue("username", username);
                    logoutCmd.ExecuteNonQuery();
                });
            }

            return result > 0;
        }

        public bool isloggedIn()
        {
            var conn = Data.DbConnection.GetConnection();
            conn.Open();

            string query = "SELECT isloggedin FROM users WHERE username = @username";
            var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("username", "admin");

            var result = cmd.ExecuteScalar(); // returns count
            return result != null && result is bool isLoggedIn && isLoggedIn;
        }
    }
}
