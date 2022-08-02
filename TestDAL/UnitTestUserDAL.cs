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
    public void PassingGetUserByEmail()
    {
        var result = userDAL.GetUserByEmail("User001@gmail.com");
        Assert.IsType<User>(result);
        Assert.Equal(1, result.UserID);
    }

    [Fact]
    public void FailingGetUserByEmail()
    {
        Assert.Null(userDAL.GetUserByEmail("User000@gmail.com"));
    }

    [Fact]
    public void PassingGetCandidateIDByEmail()
    {
        Assert.Equal(1, userDAL.GetCandidateIDByEmail("User001@gmail.com"));
        Assert.NotEqual(2, userDAL.GetCandidateIDByEmail("User001@gmail.com"));
    }

    [Fact]
    public void FailingGetCandidateIDByEmail()
    {
        Assert.Null(userDAL.GetCandidateIDByEmail("User006@gmail.com"));
    }

    [Fact]
    public void PassingInsertNewCandidate()
    {
        Assert.IsType<int>(userDAL.InsertNewCandidate(new User("User011", "User011@gmail.com", "12345", "Male")));
    }

    [Fact]
    public void FailingInsertNewCandidate()
    {
        Assert.Null(userDAL.InsertNewCandidate(new User("User001", "User001@gmail.com", "12345", "Male")));
    }

    [Fact]
    public void PassingInsertNewRecruiter()
    {
        Assert.IsType<int>(userDAL.InsertNewRecruiter(new User("User011", "User011@gmail.com", "12345", "Male")));
    }

    [Fact]
    public void FailingInsertNewRecruiter()
    {
        Assert.Null(userDAL.InsertNewRecruiter(new User("User001", "User001@gmail.com", "12345", "Male")));
    }
}