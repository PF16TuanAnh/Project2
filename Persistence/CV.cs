namespace Persistence;

public class CV
{
    public int CVID{get; set;}
    public string FullName{get; set;}
    public string? CareerTitle{get; set;}
    public string CareerObjective{get; set;}
    public string? BirthDate{get; set;}
    public string PhoneNum{get; set;}
    public string Email{get; set;}
    public string? SocialMedia{get; set;}
    public string PersonalAddress{get; set;}
    public List<CVDetails>? CVDetails{get; set;}

    public CV()
    {
        this.FullName = "";
        this.CareerObjective = "";
        this.PhoneNum = "";
        this.Email = "";
        this.PersonalAddress = "";
    }

    public CV(string _FullName, string? _CareerTitle, string _CareerObjective, string? _BirthDate, string _PhoneNum, string _Email, string? _SocialMedia, 
    string _PersonalAddress, List<CVDetails>? _CVDetails)
    {
        this.FullName = _FullName;
        this.CareerTitle = _CareerTitle;
        this.CareerObjective = _CareerObjective;
        this.BirthDate = _BirthDate;
        this.PhoneNum = _PhoneNum;
        this.Email = _Email;
        this.SocialMedia = _SocialMedia;
        this.PersonalAddress = _PersonalAddress;
        this.CVDetails = _CVDetails;
    }
}