using Xunit;
using Persistence;
using DAL;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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
    public void PassingGetCandidateByID() //TC15
    {
        Assert.IsType<Candidate>(candidateDAL.GetCandidateByID(1));
    }
    [Fact]
    public void FailingGetCandidateByID_not_found () //TC16
    {
        Assert.Null(candidateDAL.GetCandidateByID(0));
    }
    [Fact]
    public void FailingGetCandidateByID_null() //TC16
    {
        Assert.Null(candidateDAL.GetCandidateByID(null));
    }
    

    [Fact]
    public void PassingAddNewCV() //TC25
    {
        Assert.IsType<int>(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingAddNewCV_only_1() //TC26.1
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), 1));
    }
    [Fact]
    public void FailingAddNewCV_CandidateID_not_found() //TC26.2
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), 0));
    }
    [Fact]
    public void FailingCreateNewCV_CandidateID_null() //TC26.3
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null), null));
    }
    [Fact]
    public void FailingAddNewCV_PhoneNum_only_digits() //TC26.4
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "012345678a", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingAddNewCV_PhoneNum_null() //TC26.5
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "", "test@gmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingAddNewCV_Email_malformed() //TC26.6
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "testgmail.com", "", "", null), 4));
    }
    [Fact]
    public void FailingAddNewCV_Email_null() //TC26.7
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "", "", "", null), 4));
    }
    [Fact]
    public void FailingAddNewCV_PhoneNum_10_numeriC() //TC26.8
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "01234567891", "test@gmail.com", "", "", null), 4));
    }
     [Fact]
    public void FailingAddNewCV_BirthDate_null() //TC26.9
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse(""), "0123456789", "test@gmail.com", "", "", null), 4));
    } [Fact]
    public void FailingAddNewCV_BirthDate_malformed() //TC26.10
    {
        Assert.Null(candidateDAL.AddNewCV(new CV("", "", "", DateTime.Parse("11-02-2010"), "0123456789", "test@gmail.com", "", "", null), 4));
    }


    [Fact]
    public void PassingUpdateCV() //TC27
    {
        CV test = new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null);
        test.CVID = 1;
        Assert.True(candidateDAL.UpdateCV(test));
        test.CVID = 0;
        Assert.True(candidateDAL.UpdateCV(test));
        Assert.True(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_PhoneNum_only_digits() //TC28.1
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "012345678a", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_PhoneNum_Null() //TC28.2
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_Email_malformed() //TC28.3
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "testgmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_Email_null() //TC28.4
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "0123456789", "", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_PhoneNum_not_10_numerics() //TC28.5
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02/2010"), "01234567891", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_BirthDate_null() //TC28.6
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse(""), "01234567891", "test@gmail.com", "", "", null)));
    }
    [Fact]
    public void FailingUpdateCVInfo_BirthDate_malformed() //TC28.7
    {
        Assert.False(candidateDAL.UpdateCV(new CV("", "", "", DateTime.Parse("11/02"), "01234567891", "test@gmail.com", "", "", null)));
    }



    [Fact]
    public void PassingInsertToApplyCandidates() //TC29
    {
        Assert.True(candidateDAL.InsertToApplyCandidates(1, 2));
    }
    [Theory]
    [InlineData(1, 0)] //News ID not found in the database
    [InlineData(0, 2)] //Candidate ID not found in the database
    [InlineData(null, 2)] //Candidate ID cannot be null
    // [InlineData(1, null)] //News ID cannot be null
    public void FailingInsertToApplyCandidates(int? CandidateID, int NewsID) //TC30
    {
        Assert.False(candidateDAL.InsertToApplyCandidates(CandidateID, NewsID));
    }

    [Fact]
    public void PassingGetApplyStatusByNewsID() //TC31
    {
        Assert.True(candidateDAL.InsertToApplyCandidates(1, 2));
    }
    [Theory]
    [InlineData(1, 0)] //News ID not found in the database
    [InlineData(0, 2)] //Candidate ID not found in the database
    [InlineData(null, 2)] //Candidate ID cannot be null
    // [InlineData(1, null)] //News ID cannot be null
    public void FailingGetApplyStatusByNewsID(int? CandidateID, int NewsID) //TC32
    {
        Assert.False(candidateDAL.GetApplyStatusByNewsID(CandidateID, NewsID));
    }

    [Fact]
    public void PassingGetRecruiterByNewsID() //TC35
    {
        Assert.IsType<Recruiter>(candidateDAL.GetRecruiterByNewsID(1));
    }

    [Fact]
    public void FailingGetRecruiterByNewsID_not_found() //TC36
    {
        Assert.Null(candidateDAL.GetRecruiterByNewsID(0));
    }
    
    [Fact]
    public void PassingGetNewsBySalaryRange() //TC37
    {
        List<RecruitNews> results = candidateDAL.GetNewsBySalaryRange("Below 3 million");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsBySalaryRange_not_found() //TC38
    {
        Assert.Null(candidateDAL.GetNewsBySalaryRange("Higher than 10 million"));
    }
    [Fact]
    public void FailingGetNewsBySalaryRange_null() //TC38
    {
        Assert.Null(candidateDAL.GetNewsBySalaryRange(""));
    }

    [Fact]
    public void PassingGetNewsByProfession() //TC39
    {
        List<RecruitNews> results = candidateDAL.GetNewsByProfession("Seller");
        Assert.IsType<RecruitNews>(results[0]);
    }
    [Fact]
    public void FailingGetNewsByProfession_not_found() //TC40
    {
        Assert.Null(candidateDAL.GetNewsByProfession("Insurance"));
    }
    [Fact]
    public void FailingGetNewsByProfession_null() //TC40
    {
        Assert.Null(candidateDAL.GetNewsByProfession(""));
    }

    [Fact]
    public void PassingGetNewsByCityAddress() //TC41
    {
        List<RecruitNews> results = candidateDAL.GetNewsByCityAddress("Ha Noi");
        Assert.IsType<RecruitNews>(results[0]);
    }
    [Fact]
    public void FailingGetNewsByCityAddress_not_found() //TC42
    {
        Assert.Null(candidateDAL.GetNewsByCityAddress("Dong Nai"));
    }
    [Fact]
    public void FailingGetNewsByCityAddress_null() //TC42
    {
        Assert.Null(candidateDAL.GetNewsByCityAddress(""));
    }

    [Fact]
    public void PassingGetCandidateInfo() //TC97
    {
        Assert.IsType<Candidate>(candidateDAL.GetCandidateInfo(DBHelper.ExecQuery(@"select * from Candidates c inner join Users u on c.UserID = u.UserID where CandidateID = 1")));
    }
    [Fact]
    public void FailingGetCandidateInfo_not_found () //TC98
    {
        Assert.Null(candidateDAL.GetCandidateInfo(DBHelper.ExecQuery(@"select * from Candidates c inner join Users u on c.UserID = u.UserID where CandidateID = 0")));
    }
    [Fact]
    public void FailingGetCandidateInfo_SQL_error() //TC98
    {
        Assert.Null(candidateDAL.GetCandidateInfo(DBHelper.ExecQuery(@"select * from Candidates c inner join Users u on c.UserID = u.UserID  CandidateID = 1")));
    }
    
     [Fact]
    public void PassingGetCVByCandidateID() //TC99
    {
        Assert.IsType<CV>(candidateDAL.GetCVByCandidateID(1));
    }
    [Fact]
    public void FailingGetCVByCandidateID_not_found () //TC100
    {
        Assert.Null(candidateDAL.GetCVByCandidateID(0));
    }

}