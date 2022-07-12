namespace Persistence;
public class User
{
    public int UserID{get; set;}
    public string? Username{get; set;}
    public string? Email{get; set;}
    public string? Password{get; set;}
    public string? Gender{get; set;}

    public User()
    {

    }

    public User(string? _Username, string? _Email, string? _Password, string? _Gender)
    {
        this.Username = _Username;
        this.Email = _Email;
        this.Password = _Password;
        this.Gender = _Gender;
    }
}
