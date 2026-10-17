using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace QuanLySinhVien.Data.DAL
{
    public class DataHelper
    {
        public DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader(string sql, params MySqlParameter[] parameters)
        {
            var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public object? ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd.ExecuteScalar();
        }
    }
}
