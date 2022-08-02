using Xunit;
using Persistence;
using BL;
using DAL;
using System;
using MySql.Data.MySqlClient;

namespace TestBL;

public class UnitTestCandidateBL : IDisposable
{
    public CandidateBL candidateBL = new CandidateBL();

    public void Dispose()
    {
        MySqlCommand cmd = new MySqlCommand("delete from CVs where CandidateID = 4", DBHelper.OpenConnection());
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch{}
        finally
        {
            DBHelper.CloseConnection();
        }

        cmd = new MySqlCommand("delete from ApplyCandidates where CandidateID = 1 and NewsID = 2", DBHelper.OpenConnection());
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch{}
        finally
        {
            DBHelper.CloseConnection();
        }
    }

    [Fact]
    public void PassingGetCandidateByID()
    {
        Assert.IsType<Candidate>(candidateBL.GetCandidateByID(1));
    }

    [Fact]
    public void FailingGetCandidateByID()
    {
        Assert.Null(candidateBL.GetCandidateByID(0));
    }

    
    [Fact]
    public void PassingCreateNewCV()
    {
        Assert.IsType<int>(candidateBL.CreateNewCV(new CV("", "", "", "", "321598", "test@gmail.com", "", "", null), 4));
    }

    [Fact]
    public void FailingCreateNewCV()
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", "", "321598", "test@gmail.com", "", "", null), 1));
    }

    [Fact]
    public void PassingUpdateCVInfo()
    {
        CV test = new CV("", "", "", "", "321598", "test@gmail.com", "", "", null);
        test.CVID = 1;
        Assert.True(candidateBL.UpdateCVInfo(test));
        test.CVID = 0;
        Assert.True(candidateBL.UpdateCVInfo(test));
        Assert.True(candidateBL.UpdateCVInfo(new CV("", "", "", "", "321598", "test@gmail.com", "", "", null)));
    }

    [Fact]
    public void PassingInsertToApplyCandidates()
    {
        Assert.True(candidateBL.ApplyToNews(1, 2));
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(null, 1)]
    [InlineData(1, 1)]
    public void FailingApplyToNews(int? CandidateID, int NewsID)
    {
        Assert.False(candidateBL.ApplyToNews(CandidateID, NewsID));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(null, 1)]
    public void PassingIsApplied(int? CandidateID, int NewsID)
    {
        if(CandidateID != null)
        {
            Assert.True(candidateBL.IsApplied(CandidateID, NewsID));
        }
        else
        {
            Assert.False(candidateBL.IsApplied(CandidateID, NewsID));
        }
    }
}