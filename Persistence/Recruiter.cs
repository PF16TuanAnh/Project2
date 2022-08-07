namespace Persistence;

public class Recruiter : User
{
    public int RecruiterID{get; set;}
    public string? PhoneNum{get; set;}
    public string? Position{get; set;}
    public string? CompanyName{get; set;}
    public string? CompanyDescription{get; set;}
    public string? CompanyAddress{get; set;}
    public string? BusinessSize{get; set;}
    public string? BusinessField{get; set;}

    public Recruiter()
    {

    }

    public Recruiter(string? _PhoneNum, string? _Position, string? _CompanyName, 
    string? _CompanyDescription, string? _CompanyAddress,string? _BusinessSize, string? _BusinessField)
    {   
        this.PhoneNum = _PhoneNum;
        this.Position = _Position;
        this.CompanyName = _CompanyName;
        this.CompanyDescription = _CompanyDescription;
        this.CompanyAddress = _CompanyAddress;
        this.BusinessSize = _BusinessSize;
        this.BusinessField = _BusinessField;
    }
}