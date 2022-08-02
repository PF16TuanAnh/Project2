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
        List<RecruitNews> results = recruiterDAL.GetNewsBySalaryRange("Bellow 3 million");
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
}