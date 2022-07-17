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

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        User user = null!;
        if (reader.Read())
        {
            user = GetUserInfo(reader);
        }

        DBHelper.CloseConnection();

        return user;
    }

    public int? GetCandidateIDByEmail(string email)
    {
        query = @"select c.CandidateID from Candidates c inner join Users u on c.UserID = u.UserID where Email = '" + email + "'";

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        int? CandidateID = null;
        if (reader.Read())
        {
            CandidateID = reader.GetInt32("CandidateID");
        }

        DBHelper.CloseConnection();

        return CandidateID;
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
}
