using MySql.Data.MySqlClient;

namespace RentalMobilAdminApp
{
    public class Database
    {
        private string connectionString = "server=localhost;user=root;password='';database=db_rentalmobil;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
