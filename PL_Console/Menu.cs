namespace Pl_Console;
using System.Net.Mail;
using Persistence;
public class Menu
{
    public void StartMenu()
    {
        string choice;
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine("        RECRUITMENT APP");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Log in");
            Console.WriteLine(" 2) Register");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    LogInMenu();
                    break;
                case "2":
                    RegisterMenu();
                    break;
                case "0":
                    Console.WriteLine("================================"); 
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
    }

    public void LogInMenu()
    {
        string email;
        string password;
        int? CandidateID = 1;
        int? RecruiterID = null;
        bool EmailExisted = true;
        bool PasswordCorrected = true;

        Console.WriteLine("================================");
        Console.WriteLine("            LOG IN");
        Console.WriteLine("================================");
        Console.Write(" Email: ");
        email = Console.ReadLine() ?? "Error";
        Console.Write(" Password: ");
        password = Console.ReadLine() ?? "Error";

        while (true)
        {
            if (EmailExisted)
            {
                if (PasswordCorrected)
                {
                    if (CandidateID != null)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine(" Logged in as a Candidate.");
                        CandidateMenu(CandidateID);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine(" Logged in as a Recruiter.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("================================");
                    Console.WriteLine(" Password is incorrect! Please re-enter your password");
                    Console.Write(" Password: ");
                    password = Console.ReadLine() ?? "Error";
                    PasswordCorrected = true;
                }
            }
            else
            {
                Console.WriteLine("================================");
                Console.WriteLine(" Email doesn't exist! Please re-enter your emai.");
                Console.Write(" Email: ");
                email = Console.ReadLine() ?? "Error";
                EmailExisted = true;
            }
        }
    }

    public void RegisterMenu()
    {
        string email;
        string password;
        string username;
        string gender = "Other";
        int role = 1; // 1 = "Candidate", 2 = "Recruiter"
        string choice;
        bool end = false;
        int? CandidateID = 1;
        int? RecruiterID = null;

        Console.WriteLine("================================");
        Console.WriteLine("            REGISTER");
        Console.WriteLine("================================");
        while (true)
        {
            Console.Write(" Email: ");
            email = Console.ReadLine() ?? "Error";
            if (IsValidEmail(email))
            {
                break;
            }
            else
            {
                Console.WriteLine("\n Invalid email format! Please re-enter your emai.\n");
            }
        }  
        Console.Write(" Password: ");
        password = Console.ReadLine() ?? "Error";
        Console.Write(" Username: ");
        username = Console.ReadLine() ?? "Error";

        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine("          Your Gender");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Male");
            Console.WriteLine(" 2) Female");
            Console.WriteLine(" 3) Other");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
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
            Console.WriteLine("================================");
            Console.WriteLine("          Register As");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Candidate");
            Console.WriteLine(" 2) Recruiter");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
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

        if(role == 1)
        {
            if(CandidateID != null)
            {
                Console.WriteLine("================================");
                Console.WriteLine(" Logged in as a Candidate.");
                CandidateMenu(CandidateID);
            }
            else
            {
                Console.WriteLine("================================"); 
                Console.WriteLine(" System Error! Closing out.");
                System.Environment.Exit(0);
            }
        }
        else
        {
            if(RecruiterID != null)
            {
                Console.WriteLine("================================");
                Console.WriteLine(" Logged in as a Recruiter.");
                // RecruiterMenu(RecruiterID)
            }
            else
            {
                Console.WriteLine("================================"); 
                Console.WriteLine(" System Error! Closing out.");
                System.Environment.Exit(0);
            }
        }
    }

    public void CandidateMenu(int? CandidateID)
    {
        string username = "temp";
        string choice;
        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine(" Username: {0}", username);
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Create CV");
            Console.WriteLine(" 2) View CV");
            Console.WriteLine(" 3) Search Recruitment News");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    CreateCVMenu(CandidateID);
                    break;
                case "2":
                    ViewCVMenu(CandidateID);
                    break;
                case "3":
                    
                    break;
                case "0":
                    Console.WriteLine("================================"); 
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }
        }
    }

     public void RecruiterMenu(int? RecruiterID)
    {
        string username = "temp";
        string choice;
        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine(" Username: {0}", username);
            Console.WriteLine("================================");
            Console.WriteLine(" 1) View Personal Information");
            Console.WriteLine(" 2) Add Recruitment News");
            Console.WriteLine(" 3) View Your Recruitment News");
            Console.WriteLine(" 4) Search CVs");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    
                    break;
                case "2":
                    
                    break;
                case "3":
                    
                    break;
                case "4":
                    
                    break;
                case "0":
                    Console.WriteLine("================================"); 
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }
        }
    }

    public void CreateCVMenu(int? CandidateID)
    {
        string choice;
        bool end = false;

        string? FullName;
        string? CareerTitle;
        string? CareerObjective;
        string? BirthDate;
        string? PhoneNum;
        string? Email;
        string? SocialMedia;
        string? PersonalAddress;
        List<CVDetails>? CVDetails = null;

        Console.WriteLine("================================");
        Console.WriteLine("           CREATE CV");
        Console.WriteLine("================================");

        Console.Write(" Full Name       : ");
        FullName = Console.ReadLine() ?? "Error";
        Console.Write(" Career Title    : ");
        CareerTitle = Console.ReadLine() ?? "Error";
        Console.Write(" Career Objective: ");
        CareerObjective = Console.ReadLine() ?? "Error";
        Console.Write(" Date of Birth   : ");
        BirthDate = Console.ReadLine() ?? "Error";
        Console.Write(" Phone Number    : ");
        PhoneNum = Console.ReadLine() ?? "Error";
        Console.Write(" Email           : ");
        Email = Console.ReadLine() ?? "Error";
        Console.Write(" Social Media    : ");
        SocialMedia = Console.ReadLine() ?? "Error";
        Console.Write(" Address         : ");
        PersonalAddress = Console.ReadLine() ?? "Error";

        
        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine("              ADD");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) A Skill");
            Console.WriteLine(" 2) A Work Experience");
            Console.WriteLine(" 3) An Education");
            Console.WriteLine(" 4) An Activity");
            Console.WriteLine(" 5) A Certificate");
            Console.WriteLine(" 0) Done");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(1));
                    break;
                case "2":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(2));
                    break;
                case "3":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(3));
                    break;
                case "4":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(4));
                    break;
                case "5":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(5));
                    break;
                case "0":
                    Console.WriteLine("================================"); 
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

        CV newCV = new CV(FullName, CareerTitle, CareerObjective, BirthDate, PhoneNum, Email, SocialMedia, PersonalAddress, CVDetails);
    }

    public CVDetails AddCVDetails(int type)
    {
        string Title = "Skill";
        string? JobPosition;
        string? FromDate;
        string? ToDate;
        string? Association;
        string? Description;

        Console.WriteLine("================================");
        switch (type)
        {
            case 1:
                Console.WriteLine("             Skill");
                Title = "Skill";
                break;
            case 2:
                Console.WriteLine("        Work Experience");
                Title = "Work Experience";
                break;
            case 3:
                Console.WriteLine("           Education");
                Title = "Education";
                break;
            case 4:
                Console.WriteLine("            Activity");
                Title = "Activity";
                break;
            case 5:
                Console.WriteLine("          Certificate");
                Title = "Certificate";
                break;
            default:
                break;
        }
        Console.WriteLine("================================");

        Console.Write(" Job Position: ");
        JobPosition = Console.ReadLine() ?? "Error";
        Console.Write(" From        : ");
        FromDate = Console.ReadLine() ?? "Error";
        Console.Write(" To          : ");
        ToDate = Console.ReadLine() ?? "Error";
        Console.Write(" Association : ");
        Association = Console.ReadLine() ?? "Error";
        Console.Write(" Description : ");
        Description = Console.ReadLine() ?? "Error";

        return new CVDetails(Title, JobPosition, FromDate, ToDate, Association, Description);
    }

    public void ViewCVMenu(int? CandidateCV)
    {
        string choice;
        bool end = false;
        CV temp = new CV("test", "testTitle", "\n ufquorqoufhbqohtfouqhtoqhutoquhtq\n fqfqnoqfoi\n qefqfwqfqw", "11/20/2003", "28742742", "hfwfhei@gamil.com",
        "None", "", null);

        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine("            YOUR CV");
            Console.WriteLine("================================");
            Console.WriteLine(" Full Name       : {0}", temp.FullName);
            Console.WriteLine(" Career Title    : {0}", temp.CareerTitle);
            Console.WriteLine(" Career Objective: {0}", temp.CareerObjective);
            Console.WriteLine(" Date of Birth   : {0}", temp.BirthDate);
            Console.WriteLine(" Phone Number    : {0}", temp.PhoneNum);
            Console.WriteLine(" Email           : {0}", temp.Email);
            Console.WriteLine(" Social Media    : {0}", temp.SocialMedia);
            Console.WriteLine(" Address         : {0}", temp.PersonalAddress);
            Console.WriteLine(" \n Skills:\n");
            if(temp.CVDetails != null)
            {
                foreach (CVDetails detail in temp.CVDetails)
                {
                    if(detail.Title == "Skill")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Work Experiences:\n");
            if(temp.CVDetails != null)
            {
                foreach (CVDetails detail in temp.CVDetails)
                {
                    if(detail.Title == "Work Experience")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Educations:\n");
            if(temp.CVDetails != null)
            {
                foreach (CVDetails detail in temp.CVDetails)
                {
                    if(detail.Title == "Education")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Activities:\n");
            if(temp.CVDetails != null)
            {
                foreach (CVDetails detail in temp.CVDetails)
                {
                    if(detail.Title == "Activity")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Certifications:\n");
            if(temp.CVDetails != null)
            {
                foreach (CVDetails detail in temp.CVDetails)
                {
                    if(detail.Title == "Certificate")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine("================================");
            Console.WriteLine(" 1) FullName");
            Console.WriteLine(" 2) Career Title");
            Console.WriteLine(" 3) Career Objective");
            Console.WriteLine(" 4) Date of Birth");
            Console.WriteLine(" 5) Phone Number");
            Console.WriteLine(" 6) Email");
            Console.WriteLine(" 7) Social Media");
            Console.WriteLine(" 8) Address");
            Console.WriteLine(" 9) Skills, Work Experiences, Educations, Activities,Cartifications");
            Console.WriteLine(" 0) Return");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number to change the details or to return: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    Console.WriteLine("================================");
                    Console.Write(" Full Name       : ");
                    temp.FullName = Console.ReadLine() ?? "Error";
                    break;
                case "2":
                    Console.WriteLine("================================");
                    Console.Write(" Career Title    : ");
                    temp.CareerTitle = Console.ReadLine() ?? "Error";
                    break;
                case "3":
                    Console.WriteLine("================================");
                    Console.Write(" Career Objective: ");
                    temp.CareerObjective = Console.ReadLine() ?? "Error";
                    break;
                case "4":
                    Console.WriteLine("================================");
                    Console.Write(" Date of Birth   : ");
                    temp.BirthDate = Console.ReadLine() ?? "Error";
                    break;
                case "5":
                    Console.WriteLine("================================");
                    Console.Write(" Phone Number    : ");
                    temp.PhoneNum = Console.ReadLine() ?? "Error";
                    break;
                case "6":
                    Console.WriteLine("================================");
                    Console.Write(" Email           : ");
                    temp.Email = Console.ReadLine() ?? "Error";
                    break;
                case "7":
                    Console.WriteLine("================================");
                    Console.Write(" Social Media    : ");
                    temp.SocialMedia = Console.ReadLine() ?? "Error";
                    break;
                case "8":
                    Console.WriteLine("================================");
                    Console.Write(" Address         : ");
                    temp.PersonalAddress = Console.ReadLine() ?? "Error";
                    break;
                case "9":
                    temp.CVDetails = UpdateCVDetails(temp.CVDetails);
                    break;
                case "0":
                    while(true)
                    {
                        
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) Confirm changes");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        choice = Console.ReadLine() ?? "Error";  
                        switch (choice)
                        {
                            case "1":
                                end = true;
                                break;
                            case "0":
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
    }

    public List<CVDetails> UpdateCVDetails(List<CVDetails>? CVDetails)
    {
        string choice;
        bool end = false;

        while (true)
        {
            bool done = false;
            Console.WriteLine("================================"); 
            Console.WriteLine(" Skills:\n");
            if(CVDetails != null)
            {
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Skill")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Work Experiences:\n");
            if(CVDetails != null)
            {
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Work Experience")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Educations:\n");
            if(CVDetails != null)
            {
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Education")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Activities:\n");
            if(CVDetails != null)
            {
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Activity")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine(" Certifications:\n");
            if(CVDetails != null)
            {
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Certificate")
                    {
                        Console.WriteLine("  Job Position: {0}", detail.JobPosition);
                        Console.WriteLine("  From        : {0}", detail.FromDate);
                        Console.WriteLine("  To          : {0}", detail.ToDate);
                        Console.WriteLine("  Association : {0}", detail.Association);
                        Console.WriteLine("  Description : {0}\n", detail.Description);
                    }
                }
            }
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Add");
            Console.WriteLine(" 2) Change");
            Console.WriteLine(" 3) Delete");
            Console.WriteLine(" 0) Done");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1": // Add more CVDetails
                    while (true)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine("              ADD");
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) A Skill");
                        Console.WriteLine(" 2) A Work Experience");
                        Console.WriteLine(" 3) An Education");
                        Console.WriteLine(" 4) An Activity");
                        Console.WriteLine(" 5) A Certificate");
                        Console.WriteLine(" 0) Done");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        choice = Console.ReadLine() ?? "Error";   
                        switch (choice)
                        {
                            case "1":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(1));
                                break;
                            case "2":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(2));
                                break;
                            case "3":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(3));
                                break;
                            case "4":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(4));
                                break;
                            case "5":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(5));
                                break;
                            case "0":
                                Console.WriteLine("================================"); 
                                done = true;
                                break;
                            default:
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                break;
                        }

                        if (done == true)
                        {
                            break;
                        }
                    }
                    break;
                case "2": // Change info of different CVDetails
                    if(CVDetails != null)
                    {
                        while (true)
                        {
                            Console.WriteLine("================================");
                            Console.WriteLine("            CHANGE");
                            Console.WriteLine("================================");
                            Console.WriteLine(" 1) A Skill");
                            Console.WriteLine(" 2) A Work Experience");
                            Console.WriteLine(" 3) An Education");
                            Console.WriteLine(" 4) An Activity");
                            Console.WriteLine(" 5) A Certificate");
                            Console.WriteLine(" 0) Done");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            choice = Console.ReadLine() ?? "Error";   
                            switch (choice)
                            {
                                case "1":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Skill: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Skill")
                                        {
                                            Console.WriteLine("================================");
                                            Console.Write(" Job Position: ");
                                            detail.JobPosition = Console.ReadLine() ?? "Error";
                                            Console.Write(" From        : ");
                                            detail.FromDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" To          : ");
                                            detail.ToDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" Association : ");
                                            detail.Association = Console.ReadLine() ?? "Error";
                                            Console.Write(" Description : ");
                                            detail.Description = Console.ReadLine() ?? "Error";
                                        }
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Work Experience: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Work Experience")
                                        {
                                            Console.WriteLine("================================");
                                            Console.Write(" Job Position: ");
                                            detail.JobPosition = Console.ReadLine() ?? "Error";
                                            Console.Write(" From        : ");
                                            detail.FromDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" To          : ");
                                            detail.ToDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" Association : ");
                                            detail.Association = Console.ReadLine() ?? "Error";
                                            Console.Write(" Description : ");
                                            detail.Description = Console.ReadLine() ?? "Error";
                                        }
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Education: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Education")
                                        {
                                            Console.WriteLine("================================");
                                            Console.Write(" Job Position: ");
                                            detail.JobPosition = Console.ReadLine() ?? "Error";
                                            Console.Write(" From        : ");
                                            detail.FromDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" To          : ");
                                            detail.ToDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" Association : ");
                                            detail.Association = Console.ReadLine() ?? "Error";
                                            Console.Write(" Description : ");
                                            detail.Description = Console.ReadLine() ?? "Error";
                                        }
                                    }
                                    break;
                                case "4":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Activity: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Activity")
                                        {
                                            Console.WriteLine("================================");
                                            Console.Write(" Job Position: ");
                                            detail.JobPosition = Console.ReadLine() ?? "Error";
                                            Console.Write(" From        : ");
                                            detail.FromDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" To          : ");
                                            detail.ToDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" Association : ");
                                            detail.Association = Console.ReadLine() ?? "Error";
                                            Console.Write(" Description : ");
                                            detail.Description = Console.ReadLine() ?? "Error";
                                        }
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Certificate: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Certificate")
                                        {
                                            Console.WriteLine("================================");
                                            Console.Write(" Job Position: ");
                                            detail.JobPosition = Console.ReadLine() ?? "Error";
                                            Console.Write(" From        : ");
                                            detail.FromDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" To          : ");
                                            detail.ToDate = Console.ReadLine() ?? "Error";
                                            Console.Write(" Association : ");
                                            detail.Association = Console.ReadLine() ?? "Error";
                                            Console.Write(" Description : ");
                                            detail.Description = Console.ReadLine() ?? "Error";
                                        }
                                    }
                                    break;
                                case "0":
                                    Console.WriteLine("================================"); 
                                    done = true;
                                    break;
                                default:
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                    break;
                            }

                            if (done == true)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" There's nothing to change!");
                    }
                    break;
                case "3": // 'Delete' CVDetails
                    if(CVDetails != null)
                    {
                        while (true)
                        {
                            Console.WriteLine("================================");
                            Console.WriteLine("             DELETE");
                            Console.WriteLine("================================");
                            Console.WriteLine(" 1) A Skill");
                            Console.WriteLine(" 2) A Work Experience");
                            Console.WriteLine(" 3) An Education");
                            Console.WriteLine(" 4) An Activity");
                            Console.WriteLine(" 5) A Certificate");
                            Console.WriteLine(" 0) Done");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            choice = Console.ReadLine() ?? "Error";   
                            switch (choice)
                            {
                                case "1":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Skill: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Skill")
                                        {
                                            detail.JobPosition = "";
                                            detail.Title = "";
                                            detail.FromDate = "";
                                            detail.ToDate = "";
                                            detail.Association = "";
                                            detail.Description = "";
                                        }
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Work Experience: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Work Experience")
                                        {
                                            detail.JobPosition = "";
                                            detail.Title = "";
                                            detail.FromDate = "";
                                            detail.ToDate = "";
                                            detail.Association = "";
                                            detail.Description = "";
                                        }
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Education: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Education")
                                        {
                                            detail.JobPosition = "";
                                            detail.Title = "";
                                            detail.FromDate = "";
                                            detail.ToDate = "";
                                            detail.Association = "";
                                            detail.Description = "";
                                        }
                                    }
                                    break;
                                case "4":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Activity: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Activity")
                                        {
                                            detail.JobPosition = "";
                                            detail.Title = "";
                                            detail.FromDate = "";
                                            detail.ToDate = "";
                                            detail.Association = "";
                                            detail.Description = "";
                                        }
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("================================");
                                    Console.Write(" Enter the Job Position of the Certificate: ");
                                    choice = Console.ReadLine() ?? "";
                                    foreach (CVDetails detail in CVDetails)
                                    {
                                        if(detail.JobPosition!.ToUpper().Contains(choice.ToUpper()) && detail.Title == "Certificate")
                                        {
                                            detail.JobPosition = "";
                                            detail.Title = "";
                                            detail.FromDate = "";
                                            detail.ToDate = "";
                                            detail.Association = "";
                                            detail.Description = "";
                                        }
                                    }
                                    break;
                                case "0":
                                    Console.WriteLine("================================"); 
                                    done = true;
                                    break;
                                default:
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                    break;
                            }

                            if (done == true)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" There's nothing to delete!");
                    }
                    break;
                case "0":
                    Console.WriteLine("================================"); 
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

        return CVDetails!;
    }

    public void SearchRecruitNewsMenu(int? CandidateID)
    {
        string choice;
        bool end = false;

        List<RecruitNews> recruitNews = new List<RecruitNews>();
        recruitNews.Add(new RecruitNews("test", "10/12/2023", "Based on Agreement", "Offline", "All", "4-6 people",
        "Staff", "5 years of experience", "Ha Noi", "Marketing"));
        recruitNews.Add(new RecruitNews("test1", "5/7/2023", "Based on Agreement", "Offline", "All", "4-6 people",
        "Staff", "5 years of experience", "Ha Noi", "Marketing"));
        recruitNews.Add(new RecruitNews("test3", "8/10/2022", "1000$ - 3000$", "Online", "All", "1-3 people",
        "Staff", "5 years of experience", "Binh Dinh", "Marketing"));

        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine("          SEARCH VIA");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Profession");
            Console.WriteLine(" 2) Salary Range");
            Console.WriteLine(" 2) City Address");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    
                    break;
                case "2":
                    
                    break;
                case "3":
                    
                    break;
                case "0":
                    Console.WriteLine("================================"); 
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
    }

    public void DisplaySearchedNews(List<RecruitNews> recruitNews, int? CandidateID)
    {  
        string choice;

        while (true)
        {
            int count = 0;
            Console.WriteLine("================================");
            Console.WriteLine("       RECRUITMENT NEWS");
            Console.WriteLine("================================");
            foreach (RecruitNews news in recruitNews)
            {
                count++;
                Console.WriteLine(" {0}) Name: {1}", count, news.NewsName);
            }
            Console.WriteLine("================================");
            Console.Write(" Enter the position of news you like to view or 0 to return: ");
            choice = Console.ReadLine() ?? "Error";

            if(choice == "0")
            {
                break;
            }

            bool success = int.TryParse(choice, out int position);
            if(success)
            {
                
            }
            else
            {
                Console.WriteLine("================================");
                Console.WriteLine(" Invalid choice! Please re-enter your option.");
            }
        }
    }

    public void SearchedNewsDetails(RecruitNews news, int? CandidateID)
    {
        string choice;
        bool end = false;

        while (true)
        {
            Console.WriteLine("================================");
            Console.WriteLine("          NEWS DETAILS");
            Console.WriteLine("================================");

            Console.WriteLine("================================");
            Console.WriteLine(" 1) Profession");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            choice = Console.ReadLine() ?? "Error";   
            switch (choice)
            {
                case "1":
                    
                    break;
                case "2":
                    
                    break;
                case "3":
                    
                    break;
                case "0":
                    Console.WriteLine("================================"); 
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
    }

    public bool IsValidEmail(string email)
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
}