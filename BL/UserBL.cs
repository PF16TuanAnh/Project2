using DAL;
using Persistence;
using System.Security.Cryptography;
using System.Text;

namespace BL;

public class UserBL
{
    private UserDAL userDAL;
    private string? PasswordToCheck;

    public UserBL()
    {
        userDAL = new UserDAL();
    }

    public bool VerifyEmail(string email)
    {
        email = email!.ToUpper();
        
        User user = userDAL.GetUserByEmail(email);
        if (user != null)
        {
            PasswordToCheck = user.Password;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool VerifyPassword(string password)
    {
        if (PasswordToCheck == GetHashString(password))
        {
            PasswordToCheck = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int? GetCandidateIDByEmail(string email)
    {
        email = email!.ToUpper();
        return userDAL.GetCandidateIDByEmail(email);
    }

    public int? InsertNewUser(User user, int role)
    {
        user.Email = user.Email!.ToUpper();
        user.Password = GetHashString(user.Password!);
        if(role == 1)
        {
            return userDAL.InsertNewCandidate(user);
        }
        else
        {
            return userDAL.InsertNewRecruiter(user);
        }
    }

    public byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
        sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
    //NCT 
    public int? GetRecruiterIDByEmail(string email)
    {
        email = email!.ToUpper();
        return userDAL.GetRecruiterIDByEmail(email);
    }
}