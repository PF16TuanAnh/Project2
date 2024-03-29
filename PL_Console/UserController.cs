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
        Console.Clear();
        string email;
        ConsoleKey key;

        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("            LOG IN");
            Console.WriteLine("\n================================");
            Console.WriteLine(" You can Enter 0 on email or password to turn back.");
            Console.Write(" Email: ");
            email = GetUserInput();
            Console.Write(" Password: ");
            var pass = string.Empty;
            
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            if (email == "0")
            {
                Console.Clear();
                break;
            }

            if (pass == "0")
            {
                Console.Clear();
                break;
            }

            if(userBL.VerifyEmailAndPassword(email, pass))
            {
                int? CandidateID = userBL.GetCandidateIDByEmail(email);
                int? RecruiterID = userBL.GetRecruiterIDByEmail(email);
                if (CandidateID != null)
                {
                    Console.Clear();
                    Menu.PrintMainMenu(2, CandidateID);
                    break;
                }
                else if (RecruiterID != null)
                {
                    Console.Clear();
                    Menu.PrintMainMenu(3, RecruiterID);
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Unexpected errors occurred! Couldn't retrieve the user info.");
                    break;
                }
            }
        }
    }
        
    public void Register()
    {
        Console.Clear();
        string email;
        var pass = string.Empty;
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
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("  Email is too long. Maximum characters allowed is 100.");
                Console.WriteLine("================================\n");
                Console.WriteLine("            REGISTER");
                Console.WriteLine("\n================================");
                
            }
            else if (email == "0")
            {
                Console.Clear();
                break;
            }
            else
            {
                if (IsValidEmail(email))
                {
                    if (!userBL.CheckIfEmailExisted(email))
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" Email has been used.");
                        Console.WriteLine("================================\n");
                        Console.WriteLine("            REGISTER");
                        Console.WriteLine("\n================================");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid email format! Please re-enter your email.");
                    Console.WriteLine("================================\n");
                    Console.WriteLine("            REGISTER");
                    Console.WriteLine("\n================================");
                }
            }
        }
        
        if (email != "0")
        {
            while (true)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("            REGISTER");
                Console.WriteLine("\n================================");
                Console.WriteLine(" You can Enter 0 on password to turn back.");
                Console.Write(" Password: ");
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);

                if(pass.Length > 100)
                {
                    Console.Clear();
                    Console.WriteLine(" Password is too long. Maximum characters allowed is 100.");
                }
                else
                {
                    break;
                }
            }  

            if(pass != "0")
            {
                Console.Clear();
                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("            REGISTER");
                    Console.WriteLine("\n================================");
                    Console.Write(" Name: ");
                    username = GetUserInput();
                    if(username.Length > 50)
                    {
                        Console.Clear();
                        Console.WriteLine(" Name is too long. Maximum characters allowed is 50.");
                    }
                    else
                    {
                        break;
                    }
                }  
                
                Console.Clear();
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
                    string userInput = GetUserInput();
                    switch (userInput)
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
                            Console.Clear();
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Invalid input! Please re-enter your option.");
                            break;
                    }

                    if (end == true)
                    {
                        break;
                    }
                }

                Console.Clear();
                while (true)
                {
                    end = false;
                    Console.WriteLine("================================\n");
                    Console.WriteLine("          Register As");
                    Console.WriteLine("\n================================");
                    Console.WriteLine(" 1) Candidate");
                    Console.WriteLine(" 2) Recruiter");
                    Console.WriteLine("================================");
                    Console.Write(" Enter the option number: ");
                    string userInput = GetUserInput();
                    switch (userInput)
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
                            Console.Clear();
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Invalid input! Please re-enter your option.");
                            break;
                    }

                    if (end == true)
                    {
                        break;
                    }
                }

                User newAccount = new User(username, email, pass, gender);
                int? ID = userBL.InsertNewUser(newAccount, role);

                if(role == 1)
                {
                    if(ID != null)
                    {
                        Menu.PrintMainMenu(2, ID);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" System Error! Returning to main menu.");
                    }
                }
                else
                {
                    if(ID != null)
                    {
                        Menu.PrintMainMenu(3, ID);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" System Error! Returning to main menu.");
                    }
                }
            }
            else
            {
                Console.Clear();
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
