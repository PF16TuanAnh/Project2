using Xunit;
using Persistence;
using DAL;
using System;
using MySql.Data.MySqlClient;

namespace TestDAL;

public class UnitTestUserDAL : IDisposable
{
    public UserDAL userDAL = new UserDAL();

    public void Dispose()
    {
        MySqlCommand cmd = new MySqlCommand("sp_DeleteTestData_InsertNewCandidate", DBHelper.OpenConnection());
        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", "User011@gmail.com");
            cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch{}
        finally
        {
            DBHelper.CloseConnection();
        }

        cmd = new MySqlCommand("sp_DeleteTestData_InsertNewRecruiter", DBHelper.OpenConnection());
        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", "User011@gmail.com");
            cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch{}
        finally
        {
            DBHelper.CloseConnection();
        }
    }

    [Fact]
    public void PassingGetUserByEmail() //TC01
    {
        var result = userDAL.GetUserByEmail("User001@gmail.com");
        Assert.IsType<User>(result);
        Assert.Equal(1, result.UserID);     
    } 
    [Fact]
    public void FailingGetUserByEmail_notfound() //TC02
    {
        Assert.Null(userDAL.GetUserByEmail("User000@gmail.com"));
    }
    [Fact]
    public void FailingGetUserByEmail_malformed() //TC02
    {
        Assert.Null(userDAL.GetUserByEmail("User001gmail.com ") );
    }
    [Fact]
    public void FailingGetUserByEmail_null() //TC02
    {
        Assert.Null(userDAL.GetUserByEmail(""));
    }

    [Fact]
    public void PassingGetCandidateIDByEmail() //TC03
    {
        Assert.Equal(1, userDAL.GetCandidateIDByEmail("User001@gmail.com"));
        // Assert.NotEqual(2, userDAL.GetCandidateIDByEmail("User001@gmail.com"));
    }
    [Fact]
    public void FailingGetCandidateIDByEmail_notfound() //TC04
    {
        Assert.Null(userDAL.GetCandidateIDByEmail("User000@gmail.com"));
    }
    [Fact]
    public void FailingGetCandidateIDByEmail_malformed()//TC04
    {
        Assert.Null(userDAL.GetCandidateIDByEmail("User006gmail.com"));
    }
    [Fact]
    public void FailingGetCandidateIDByEmail_null()//TC04
    {
        Assert.Null(userDAL.GetCandidateIDByEmail(""));
    }

    [Fact]
    public void PassingInsertNewCandidate() //TC05
    {
        Assert.IsType<int>(userDAL.InsertNewCandidate(new User("User011", "User011@gmail.com", "12345", "Male")));
    }
    [Fact]
    public void FailingInsertNewCandidate_already_exists() //TC06
    {
        Assert.Null(userDAL.InsertNewCandidate(new User("User001", "User001@gmail.com", "12345", "Male")));
    }
    [Fact]
    public void FailingInsertNewCandidate_null() //TC06
    {
        Assert.Null(userDAL.InsertNewCandidate(new User("User011", "", "12345", "Male")));
    }
    [Fact]
    public void FailingInsertNewCandidate_malformed() //TC06
    {
        Assert.Null(userDAL.InsertNewCandidate(new User("User011", " User011gmail.com ", "12345", "Male")));
    }

    [Fact]
    public void PassingInsertNewRecruiter() //TC07
    {
        Assert.IsType<int>(userDAL.InsertNewRecruiter(new User("User016", "User016@gmail.com", "12345", "Male")));
    }
    [Fact]
    public void FailingInsertNewRecruiter_already_exists() //TC08
    {
        Assert.Null(userDAL.InsertNewRecruiter(new User("User006", "User006@gmail.com", "12345", "Male")));
    }
    [Fact]
    public void FailingInsertNewRecruiter_null() //TC08
    {
        Assert.Null(userDAL.InsertNewRecruiter(new User("User016", "", "12345", "Male")));
    }
    [Fact]
    public void FailingInsertNewRecruiter_malformed() //TC08
    {
        Assert.Null(userDAL.InsertNewRecruiter(new User("User016", "User016gmail.com", "12345", "Male")));
    }

    [Fact]
    public void PassingGetRecruiterIDByEmail() //TC91
    {
        Assert.Equal(1, userDAL.GetRecruiterIDByEmail("User006@gmail.com"));
        // Assert.NotEqual(2, userDAL.GetCandidateIDByEmail("User006@gmail.com"));
    }
    [Fact]
    public void FailingGetRecruiterIDByEmail_notfound() //TC92
    {
        Assert.Null(userDAL.GetRecruiterIDByEmail("User000@gmail.com"));
    }
    [Fact]
    public void FailingGetRecruiterIDByEmail_malformed()//TC92
    {
        Assert.Null(userDAL.GetRecruiterIDByEmail("User006gmail.com"));
    }
    [Fact]
    public void FailingGetRecruiterIDByEmail_null()//TC92
    {
        Assert.Null(userDAL.GetRecruiterIDByEmail(""));
    }

}