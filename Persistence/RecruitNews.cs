namespace Persistence;

public class RecruitNews
{
    public int NewsID{get; set;}
    public int RecruiterID{get; set;}
    public string NewsName{get; set;}
    public DateTime DeadLine{get; set;}
    public string SalaryRange{get; set;}
    public string FormOfEmploy{get; set;}
    public string Gender{get; set;}
    public bool IsOpen{get; set;} // them
    public string HiringAmount{get; set;}
    public string HiringPosition{get; set;}
    public string RequiredExp{get; set;}
    public string CityAddress{get; set;}
    public string Profession{get; set;}


    public RecruitNews()
    {
        this.NewsName = "";
        this.DeadLine = new DateTime();
        this.SalaryRange = "";
        this.HiringAmount = "";
        this.HiringPosition = "";
        this.CityAddress = "";
        this.Profession = "";
        this.SalaryRange = "";
        this.CityAddress = "";
        this.Profession = "";
        this.FormOfEmploy = "";
        this.Gender = "";
        this.RequiredExp = "";
    }

    public RecruitNews(string _NewsName, DateTime _DeadLine,  string _FormOfEmploy, string _Gender, string _HiringAmount, 
    string _HiringPosition, string _RequiredExp, string _SalaryRange,string _CityAddress, string _Profession)  
    { 
        this.NewsName = _NewsName;
        this.DeadLine = _DeadLine;
        this.FormOfEmploy = _FormOfEmploy;
        this.Gender = _Gender;
        this.HiringAmount = _HiringAmount;
        this.HiringPosition = _HiringPosition;
        this.RequiredExp = _RequiredExp;
        this.CityAddress = _CityAddress;
        this.Profession = _Profession;
        this.SalaryRange = _SalaryRange;
    }
}