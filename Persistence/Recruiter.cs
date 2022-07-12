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
}