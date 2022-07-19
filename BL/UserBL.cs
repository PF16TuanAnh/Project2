﻿using DAL;
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
        // email = email!.ToUpper();
        // email = GetHashString(email);
        User user = userDAL.GetUserByEmail(email);
        return user;
    }

    public bool CheckUserEmail(string email)
    {
        if (GetUserByEmail(email) != null)
        {
            return true;
        }
        
        return false;
    }
    public bool CheckUserPassword(string email, string password)
    {
        User user = GetUserByEmail(email);
        // password = GetHashString(password);
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
        // email = email!.ToUpper();
        // email = GetHashString(email);
        return userDAL.GetCandidateIDByEmail(email);
    }

    public int? InsertNewUser(User user, int role)
    {
        user.Email = user.Email!.ToUpper();
        user.Email = GetHashString(user.Email);
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
}