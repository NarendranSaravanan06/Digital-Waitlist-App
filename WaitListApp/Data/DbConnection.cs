using MaterialDesignThemes.Wpf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WaitListApp.Data
{
    internal class DbConnection
    {

            private static string connStr = "Host=localhost;Username=postgres;Password=admin@eagle;Database=waitListApp";

            public static NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection(connStr);
            }
        }
    

}
