using MySql.Data.MySqlClient;

namespace DAL;

public class DBHelper
{
    private static MySqlConnection? connection;

    public static MySqlConnection GetConnection()
    {
        if (connection == null)
        {
            connection = new MySqlConnection
            {
                ConnectionString = @"server=localhost; user=root; password=36558136; database=project;" // Change password according to your MySQL password
            };
        }

        return connection;
    }

    public static MySqlDataReader ExecQuery(string query)
    {
        MySqlCommand sqlCommand = new MySqlCommand(query, connection);
        return sqlCommand.ExecuteReader();
    }

    public static MySqlConnection OpenConnection()
    {
        if (connection == null)
        {
            GetConnection();
        }
        
        connection!.Open();
        
        return connection;
    }

    public static void CloseConnection()
    {
        if (connection != null)
        {
            connection.Close();
        }
    }
}