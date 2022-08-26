using Xunit;
using Persistence;
using BL;
using DAL;
using System;
using MySql.Data.MySqlClient;

namespace TestBL;

public class UnitTestUserBL : IDisposable
{
    public UserBL userBL = new UserBL();

    public void Dispose()
    {
        string insertEmail = "User011@gmail.com";
        insertEmail = insertEmail.ToUpper();
        MySqlCommand cmd = new MySqlCommand("sp_DeleteTestData_InsertNewCandidate", DBHelper.OpenConnection());
        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", userBL.GetHashString(insertEmail));
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
            cmd.Parameters.AddWithValue("@Email", userBL.GetHashString(insertEmail));
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
    public void PassingVerifyEmailAndPassword() //TC09
    {
        var result = userBL.VerifyEmailAndPassword("User001@gmail.com", "12345");
        Assert.IsType<User>(result);
    }
    [Fact]
    public void FailingVerifyEmail_not_found() //TC10
    {
        Assert.False(userBL.VerifyEmailAndPassword ("User001@gmail.com", "12345"));
    }
    [Fact]
    public void FailingVerifyEmail_null() //TC10
    {
        Assert.False(userBL.VerifyEmailAndPassword("", "12345"));
    }
    [Fact]
    public void FailingVerifyEmail_malformed() //TC10
    {
        Assert.False(userBL.VerifyEmailAndPassword("User001gmail.com", "12345"));
    }
    [Fact]
    public void FailingVerifyPassword_null() //TC10
    {
        Assert.False(userBL.VerifyEmailAndPassword("User001@gmail.com", ""));
    }
    [Fact]
    public void FailingVerifyPassword_wrong() //TC10
    {
        Assert.False(userBL.VerifyEmailAndPassword("User001@gmail.com", "123456"));
    }

    [Fact]
    public void PassingGetCandidateIDByEmail() //TC11
    {
        Assert.IsType<int>(userBL.GetCandidateIDByEmail("User001@gmail.com "));
    }
    [Fact]
    public void FailingGetCandidateIDByEmail_not_found () //TC12
    {
        Assert.Null(userBL.GetCandidateIDByEmail("User000@gmail.com"));
    }
    [Fact]
    public void FailingGetCandidateIDByEmail_null() //TC12
    {
        Assert.Null(userBL.GetCandidateIDByEmail(""));
    }
    [Fact]
    public void FailingGetCandidateIDByEmail_malformed() //TC12
    {
        Assert.Null(userBL.GetCandidateIDByEmail("User001gmail.com"));
    }

    [Fact]
    public void PassingInsertNewUser_Candidate() //TC13
    {
        Assert.IsType<int>(userBL.InsertNewUser(new User("User011", " User011@gmail.com", "12345", "Male"), 1));
    }
    [Fact]
    public void PassingInsertNewUser_Recruiter() //TC13
    {
        Assert.IsType<int>(userBL.InsertNewUser(new User("User016", " User016@gmail.com", "12345", "Male"), 2));
    }

    [Fact]
    public void FailingInsertNewUser_already_exist () //TC14
    {
        Assert.Null(userBL.InsertNewUser(new User("User001", " User001@gmail.com", "12345", "Male"), 1));
    }
    [Fact]
    public void FailingInsertNewUser_null() //TC14
    {
        Assert.Null(userBL.InsertNewUser(new User("User011", "", "12345", "Male"), 1));
    }
    [Fact]
    public void FailingInsertNewUser_malformed() //TC14
    {
        Assert.Null(userBL.InsertNewUser(new User("User011", " User011gmail.com", "12345", "Male"), 1));
    }

    [Fact]
    public void PassingGetRecruiterIDByEmail() //TC93
    {
        Assert.Equal(1, userBL.GetRecruiterIDByEmail("User006@gmail.com"));
        // Assert.NotEqual(2, userDAL.GetCandidateIDByEmail("User006@gmail.com"));
    }
    [Fact]
    public void FailingGetRecruiterIDByEmail_notfound() //TC94
    {
        Assert.Null(userBL.GetRecruiterIDByEmail("User000@gmail.com"));
    }
    [Fact]
    public void FailingGetRecruiterIDByEmail_malformed()//TC94
    {
        Assert.Null(userBL.GetRecruiterIDByEmail("User006gmail.com"));
    }
    [Fact]
    public void FailingGetRecruiterIDByEmail_null()//TC94
    {
        Assert.Null(userBL.GetRecruiterIDByEmail(""));
    }
    
    [Fact]
    public void PassingCheckIfEmailExisted() //TC95
    {
        var result = userBL.CheckIfEmailExisted("User001@gmail.com");
        Assert.IsType<User>(result);
    }
    [Fact]
    public void FailingCheckIfEmailExisted_null() //TC96
    {
        Assert.False(userBL.CheckIfEmailExisted(""));
    }
}