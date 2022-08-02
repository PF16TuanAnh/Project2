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
    public void PassingGetUserByEmail()
    {
        var result = userBL.GetUserByEmail("truebk98@gmail.com");
        Assert.IsType<User>(result);
    }

    [Fact]
    public void FailingGetUserByEmail()
    {
        Assert.Null(userBL.GetUserByEmail("User001@gmail.com"));
    }

    [Fact]
    public void PassingGetCandidateIDByEmail()
    {
        Assert.IsType<int>(userBL.GetCandidateIDByEmail("truebk98@gmail.com"));
    }

    [Fact]
    public void FailingGetCandidateIDByEmail()
    {
        Assert.Null(userBL.GetCandidateIDByEmail("User001@gmail.com"));
    }

    [Fact]
    public void PassingInsertNewUser_Candidate()
    {
        Assert.IsType<int>(userBL.InsertNewUser(new User("User011", "User011@gmail.com", "12345", "Male"), 1));
    }

    [Fact]
    public void PassingInsertNewUser_Recruiter()
    {
        Assert.IsType<int>(userBL.InsertNewUser(new User("User011", "User011@gmail.com", "12345", "Male"), 2));
    }

    [Fact]
    public void FailingInsertNewUser()
    {
        Assert.Null(userBL.InsertNewUser(new User("User011", "truebk98@gmail.com", "12345", "Male"), 1));
    }
}