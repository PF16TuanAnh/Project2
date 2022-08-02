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
        List<RecruitNews> results = recruiterBL.GetNewsBySalaryRange("Bellow 3 million");
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
}