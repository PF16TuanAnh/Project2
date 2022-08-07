using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class UserDAL
{
    private string? query;
    private MySqlDataReader? reader;

    public User GetUserByEmail(string email)
    {
        
        query = @"select * from Users where Email = '" + email + "'";
        User user = null!;

        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            
            if (reader.Read())
            {
                user = GetUserInfo(reader);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected errors occurred to the connection to the database! Couldn't retrieve user login info.");
        }
        

        DBHelper.CloseConnection();

        return user;
    }

    public int? GetCandidateIDByEmail(string email)
    {
        query = @"select c.CandidateID from Candidates c inner join Users u on c.UserID = u.UserID where Email = '" + email + "'";
        int? CandidateID = null;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            if (reader.Read())
            {
                CandidateID = reader.GetInt32("CandidateID");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected errors occurred to the connection to the database! Closing out.");
            System.Environment.Exit(0);
        }
        

        DBHelper.CloseConnection();

        return CandidateID;
    }

    public int? InsertNewCandidate(User user)
    {
        int? CandidateID = null;

        MySqlCommand cmd = new MySqlCommand("sp_AddCandidate", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters["@Username"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@UserPassword", user.Password);
            cmd.Parameters["@UserPassword"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters["@Gender"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CandidateID", MySqlDbType.Int32);
            cmd.Parameters["@CandidateID"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            CandidateID = (int) cmd.Parameters["@CandidateID"].Value;
        }
        catch {}
        finally
        {
            DBHelper.CloseConnection();
        }
        
        return CandidateID;
    }

    public int? InsertNewRecruiter(User user)
    {
        int? RecruiterID = null;

        MySqlCommand cmd = new MySqlCommand("sp_AddRecruiter", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters["@Username"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@UserPassword", user.Password);
            cmd.Parameters["@UserPassword"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters["@Gender"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@RecruiterID", MySqlDbType.Int32);
            cmd.Parameters["@RecruiterID"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            RecruiterID = (int) cmd.Parameters["@RecruiterID"].Value;
        }
        catch {}
        finally
        {
            DBHelper.CloseConnection();
        }
        
        return RecruiterID;
    }

    private User GetUserInfo(MySqlDataReader reader)
    {
        User user = new User();
        user.UserID = reader.GetInt32("UserID");
        user.Username = reader.GetString("Username");
        user.Password = reader.GetString("UserPassword");
        user.Email = reader.GetString("Email");
        user.Gender = reader.GetString("Gender");

        return user;
    }

    //NCT 
    public int? GetRecruiterIDByEmail(string email)
    {
        query = @"select c.RecruiterID from Recruiters c inner join Users u on c.UserID = u.UserID where Email = '" + email + "'";
        int? RecruiterID = null;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            if (reader.Read())
            {
                RecruiterID = reader.GetInt32("RecruiterID");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected errors occurred to the connection to the database! Closing out.");
            System.Environment.Exit(0);
        }
        

        DBHelper.CloseConnection();

        return RecruiterID;
    }

    public string? GetRecruiterUsernameByEmail(string email)
    {
        query = @"select u.Username from Recruiters c inner join Users u on c.UserID = u.UserID where Email = '" + email + "'";
        string? Username = null;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            if (reader.Read())
            {
                Username = reader.GetString("Username");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected errors occurred to the connection to the database! Closing out.");
            System.Environment.Exit(0);
        }
        

        DBHelper.CloseConnection();

        return Username;
    }
}
