using MySql.Data.MySqlClient;

namespace QuanLySinhVien.Data.DAL
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = "Server=localhost;Port=3306;Database=quanlysinhvien;Uid=root;Pwd=phuonglinh3001;Charset=utf8mb4;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
