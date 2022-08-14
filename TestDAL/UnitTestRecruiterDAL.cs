using Xunit;
using Persistence;
using DAL;
using System;
using System.Collections.Generic;

namespace TestDAL;

public class UnitTestRecruiterDAL : IDisposable
{
    public RecruiterDAL recruiterDAL = new RecruiterDAL();

    public void Dispose()
    {
        
    }

    [Fact]
    public void PassingGetRecruiterByNewsID()
    {
        Assert.IsType<Recruiter>(recruiterDAL.GetRecruiterByNewsID(1));
    }

    [Fact]
    public void FailingGetRecruiterByNewsID()
    {
        Assert.Null(recruiterDAL.GetRecruiterByNewsID(0));
    }

    [Fact]
    public void PassingGetNewsBySalaryRange()
    {
        List<RecruitNews> results = recruiterDAL.GetNewsBySalaryRange("Below 3 million");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsBySalaryRange()
    {
        Assert.Null(recruiterDAL.GetNewsBySalaryRange("Higher than 10 million"));
    }

    [Fact]
    public void PassingGetNewsByProfession()
    {
        List<RecruitNews> results = recruiterDAL.GetNewsByProfession("Seller");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsByProfession()
    {
        Assert.Null(recruiterDAL.GetNewsByProfession("Insurance"));
    }

    [Fact]
    public void PassingGetNewsByCityAddress()
    {
        List<RecruitNews> results = recruiterDAL.GetNewsByCityAddress("Ha Noi");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsByCityAddress()
    {
        Assert.Null(recruiterDAL.GetNewsByCityAddress("Dong Nai"));
    }
    // NCT
    [Fact]
    public void PassingGetRecruterByID()
    {
        Assert.IsType<Recruiter>(recruiterDAL.GetRecruiterByID(2));
    }
    public void FailingGetRecruterByID()
    {
        Assert.IsType<Recruiter>(recruiterDAL.GetRecruiterByID(0));
    } 
    public void PassingInsertNewProfile()
    {
        Assert.IsType<int>(recruiterDAL.InsertNewProfile(new Recruiter("09","staff" , "", "", "", "", "" ), 1) );
    }
    public void FailingCreateNewProfileInformation ()
    {
        Assert.IsType<int>(recruiterDAL.InsertNewProfile(new Recruiter("09","staff" , "", "", "", "", "" ), 1) );
    }
    public void PassingGetRecruitNewsByRecruterID()
    {
        Assert.IsType<RecruitNews>(recruiterDAL.GetRecruitNewsByRecruterID(2));
    }
    public void FailingGetRecruitNewsByRecruterID()
    {
        Assert.IsType<RecruitNews>(recruiterDAL.GetRecruitNewsByRecruterID(0));
    } 
    public void PassingInsertRecruitmentNew()
    {
        Assert.IsType<int>(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", "" , "", "", "1", "staff", "", true , "", "", "" ), 1) );
    }
    public void FailingInsertRecruitmentNew()
    {
        Assert.IsType<int>(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", "" , "", "", "1", "staff", "", true , "", "", "" ), 0) );
    } 
    public void PassingUpdateNews ()
    {
        RecruitNews test = new RecruitNews("", "" , "", "", "1", "staff", "", true, "", "", "" );
        test.NewsID = 1;
        Assert.True(recruiterDAL.UpdateNews(test));
        test.NewsID = 0;
        Assert.True(recruiterDAL.UpdateNews(test));
        Assert.True(recruiterDAL.UpdateNews(new RecruitNews("", "" , "", "", "1", "staff", "", true, "", "", "" )));
    }
    public void PassingUpdateRecruitInformation  ()
    {
        Recruiter test = new Recruiter("09","staff" , "", "", "", "", "");
        test.RecruiterID = 1;
        Assert.True(recruiterDAL.UpdateRecruitInformation(test));
        test.RecruiterID = 0;
        Assert.True(recruiterDAL.UpdateRecruitInformation(test));
        Assert.True(recruiterDAL.UpdateRecruitInformation(new Recruiter("09","staff" , "", "", "", "", "")));
    }
}