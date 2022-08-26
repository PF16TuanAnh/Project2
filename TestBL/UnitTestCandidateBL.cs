using Xunit;
using Persistence;
using BL;
using DAL;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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
    public void PassingCreateNewCV_() //TC17
    {
        Assert.IsType<int>(candidateBL.CreateNewCV(new CV("", "", "",DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_only_1() //TC18.1
    {
        Assert.IsType<int>(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), 1));
    }
    [Fact]
    public void FailingCreateNewCV_CandidateID_not_found () //TC18.2
    {
        Assert.IsType<int>(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), 0));
    }
    [Fact]
    public void FailingCreateNewCV_CandidateID_null() //TC18.3
    {
        Assert.IsType<int>(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), null));
    }
    [Fact]
    public void FailingCreateNewCV_PhoneNum_only_accepts_digits() //TC18.4
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "012345678a", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_PhoneNum_Null() //TC18.5
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_Email_malformed() //TC18.6
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "testgmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_Email_null() //TC18.7
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_PhoneNum_not_10_numerics() //TC18.8
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "01234567891", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_BirthDate_null() //TC18.9
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", null, "0123456789", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingCreateNewCV_BirthDate_malformed() //TC18.10
    {
        Assert.Null(candidateBL.CreateNewCV(new CV("", "", "", DateTime.Parse("11-02-20210"), "0123456789", "test@gmail.com", "", "", null), 4));
    }

    [Fact]
    public void PassingUpdateCVInfo() //TC19
    {
        CV test = new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null);
        test.CVID = 1;
        Assert.True(candidateBL.UpdateCVInfo(test));
        test.CVID = 0;
        Assert.True(candidateBL.UpdateCVInfo(test));
        Assert.True(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_PhoneNum_only_digits() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02/2010"), "012345678a", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_PhoneNum_Null() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02/2010"), "", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_Email_malformed() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "testgmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_Email_null() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_PhoneNum_not_10_numerics() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02/2010"), "01234567891", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_BirthDate_null() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse(""), "01234567891", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_BirthDate_malformed() //TC20
    {
        Assert.False(candidateBL.UpdateCVInfo(new CV("", "", "", DateTime.Parse("11/02"), "01234567891", "test@gmail.com", "", "", null)));
    }

    [Fact]
    public void PassingInsertToApplyCandidates() //TC21
    {
        Assert.True(candidateBL.ApplyToNews(1, 2));
    }

    [Theory]
    [InlineData(1, 0)] // News ID not found in the database
    [InlineData(0, 2)] //Candidate ID not found in the database
    [InlineData(null, 2)] // Candidate ID cannot be null
    // [InlineData(1, null)] // News ID cannot be null
    
    public void FailingApplyToNews(int? CandidateID, int NewsID) //TC22
    {
        Assert.False(candidateBL.ApplyToNews(CandidateID, NewsID));
    }
    
    [Fact]
    public void PassingIsApplied() //TC23
    {
        Assert.True(candidateBL.IsApplied(1, 2));
    }
    
    [Theory]
    [InlineData(1, 0)] // News ID not found in the database
    [InlineData(0, 2)] //Candidate ID not found in the database
    [InlineData(null, 2)] // Candidate ID cannot be null
    // [InlineData(1, null)] // News ID cannot be null
    public void FailingIsApplied(int? CandidateID, int NewsID) //TC24
    {
        Assert.False(candidateBL.IsApplied(CandidateID, NewsID));
    }

    [Fact]
    public void PassingGetCandidateByID() //TC33
    {
        Assert.IsType<Candidate>(candidateBL.GetCandidateByID(1));
    }

    [Fact]
    public void FailingGetCandidateByID_not_found() //TC34
    {
        Assert.Null(candidateBL.GetCandidateByID(0));
    }
    [Fact]
    public void FailingGetCandidateByID_null() //TC34
    {
        Assert.Null(candidateBL.GetCandidateByID(null));
    }
    [Fact]
    public void PassingGetRecruiterByNewsID() //TC43
    {
        Assert.IsType<Recruiter>(candidateBL.GetRecruiterByNewsID(1));
    }

    [Fact]
    public void FailingGetRecruiterByNewsID() //TC44
    {
        Assert.Null(candidateBL.GetRecruiterByNewsID(0));
    }
    [Fact]
    public void PassingGetNewsBySalaryRange() //TC45
    {
        List<RecruitNews> results = candidateBL.GetNewsBySalaryRange("Below 3 million");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsBySalaryRange_not_found() //TC46
    {
        Assert.Null(candidateBL.GetNewsBySalaryRange("Higher than 10 million"));
    }
    [Fact]
    public void FailingGetNewsBySalaryRange_null() //TC46
    {
        Assert.Null(candidateBL.GetNewsBySalaryRange(""));
    }

    [Fact]
    public void PassingGetNewsByProfession() //TC47
    {
        List<RecruitNews> results = candidateBL.GetNewsByProfession("Seller");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsByProfession_not_found() //TC48
    {
        Assert.Null(candidateBL.GetNewsByProfession("Insurance"));
    }
    [Fact]
    public void FailingGetNewsByProfession_null() //TC48
    {
        Assert.Null(candidateBL.GetNewsByProfession(""));
    }

    [Fact]
    public void PassingGetNewsByCityAddress() //TC49
    {
        List<RecruitNews> results = candidateBL.GetNewsByCityAddress("Ha Noi");
        Assert.IsType<RecruitNews>(results[0]);
    }
    [Fact]
    public void FailingGetNewsByCityAddress_not_found() //TC50
    {
        Assert.Null(candidateBL.GetNewsByCityAddress("Dong Nai"));
    }
    [Fact]
    public void FailingGetNewsByCityAddress_null() //TC50
    {
        Assert.Null(candidateBL.GetNewsByCityAddress(""));
    }

    
}