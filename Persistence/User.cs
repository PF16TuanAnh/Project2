namespace Persistence;
public class User
{
    public int UserID{get; set;}
    public string? Name{get; set;}
    public string? Email{get; set;}
    public string? Password{get; set;}
    public string? Gender{get; set;}

    public User()
    {

    }

    public User(string? _Name, string? _Email, string? _Password, string? _Gender)
    {
        this.Name = _Name;
        this.Email = _Email;
        this.Password = _Password;
        this.Gender = _Gender;
    }
}
