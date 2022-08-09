using MySql.Data.MySqlClient;
using System.IO;

namespace DAL;

public class DBHelper
{
    private static MySqlConnection? connection;

    public static MySqlConnection GetConnection()
    {
        string? s;
        string path = Directory.GetCurrentDirectory() + @"\ConnectionString.txt";
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(@"server=localhost; user=root; password=Sieunhan159357; database=project;");
            }
        }

        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            s = sr.ReadLine();
        }


        if (connection == null)
        {
            connection = new MySqlConnection
            {
                ConnectionString = s // Change password according to your MySQL password
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