using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class UserDAL
{
    private string? query;
    private MySqlDataReader? reader;

    public User GetUserByEmail(string email)
    {
        bool success = true;
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
            success = false;
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected errors occurred! Please return to the main menu.");
        }

        if (success != false)
        {
            Console.Clear();
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
        catch{}
        
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
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters["@Name"].Direction = System.Data.ParameterDirection.Input;
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
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters["@Name"].Direction = System.Data.ParameterDirection.Input;
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
        user.Name = reader.GetString("Name");
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
        catch (Exception){}
        

        DBHelper.CloseConnection();

        return RecruiterID;
    }
}
