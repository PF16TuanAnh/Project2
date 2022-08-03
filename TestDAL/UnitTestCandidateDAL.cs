using Xunit;
using Persistence;
using DAL;
using System;
using MySql.Data.MySqlClient;

namespace TestDAL;

public class UnitTestCandidateDAL : IDisposable
{
    public CandidateDAL candidateDAL = new CandidateDAL();

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
        Assert.IsType<Candidate>(candidateDAL.GetCandidateByID(1));
    }

    [Fact]
    public void FailingGetCandidateByID()
    {
        Assert.Null(candidateDAL.GetCandidateByID(0));
    }

    [Fact]
    public void PassingAddNewCV()
    {
        Assert.IsType<int>(candidateDAL.AddNewCV(new CV("", "", "", "", "321598", "test@gmail.com", "", "", null), 4));
    }

    [Fact]
    public void FailingAddNewCV()
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", "", "321598", "test@gmail.com", "", "", null), 1));
    }

    [Fact]
    public void PassingUpdateCV()
    {
        CV test = new CV("", "", "", "", "321598", "test@gmail.com", "", "", null);
        test.CVID = 1;
        Assert.True(candidateDAL.UpdateCV(test));
        test.CVID = 0;
        Assert.True(candidateDAL.UpdateCV(test));
        Assert.True(candidateDAL.UpdateCV(new CV("", "", "", "", "321598", "test@gmail.com", "", "", null)));
    }

    [Fact]
    public void PassingInsertToApplyCandidates()
    {
        Assert.True(candidateDAL.InsertToApplyCandidates(1, 2));
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(null, 1)]
    [InlineData(1, 1)]
    public void FailingInsertToApplyCandidates(int? CandidateID, int NewsID)
    {
        Assert.False(candidateDAL.InsertToApplyCandidates(CandidateID, NewsID));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(null, 1)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void PassingGetApplyStatusByNewsID(int? CandidateID, int NewsID)
    {
        if(CandidateID > 0 && NewsID > 0)
        {
            Assert.True(candidateDAL.GetApplyStatusByNewsID(CandidateID, NewsID));
        }
        else
        {
            Assert.False(candidateDAL.GetApplyStatusByNewsID(CandidateID, NewsID));
        }
    }
}