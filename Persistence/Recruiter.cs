namespace Persistence;

public class Recruiter : User
{
    public int RecruiterID{get; set;}
    public string? PhoneNum{get; set;}
    public string? Position{get; set;}
    public string? CompanyName{get; set;}
    public string? CompanyDescription{get; set;}
    public string? BussinessSize{get; set;}
    public string? BussinessField{get; set;}

    public Recruiter()
    {

    }

    public Recruiter(string? _Username, string? _PhoneNum, string? _Position, string? _CompanyName, 
    string? _CompanyDescription, string? _BussinessSize, string? _BussinessField)
    {   
        this.Username = _Username;
        this.PhoneNum = _PhoneNum;
        this.Position = _Position;
        this.CompanyName = _CompanyName;
        this.CompanyDescription = _CompanyDescription;
        this.BussinessSize = _BussinessSize;
        this.BussinessField = _BussinessField;
    }
}