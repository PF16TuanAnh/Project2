using DAL;
using Persistence;
using System.Security.Cryptography;
using System.Text;

namespace BL;

public class UserBL
{
    private UserDAL userDAL;

    public UserBL()
    {
        userDAL = new UserDAL();
    }

    public User GetUserByEmail(string email)
    {
        User user = userDAL.GetUserByEmail(email);
        return user;
    }

    public bool CheckUserEmail(string email)
    {
        // email = email.ToUpper();
        
        if (GetUserByEmail(email) != null)
        {
            return true;
        }
        
        return false;
    }
    public bool CheckUserPassword(string email, string password)
    {
        User user = GetUserByEmail(email);
        if (user != null)
        {
            if (password == user.Password)
            {
                return true;
            }

            return false;
        }
        
        return false;
    }

    public int? GetCandidateIDByEmail(string email)
    {
        return userDAL.GetCandidateIDByEmail(email);
    }

    public static byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
        sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}