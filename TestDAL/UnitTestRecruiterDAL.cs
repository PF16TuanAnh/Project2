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

    
    // NCT
    [Fact]
    public void PassingGetRecruterByID() //TC51
    {
        Assert.IsType<Recruiter>(recruiterDAL.GetRecruiterByID(6));
    }
    [Fact]
    public void FailingGetRecruterByID_null() //TC52
    {
        Assert.IsType<Recruiter>(recruiterDAL.GetRecruiterByID(null));
    } 
    [Fact]
    public void FailingGetRecruterByID_not_found() //TC52
    {
        Assert.IsType<Recruiter>(recruiterDAL.GetRecruiterByID(0));
    }

    [Fact]
    public void PassingInsertNewProfile() //TC53
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("0123456789","staff" , "", "", "", "", "" ), 6) );
    }
    [Fact]
    public void FailingInsertNewProfile_ID_not_found() //TC54.1
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("0123456789","staff" , "", "", "", "", "" ), 0) );
    }
    [Fact]
    public void FailingInsertNewProfile_ID_null() //TC54.2
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("0123456789","staff" , "", "", "", "", "" ), null) );
    }
    [Fact]
    public void FailingInsertNewProfile_PhoneNum_only_degits() //TC54.3
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("012345678a","staff" , "", "", "", "", "" ), 6) );
    }
    [Fact]
    public void FailingInsertNewProfile_PhoneNum_null() //TC54.4
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("","staff" , "", "", "", "", "" ), 6) );
    }
    [Fact]
    public void FailingInsertNewProfile_Position_null() //TC54.5
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("0123456789","" , "", "", "", "", "" ), 6) );
    }
    [Fact]
    public void FailingInsertNewProfile_Position_improperly() //TC54.6
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("0123456789","a" , "", "", "", "", "" ), 6) );
    }
    [Fact]
    public void FailingInsertNewProfile_PhoneNum_must_10() //TC54.7
    {
        Assert.Null(recruiterDAL.InsertNewProfile(new Recruiter("01234567891","staff" , "", "", "", "", "" ), 6) );
    }

    [Fact]
    public void PassingGetRecruitNewsByRecruterID() //TC67
    {
        Assert.IsType<RecruitNews>(recruiterDAL.GetRecruitNewsByRecruterID(6));
    }
    [Fact]
    public void FailingGetRecruitNewsByRecruterID_not_found() //TC68
    {
        Assert.IsType<RecruitNews>(recruiterDAL.GetRecruitNewsByRecruterID(0));
    } 
    [Fact]
    public void FailingGetRecruitNewsByRecruterID_null() //TC68
    {
        Assert.IsType<RecruitNews>(recruiterDAL.GetRecruitNewsByRecruterID(null));
    } 

    [Fact]
    public void PassingInsertRecruitmentNew() //TC69
    {
        Assert.IsType<int>(recruiterDAL.InsertRecruitmentNew(new RecruitNews ("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_ID_not_found() //TC70.1
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 0) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_ID_null() //TC70.2
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), null) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_SalaryRange_null() //TC70.3
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_SalaryRange_improperly() //TC70.4
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "a", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_HiringPosition_nul() //TC70.5
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_HiringPosition_improperly() //TC70.6
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","a", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_Profession_nul() //TC70.7
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  ""), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_Profession_improperly() //TC70.8
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "a"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_CityAddress_nul() //TC70.9
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_CityAddress_improperly() //TC70.10
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "a",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_Deadline_nul() //TC70.11
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }
    [Fact]
    public void FailingInsertRecruitmentNew_Deadline_improperly() //TC70.12
    {
        Assert.Null(recruiterDAL.InsertRecruitmentNew(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller"), 6) );
    }

    [Fact] 
    public void PassingUpdateNews () //TC71
    {
        RecruitNews test = new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller");
        test.NewsID = 1;
        Assert.True(recruiterDAL.UpdateNews(test));
        test.NewsID = 0;
        Assert.True(recruiterDAL.UpdateNews(test));
        Assert.True(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_HiringPosition_null() //TC72.1
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_HiringPosition_improperly() //TC72.2
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","a", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_SalaryRange_null() //TC72.3
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_SalaryRange_improperly() //TC72.4
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", "a", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_Profession_improperly() //TC72.5
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "a")));
    }
    [Fact]
    public void FailingUpdateNews_Profession_null() //TC72.6
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "")));
    }
    [Fact]
    public void FailingUpdateNews_CityAddress_null() //TC72.7
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_CityAddress_improperly() //TC72.8
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23/02/2010") , "", "", "","staff", "", " Below 3 million", "a",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_Deadline_null() //TC72.9
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller")));
    }
    [Fact]
    public void FailingUpdateNews_Deadline_improperly() //TC72.10
    {
        Assert.False(recruiterDAL.UpdateNews(new RecruitNews("", DateTime.Parse("23-02-2010") , "", "", "","staff", "", " Below 3 million", "Ha Noi",  "Seller")));
    }

    [Fact]
    public void PassingUpdateRecruitInformation  () //TC73
    {
        Recruiter test = new Recruiter("0123456789","staff" , "", "", "", "", "");
        test.RecruiterID = 1;
        Assert.True(recruiterDAL.UpdateRecruitInformation(test));
        test.RecruiterID = 0;
        Assert.True(recruiterDAL.UpdateRecruitInformation(test));
        Assert.True(recruiterDAL.UpdateRecruitInformation(new Recruiter("0123456789","staff" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdateRecruitInformation_PhoneNum_only_digits() //TC74.1
    {
        Assert.False(recruiterDAL.UpdateRecruitInformation(new Recruiter("012345678a","staff" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdateRecruitInformation_PhoneNum_null() //TC74.2
    {
        Assert.False(recruiterDAL.UpdateRecruitInformation(new Recruiter("","staff" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdateRecruitInformation_Position_null() //TC74.3
    {
        Assert.False(recruiterDAL.UpdateRecruitInformation(new Recruiter("0123456789","" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdateRecruitInformation_Position_improperly() //TC74.4
    {
        Assert.False(recruiterDAL.UpdateRecruitInformation(new Recruiter("0123456789","a" , "", "", "", "", "")));
    }
    [Fact]
    public void FailingUpdateRecruitInformation_PhoneNum_must_10() //TC74.5
    {
        Assert.False(recruiterDAL.UpdateRecruitInformation(new Recruiter("01234567891","staff" , "", "", "", "", "")));
    }

    [Fact]
    public void PassingGetCVByJobPosition() //TC83
    {
        List<CV> cv = recruiterDAL.GetCVByJobPosition("staff");
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVByJobPosition_null() //TC84
    {
        Assert.Null(recruiterDAL.GetCVByJobPosition(""));
    }
    [Fact]
    public void FailingGetCVByJobPosition_not_found() //TC84
    {
        Assert.Null(recruiterDAL.GetCVByJobPosition("CEO"));
    }

    [Fact]
    public void PassingGetCVByCareerTitle() //TC85
    {
        List<CV> cv = recruiterDAL.GetCVByCareerTitle("Computer");
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVByCareerTitle_null() //TC86
    {
        Assert.Null(recruiterDAL.GetCVByCareerTitle(""));
    }
    [Fact]
    public void FailingGetCVByCareerTitle_not_found() //TC86
    {
        Assert.Null(recruiterDAL.GetCVByCareerTitle("Gamer"));
    }

    [Fact]
    public void PassingGetCVByAddress () //TC87
    {
        List<CV> cv = recruiterDAL.GetCVByAddress("Ha Noi");
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVByAddress_null() //TC88
    {
        Assert.Null(recruiterDAL.GetCVByAddress(""));
    }
    [Fact]
    public void FailingGetCVByAddress_not_found() //TC88
    {
        Assert.Null(recruiterDAL.GetCVByAddress("Soc Son"));
    }

    [Fact]
    public void PassingGetCVAppliedInNews() //TC89
    {
        List<CV> cv = recruiterDAL.GetCVAppliedInNews(1);
        Assert.IsType<CV>(cv[0]);
    }
    [Fact]
    public void FailingGetCVAppliedInNews_not_found() //TC90
    {
        Assert.Null(recruiterDAL.GetCVAppliedInNews(0));
    }
}