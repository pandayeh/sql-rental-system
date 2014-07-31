using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    class SQLiteHelper
    {
        private string dbfile;
        private SQLiteConnection con;
        public SQLiteHelper() {
            dbfile = @"Data Source=C:\Users\Panda\Source\Repos\sql-rental-system\Rental\Rental.sqlite";

        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            //try
            //{
                con = new SQLiteConnection(dbfile);
                con.Open();
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = sql;
                SQLiteDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            //    con.Close();
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Failed to get table.");
            //}
            return dt;
        }

        public string ExecuteScalar(string sql)
        {
            con = new SQLiteConnection(dbfile);
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = sql;
            object value = command.ExecuteScalar();
            con.Close();
            if (value != null)
                return value.ToString();
            return "";

        }

        public int ExecuteNonQuery(string sql)
        {
            con = new SQLiteConnection(dbfile);
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = sql;
            int rowsUpdated = command.ExecuteNonQuery();
            con.Close();
            return rowsUpdated;
        }

    }
}
