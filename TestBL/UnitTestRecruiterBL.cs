using Xunit;
using Persistence;
using BL;
using System;
using System.Collections.Generic;

namespace TestDAL;

public class UnitTestRecruiterDAL : IDisposable
{
    public RecruiterBL recruiterBL = new RecruiterBL();

    public void Dispose()
    {
        
    }

    [Fact]
    public void PassingGetRecruiterByNewsID()
    {
        Assert.IsType<Recruiter>(recruiterBL.GetRecruiterByNewsID(1));
    }

    [Fact]
    public void FailingGetRecruiterByNewsID()
    {
        Assert.Null(recruiterBL.GetRecruiterByNewsID(0));
    }

    [Fact]
    public void PassingGetNewsBySalaryRange()
    {
        List<RecruitNews> results = recruiterBL.GetNewsBySalaryRange("Below 3 million");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsBySalaryRange()
    {
        Assert.Null(recruiterBL.GetNewsBySalaryRange("Higher than 10 million"));
    }

    [Fact]
    public void PassingGetNewsByProfession()
    {
        List<RecruitNews> results = recruiterBL.GetNewsByProfession("Seller");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsByProfession()
    {
        Assert.Null(recruiterBL.GetNewsByProfession("Insurance"));
    }

    [Fact]
    public void PassingGetNewsByCityAddress()
    {
        List<RecruitNews> results = recruiterBL.GetNewsByCityAddress("Ha Noi");
        Assert.IsType<RecruitNews>(results[0]);
    }

    [Fact]
    public void FailingGetNewsByCityAddress()
    {
        Assert.Null(recruiterBL.GetNewsByCityAddress("Dong Nai"));
    }
    //NCT 
    [Fact]
    public void PassingCreateNewProfile()
    {
        Assert.IsType<int>(recruiterBL.CreateNewProfile(new Recruiter("09","staff" , "", "", "", "", "" ), 1));
    }
    public void FailingCreateNewProfile()
    {
        Assert.IsType<int>(recruiterBL.CreateNewProfile(new Recruiter("09","staff" , "", "", "", "", "" ), 1));
    }
    public void PassingCreateRecruitment()
    {
        Assert.IsType<int>(recruiterBL.CreateRecruitment(new RecruitNews("", "" , "", "", "1", "staff", "", true, "", "", ""), 1) );
    }
    public void FailingCreateRecruitment()
    {
        Assert.IsType<int>(recruiterBL.CreateRecruitment(new RecruitNews("", "" , "", "", "1", "staff", "", true, "", "", ""), 0) );
    }
    public void PassingGetRecruiterByID ()
    {
        Assert.IsType<Recruiter>(recruiterBL.GetRecruiterByID(2));
    }
    public void FailingGetRecruiterByID()
    {
        Assert.IsType<Recruiter>(recruiterBL.GetRecruiterByID(0));
    }
    public void PassingGetRecruitNewByID()
    {
        Assert.IsType<Recruiter>(recruiterBL.GetRecruitNewByID(2));
    }
    public void FailingGetRecruitNewByID()
    {
        Assert.IsType<Recruiter>(recruiterBL.GetRecruitNewByID(0));
    }
    public void PassingUpdateNewsInfo ()
    {
        RecruitNews test = new RecruitNews("", "" , "", "", "1", "staff", "", true, "", "", "" );
        test.NewsID = 1;
        Assert.True(recruiterBL.UpdateNewsInfo(test));
        test.NewsID = 0;
        Assert.True(recruiterBL.UpdateNewsInfo(test));
        Assert.True(recruiterBL.UpdateNewsInfo(new RecruitNews("", "" , "", "", "1", "staff", "", true, "", "", "" )));
    }
    public void PassingUpdatePersonalRecruitInfo ()
    {
        Recruiter test = new Recruiter("09","staff" , "", "", "", "", "");
        test.RecruiterID = 1;
        Assert.True(recruiterBL.UpdatePersonalRecruitInfo(test));
        test.RecruiterID = 0;
        Assert.True(recruiterBL.UpdatePersonalRecruitInfo(test));
        Assert.True(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("09","staff" , "", "", "", "", "")));
    }
}