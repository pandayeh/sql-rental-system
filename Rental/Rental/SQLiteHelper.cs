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
        string dbfile;
        SQLiteConnection con;
        public SQLiteHelper() {
            dbfile = @"Data Source=C:\Users\Panda\Source\Repos\sql-rental-system\Rental\Rental.sqlite";

        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                con = new SQLiteConnection(dbfile);
                con.Open();
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = sql;
                SQLiteDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get table.");
            }
            return dt;
        }

    }
}
