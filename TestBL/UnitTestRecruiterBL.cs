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
    public void PassingCreateNewProfile() //TC55
    {
        Assert.IsType<Recruiter>(recruiterBL.CreateNewProfile(new Recruiter("0123456789","staff" , "", "", "", "", ""), 6));
    }
    [Fact]
    public void FailingCreateNewProfile_ID_not_found() //TC56.1
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("0123456789","staff" , "", "", "", "", "" ), 0));
    }
    [Fact]
    public void FailingCreateNewProfile_ID_null() //TC56.2
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("0123456789","staff" , "", "", "", "", "" ), null));
    }
    [Fact]
    public void FailingCreateNewProfile_PhoneNUm_only_digits() //TC56.3
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("012345678a","staff" , "", "", "", "", "" ), 6));
    }
    [Fact]
    public void FailingCreateNewProfile_PhoneNum_null() //TC56.4
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("","staff" , "", "", "", "", "" ), 6));
    }
    [Fact]
    public void FailingCreateNewProfile_Postion_null() //TC56.5
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("0123456789","" , "", "", "", "", "" ), 6));
    }
    [Fact]
    public void FailingCreateNewProfile_Position_improperly() //TC56.6
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("0123456789","a" , "", "", "", "", "" ), 6));
    }
    [Fact]
    public void FailingCreateNewProfile_PhoneNum_must_10() //TC56.7
    {
        Assert.Null(recruiterBL.CreateNewProfile(new Recruiter("01234567891","staff" , "", "", "", "", "" ), 6));
    }


    [Fact]
    public void PassingCreateRecruitment() //TC57
    {
        Assert.IsType<RecruitNews>(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_ID_not_found() //TC58.1
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 0) );
    }
    [Fact]
    public void FailingCreateRecruitment_ID_null() //TC58.2
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), null) );
    }
    [Fact]
    public void FailingCreateRecruitment_SalaryRange_null() //TC58.3
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_SalaryRange_improperly() //TC58.4
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "a", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_HiringPosition_nul() //TC58.5
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_HiringPosition_improperly() //TC58.6
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","a", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_Profession_nul() //TC58.7
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  ""), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_Profession_improperly() //TC58.8
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "a"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_CityAddress_nul() //TC58.9
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_CityAddress_improperly() //TC58.10
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "a",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_Deadline_nul() //TC58.11
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingCreateRecruitment_Deadline_improperly() //TC58.12
    {
        Assert.Null(recruiterBL.CreateRecruitment(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }

    [Fact]
    public void PassingGetRecruiterByID() //TC59
    {
        Assert.IsType<int>(recruiterBL.GetRecruiterByID(6));
    }
    [Fact]
    public void FailingGetRecruiterByID_null() //TC60
    {
        Assert.Null(recruiterBL.GetRecruiterByID(null));
    }
    [Fact]
    public void FailingGetRecruiterByID_not_found() //TC60
    {
        Assert.Null(recruiterBL.GetRecruiterByID(0));
    }

    [Fact]
    public void PassingGetRecruitNewByID() //TC61
    {
        Assert.IsType<Recruiter>(recruiterBL.GetRecruitNewByID(6));
    }
    [Fact]
    public void FailingGetRecruitNewByID_not_found() //TC62
    {
        Assert.Null(recruiterBL.GetRecruitNewByID(0));
    }
    [Fact]
    public void FailingGetRecruitNewByID_null() //TC62
    {
        Assert.Null(recruiterBL.GetRecruitNewByID(null));
    }

    [Fact]
    public void PassingUpdateNewsInfo() //TC63
    {
        RecruitNews test = new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller");
        test.NewsID = 1;
        Assert.True(recruiterBL.UpdateNewsInfo(test));
        test.NewsID = 0;
        Assert.True(recruiterBL.UpdateNewsInfo(test));
        Assert.True(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_HiringPosition_null() //TC64.1
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_HiringPosition_improperly() //TC64.2
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","a", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_SalaryRange_null() //TC64.3
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_SalaryRange_improperly() //TC64.4
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "a", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_Profession_improperly() //TC64.5
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "a")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_Profession_null() //TC64.6
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_CityAddress_null() //TC64.7
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_CityAddress_improperly() //TC64.8
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "a",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_Deadline_null() //TC64.9
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNewsInfo_Deadline_improperly() //TC64.10
    {
        Assert.False(recruiterBL.UpdateNewsInfo(new RecruitNews("", DateTime.Parse("23-02-2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller")));
    }

    [Fact]
    public void PassingUpdatePersonalRecruitInfo() //TC65
    {
        Recruiter test = new Recruiter("0123456789","staff" , "", "", "", "", "");
        test.RecruiterID = 1;
        Assert.True(recruiterBL.UpdatePersonalRecruitInfo(test));
        test.RecruiterID = 0;
        Assert.True(recruiterBL.UpdatePersonalRecruitInfo(test));
        Assert.True(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("0123456789","staff" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdatePersonalRecruitInfo_PhoneNum_only_digits() //TC66.1
    {
        Assert.False(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("012345678a","staff" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdatePersonalRecruitInfo_PhoneNum_null() //TC66.2
    {
        Assert.False(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("","staff" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdatePersonalRecruitInfo_Position_null() //TC66.3
    {
        Assert.False(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("0123456789","" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdatePersonalRecruitInfo_Position_improperly() //TC66.4
    {
        Assert.False(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("0123456789","a" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdatePersonalRecruitInfo_PhoneNum_must_10() //TC66.5
    {
        Assert.False(recruiterBL.UpdatePersonalRecruitInfo(new Recruiter("01234567891","staff" , "", "", "", "", "")));
    }

    [Fact]
    public void PassingGetCVByJobPosition() //TC75
    {
        List<CV> cv = recruiterBL.GetCVByJobPosition("staff");
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVByJobPosition_null() //TC76
    {
        Assert.Null(recruiterBL.GetCVByJobPosition(""));
    }
    [Fact]
    public void FailingGetCVByJobPosition_not_found() //TC76
    {
        Assert.Null(recruiterBL.GetCVByJobPosition("CEO"));
    }

    [Fact]
    public void PassingGetCVByCareerTitle() //TC77
    {
        List<CV> cv = recruiterBL.GetCVByCareerTitle("Computer");
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVByCareerTitle_null() //TC78
    {
         Assert.Null(recruiterBL.GetCVByCareerTitle(""));
    }
    [Fact]
    public void FailingGetCVByCareerTitle_not_found() //TC78
    {
         Assert.Null(recruiterBL.GetCVByCareerTitle("Gamer"));
    }

    [Fact]
    public void PassingGetCVByAddress() //TC79
    {
        List<CV> cv = recruiterBL.GetCVByAddress("Ha Noi");
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVByAddress_null() //TC80
    {
        Assert.Null(recruiterBL.GetCVByAddress(""));
    }
    [Fact]
    public void FailingGetCVByAddress_not_found() //TC80
    {
        Assert.Null(recruiterBL.GetCVByAddress("Soc Son"));
    }

    [Fact]
    public void PassingGetCVAppliedInNews() //TC81
    {
        List<CV> cv = recruiterBL.GetCVAppliedInNews(1);
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVAppliedInNews_not_found() //TC82
    {
        Assert.Null(recruiterBL.GetCVAppliedInNews(0));
    }
}