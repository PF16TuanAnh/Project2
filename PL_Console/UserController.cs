namespace Pl_Console;
using BL;
using System.Net.Mail;
using Persistence;
using System.Text.RegularExpressions;

public class UserController
{
    private readonly Regex NumericRegex = new Regex(@"^[0-9]*$");
    private UserBL userBL;

    public UserController()
    {
        userBL = new UserBL();
    }

    public void LogIn()
    {
        string email;
        string password;

        Console.WriteLine("================================\n");
        Console.WriteLine("            LOG IN");
        Console.WriteLine("\n================================");
        Console.WriteLine(" You can Enter 0 on email or password to turn back.");
        Console.Write(" Email: ");
        email = GetUserInput();

        while (true)
        {
            if (email == "0")
            {
                break;
            }

            
            if (userBL.VerifyEmail(email))
            {
                Console.Write(" Password: ");
                password = GetUserInput();
                if (password == "0")
                {
                    break;
                }

                if (userBL.VerifyPassword(password))
                {
                    int? CandidateID = userBL.GetCandidateIDByEmail(email);
                    int? RecruiterID = userBL.GetRecruiterIDByEmail(email);
                    if (CandidateID != null)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine(" Logged in as a Candidate.");
                        Menu.PrintMainMenu(2, CandidateID);
                    }
                    else if (RecruiterID != null)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine(" Logged in as a Recruiter.");
                        Menu.PrintMainMenu(3, RecruiterID);
                    }
                }
                else
                {
                    Console.WriteLine("================================");
                    Console.WriteLine(" Password is incorrect! Please re-enter your password.");
                }
            }
            else
            {
                Console.WriteLine("================================");
                Console.WriteLine(" Email doesn't exist! Please re-enter your email.");
                Console.Write(" Email: ");
                email = GetUserInput();
            }
        }
    }
        
    public void Register()
    {
        string email;
        string password;
        string username;
        string gender = "Other";
        int role = 1; // 1 = "Candidate", 2 = "Recruiter"
        bool end = false;

        Console.WriteLine("================================\n");
        Console.WriteLine("            REGISTER");
        Console.WriteLine("\n================================");
        while (true)
        {
            Console.WriteLine(" You can Enter 0 on email to turn back.");
            Console.Write(" Email: ");
            email = GetUserInput();
            if(email.Length > 100)
            {
                Console.WriteLine("\n Email is too long. Maximum characters allowed is 100\n");
            }
            else if (email == "0")
            {
                break;
            }
            else
            {
                if (IsValidEmail(email))
                {
                    if (!userBL.VerifyEmail(email))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n Email has been used.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n Invalid email format! Please re-enter your emai.\n");
                }
            }
        }
        
        if (email != "0")
        {
            while (true)
            {
                Console.WriteLine(" You can Enter 0 on password to turn back.");
                Console.Write(" Password: ");
                password = GetUserInput();
                if(password.Length > 100)
                {
                    Console.WriteLine("\n Password is too long. Maximum characters allowed is 100\n");
                }
                else
                {
                    break;
                }
            }  

            if(password != "0")
            {
                while (true)
                {
                    Console.Write(" Username: ");
                    username = GetUserInput();
                    if(username.Length > 50)
                    {
                        Console.WriteLine("\n Username is too long. Maximum characters allowed is 50\n");
                    }
                    else
                    {
                        break;
                    }
                }  
                

                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("          Your Gender");
                    Console.WriteLine("\n================================");
                    Console.WriteLine(" 1) Male");
                    Console.WriteLine(" 2) Female");
                    Console.WriteLine(" 3) Other");
                    Console.WriteLine("================================");
                    Console.Write(" Enter the option number: ");
                    switch (GetUserInput())
                    {
                        case "1":
                            gender = "Male";
                            end = true;
                            break;
                        case "2":
                            gender = "Female";
                            end = true;
                            break;
                        case "3":
                            gender = "Other";
                            end = true;
                            break;
                        default:
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Invalid choice! Please re-enter your option.");
                            break;
                    }

                    if (end == true)
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("          Register As");
                    Console.WriteLine("\n================================");
                    Console.WriteLine(" 1) Candidate");
                    Console.WriteLine(" 2) Recruiter");
                    Console.WriteLine("================================");
                    Console.Write(" Enter the option number: ");
                    switch (GetUserInput())
                    {
                        case "1":
                            role = 1;
                            end = true;
                            break;
                        case "2":
                            role = 2;
                            end = true;
                            break;
                        default:
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Invalid choice! Please re-enter your option.");
                            break;
                    }

                    if (end == true)
                    {
                        break;
                    }
                }

                User newAccount = new User(username, email, password, gender);
                int? ID = userBL.InsertNewUser(newAccount, role);

                if(role == 1)
                {
                    if(ID != null)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine(" Logged in as a Candidate.");
                        Menu.PrintMainMenu(2, ID);
                    }
                    else
                    {
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" System Error! Couldn't log in.");
                    }
                }
                else
                {
                    if(ID != null)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine(" Logged in as a Recruiter.");
                        Menu.PrintMainMenu(3, ID);
                    }
                    else
                    {
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" System Error! Couldn't log in.");
                    }
                }
            }
        }
        
    }

    public static bool IsValidEmail(string email)
    {
        if (!MailAddress.TryCreate(email, out var mailAddress))
            return false;

        // And if you want to be more strict:
        var hostParts = mailAddress.Host.Split('.');
        if (hostParts.Length == 1)
            return false; // No dot.
        if (hostParts.Any(p => p == string.Empty))
            return false; // Double dot.
        if (hostParts[^1].Length < 2)
            return false; // TLD only one letter.

        if (mailAddress.User.Contains(' '))
            return false;
        if (mailAddress.User.Split('.').Any(p => p == string.Empty))
            return false; // Double dot or dot at end of user part.

        return true;
    }

    public static string GetUserInput()
    {
        string input;

        try
        {
            input = Console.ReadLine() ?? "Error";   
        }
        catch (Exception)
        {
            
            return "Error";
        }

        return input;
    }
}
