using MySql.Data.MySqlClient;
using System.IO;

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
                ConnectionString = @"server=localhost; user=guest; password=123456; database=project;"
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
        
        try
        {
            connection!.Open();
        }
        catch{
            connection = null;
        }

        
        return connection!;
    }

    public static void CloseConnection()
    {
        if (connection != null)
        {
            connection.Close();
        }
    }
}