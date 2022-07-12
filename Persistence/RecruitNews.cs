namespace Persistence;

public class RecruitNews
{
    public int NewsID{get; set;}
    public string? NewsName{get; set;}
    public string? DeadLine{get; set;}
    public string? SalaryRange{get; set;}
    public string? FormOfEmploy{get; set;}
    public string? Gender{get; set;}
    public string? HiringAmount{get; set;}
    public string? HiringPosition{get; set;}
    public string? RequiredExp{get; set;}
    public string? CityAddress{get; set;}
    public string? Profession{get; set;}
    public Boolean IsOpen{get; set;}

    public RecruitNews()
    {

    }

    public RecruitNews(string? _NewsName, string? _DeadLine, string? _SalaryRange, string? _FormOfEmploy, string? _Gender, string? _HiringAmount,
    string? _HiringPosition, string? _RequiredExp, string? _CityAddress, string? _Profession)
    {
        this.NewsName = _NewsName;
        this.DeadLine = _DeadLine;
        this.SalaryRange = _SalaryRange;
        this.FormOfEmploy = _FormOfEmploy;
        this.Gender = _Gender;
        this.HiringAmount = _HiringAmount;
        this.HiringPosition = _HiringPosition;
        this.RequiredExp = _RequiredExp;
        this.CityAddress = _CityAddress;
        this.Profession = _Profession;
    }
}