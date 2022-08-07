namespace Pl_Console;
using BL;
using System.Net.Mail;
using Persistence;
using System.Text.RegularExpressions;
using ConsoleTables;
public class Menu
{
    private static readonly Regex NumericRegex = new Regex(@"^[0-9]*$");
    private UserBL userBL;
    private CandidateBL candidateBL;
    private RecruiterBL recruiterBL;

    public Menu()
    {
        userBL = new UserBL();
        candidateBL = new CandidateBL();
        recruiterBL = new RecruiterBL();
    }

    public void StartMenu()
    {
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        RECRUITMENT APP");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Log in");
            Console.WriteLine(" 2) Register");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");  
            switch (GetUserInput())
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

        Console.WriteLine("================================\n");
        Console.WriteLine("            LOG IN");
        Console.WriteLine("\n================================");
        Console.WriteLine(" You can Enter 0 on email or password to turn back.");
        Console.Write(" Email: ");
        email = GetUserInput();
        Console.Write(" Password: ");
        password = GetUserInput();

        while (true)
        {
            if (email == "0")
            {
                break;
            }

            if (password == "0")
            {
                break;
            }

            User user = userBL.GetUserByEmail(email);

            if (user != null)
            {
                if (password == "0")
                {
                    break;
                }

                password = userBL.GetHashString(password);

                if (password == user.Password)
                {
                    int? CandidateID = userBL.GetCandidateIDByEmail(email);
                    int? RecruiterID = userBL.GetRecruiterIDByEmail(email);
                    string? username = userBL.GetUsernameByEmailForRecruiter(email);
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
                        RecruiterMenu(RecruiterID, username);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("================================");
                    Console.WriteLine(" Password is incorrect! Please re-enter your password.");
                    Console.Write(" Password: ");
                    password = GetUserInput();
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

    public void RegisterMenu()
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
            Console.Write(" Email: ");
            email = GetUserInput();
            if(email.Length > 100)
            {
                Console.WriteLine("\n Email is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                if (IsValidEmail(email))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n Invalid email format! Please re-enter your emai.\n");
                }
            }
        }

        while (true)
        {
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
                CandidateMenu(ID);
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
            if(ID != null)
            {
                Console.WriteLine("================================");
                Console.WriteLine(" Logged in as a Recruiter.");
                RecruiterMenu(ID, username);
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
        while (true)
        {
            Candidate candidate = candidateBL.GetCandidateByID(CandidateID);
            if(candidate != null)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine(" Username: {0}", candidate.Username);
                Console.WriteLine("\n================================");
                if (candidate.CandidateCV != null)
                {
                    Console.WriteLine(" 1) View CV");
                }
                else
                {
                    Console.WriteLine(" 1) Create CV");
                }
                Console.WriteLine(" 2) Search Recruitment News");
                Console.WriteLine(" 0) Exit");
                Console.WriteLine("================================");
                Console.Write(" Enter the option number: ");
                switch (GetUserInput())
                {
                    case "1":
                        if (candidate.CandidateCV != null)
                        {
                            ViewCVMenu(candidate.CandidateCV);
                        }
                        else
                        {
                            CreateCVMenu(CandidateID);
                        }
                        break;
                    case "2":
                        SearchRecruitNewsMenu(CandidateID);
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
            else
            {
                Console.WriteLine("================================"); 
                Console.WriteLine(" Couldn't retrieve the user info. Unexpected problems might have occurred to the connection to the database.");
                break;
            }
            
        }
    }

    public void CreateCVMenu(int? CandidateID)
    {
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

        Console.WriteLine("================================\n");
        Console.WriteLine("           CREATE CV");
        Console.WriteLine("\n================================");

        while (true)
        {
            Console.Write(" Full Name       : ");
            FullName = GetUserInput();
            if(FullName.Length > 100)
            {
                Console.WriteLine("\n Full Name is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Career Title    : ");
            CareerTitle = GetUserInput();
            if(CareerTitle.Length > 50)
            {
                Console.WriteLine("\n Career Title is too long. Maximum characters allowed is 50\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Career Objective: ");
            CareerObjective = GetUserInput();
            if(CareerObjective.Length > 5000)
            {
                Console.WriteLine("\n Career Objective is too long. Maximum characters allowed is 5000\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Date of Birth   : ");
            BirthDate = GetUserInput();
            if(BirthDate.Length > 20)
            {
                Console.WriteLine("\n Date of Birth is too long. Maximum characters allowed is 20\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Phone Number    : ");
            PhoneNum = GetUserInput();
            if(PhoneNum.Length > 10)
            {
                Console.WriteLine("\n Phone Number is too long. Maximum characters allowed is 10.\n");
            }
            else if (string.IsNullOrEmpty(PhoneNum))
            {
                Console.WriteLine("\n Phone Number can't be left empty.\n");
            }
            else
            {
                if (NumericRegex.IsMatch(PhoneNum))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n Phone Number is numbers only.\n");
                }
            }
        }
        
        while (true)
        {
            Console.Write(" Email           : ");
            Email = GetUserInput();
            if(Email.Length > 100)
            {
                Console.WriteLine("\n Email is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                if (IsValidEmail(Email))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n Invalid email format! Please re-enter your emai.\n");
                }
            }
        }

        while (true)
        {
            Console.Write(" Social Media    : ");
            SocialMedia = GetUserInput();
            if(SocialMedia.Length > 2000)
            {
                Console.WriteLine("\n Social Media is too long. Maximum characters allowed is 2000\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Address         : ");
            PersonalAddress = GetUserInput();
            if(PersonalAddress.Length > 5000)
            {
                Console.WriteLine("\n Address is too long. Maximum characters allowed is 5000\n");
            }
            else
            {
                break;
            }
        }
            
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("              ADD");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) A Skill");
            Console.WriteLine(" 2) A Work Experience");
            Console.WriteLine(" 3) An Education");
            Console.WriteLine(" 4) An Activity");
            Console.WriteLine(" 5) A Certificate");
            Console.WriteLine(" 0) Done");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
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
        candidateBL.CreateNewCV(newCV, CandidateID);
    }

    public CVDetails AddCVDetails(int type)
    {
        string Title = "Skill";
        string? JobPosition;
        string? FromDate;
        string? ToDate;
        string? Association;
        string? Description;

        Console.WriteLine("================================\n");
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
        Console.WriteLine("\n================================");

        while (true)
        {
            Console.Write(" Job Position: ");
            JobPosition = GetUserInput();
            if(JobPosition.Length > 100)
            {
                Console.WriteLine("\n Job Position is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" From        : ");
            FromDate = GetUserInput();
            if(FromDate.Length > 50)
            {
                Console.WriteLine("\n From Date is too long. Maximum characters allowed is 50\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" To          : ");
            ToDate = GetUserInput();
            if(ToDate.Length > 50)
            {
                Console.WriteLine("\n To Date is too long. Maximum characters allowed is 50\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Association : ");
            Association = GetUserInput();
            if(Association.Length > 100)
            {
                Console.WriteLine("\n Association is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        
        while (true)
        {
            Console.Write(" Description : ");
            Description = GetUserInput();
            if(Description.Length > 5000)
            {
                Console.WriteLine("\n Description is too long. Maximum characters allowed is 5000\n");
            }
            else
            {
                break;
            }
        }
        

        return new CVDetails(Title, JobPosition, FromDate, ToDate, Association, Description);
    }

    public void ViewCVMenu(CV cv)
    {
        bool end = false;

        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("            YOUR CV");
            Console.WriteLine("\n================================");
            Console.WriteLine(" Full Name       : {0}", cv.FullName);
            Console.WriteLine(" Career Title    : {0}", cv.CareerTitle);
            Console.WriteLine(" Career Objective: {0}", cv.CareerObjective);
            Console.WriteLine(" Date of Birth   : {0}", cv.BirthDate);
            Console.WriteLine(" Phone Number    : {0}", cv.PhoneNum);
            Console.WriteLine(" Email           : {0}", cv.Email);
            Console.WriteLine(" Social Media    : {0}", cv.SocialMedia);
            Console.WriteLine(" Address         : {0}", cv.PersonalAddress);
            Console.WriteLine(" \n Skills:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Skill")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Work Experiences:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Work Experience")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Educations:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Education")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Activities:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Activity")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Certifications:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Certificate")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
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
            Console.WriteLine(" 9) Skills, Work Experiences, Educations, Activities, Certifications");
            Console.WriteLine(" 0) Return");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number to change the details or to return: ");
            switch (GetUserInput())
            {
                case "1":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Full Name       : ");
                        string FullName = GetUserInput();
                        if(FullName.Length > 100)
                        {
                            Console.WriteLine("\n Full Name is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            cv.FullName = FullName;
                            break;
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Career Title       : ");
                        string CareerTitle = GetUserInput();
                        if(CareerTitle.Length > 50)
                        {
                            Console.WriteLine("\n Career Title is too long. Maximum characters allowed is 50\n");
                        }
                        else
                        {
                            cv.CareerTitle = CareerTitle;
                            break;
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Career Objective: ");
                        string CareerObjective = GetUserInput();
                        if(CareerObjective.Length > 5000)
                        {
                            Console.WriteLine("\n Career Objective is too long. Maximum characters allowed is 5000\n");
                        }
                        else
                        {
                            cv.CareerObjective = CareerObjective;
                            break;
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Date of Birth   : ");
                        string BirthDate = GetUserInput();
                        if(BirthDate.Length > 20)
                        {
                            Console.WriteLine("\n Date of Birth is too long. Maximum characters allowed is 20\n");
                        }
                        else
                        {
                            cv.BirthDate = BirthDate;
                            break;
                        }
                    }
                    break;
                case "5":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Phone Number    : ");
                        string? _PhoneNum = GetUserInput();

                        if(_PhoneNum.Length > 10)
                        {
                            Console.WriteLine("\n Phone Number is too long. Maximum characters allowed is 10\n");
                        }
                        else if (string.IsNullOrEmpty(_PhoneNum))
                        {
                            Console.WriteLine("\n Phone Number can't be left empty.\n");
                        }
                        else
                        {
                            if (NumericRegex.IsMatch(_PhoneNum))
                            {
                                cv.PhoneNum = _PhoneNum;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n Phone number is numbers only.\n");
                            }
                        }
                    }
                    break;
                case "6":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Email           : ");
                        string Email = GetUserInput();
                        if(Email.Length > 100)
                        {
                            Console.WriteLine("\n Email is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            if (IsValidEmail(Email))
                            {
                                cv.Email = Email;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n Invalid email format! Please re-enter your emai.\n");
                            }
                        }
                    }
                    break;
                case "7":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Social Media    : ");
                        string SocialMedia = GetUserInput();
                        if(SocialMedia.Length > 2000)
                        {
                            Console.WriteLine("\n Social Media is too long. Maximum characters allowed is 2000\n");
                        }
                        else
                        {
                            cv.SocialMedia = SocialMedia;
                            break;
                        }
                    }
                    break;
                case "8":
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Address         : ");
                        string PersonalAddress = GetUserInput();
                        if(PersonalAddress.Length > 5000)
                        {
                            Console.WriteLine("\n Address is too long. Maximum characters allowed is 5000\n");
                        }
                        else
                        {
                            cv.PersonalAddress = PersonalAddress;
                            break;
                        }
                    }
                    break;
                case "9":
                    cv.CVDetails = UpdateCVDetails(cv.CVDetails);
                    break;
                case "0":
                    while(true)
                    {
                        
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) Confirm changes");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (GetUserInput())
                        {
                            case "1":
                                candidateBL.UpdateCVInfo(cv);
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
        bool end = false;

        while (true)
        {
            bool done = false;
            Console.WriteLine("================================"); 
            Console.WriteLine(" Skills:\n");
            if(CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Skill")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Work Experiences:\n");
            if(CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Work Experience")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Educations:\n");
            if(CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Education")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Activities:\n");
            if(CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Activity")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Certifications:\n");
            if(CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in CVDetails)
                {
                    if(detail.Title == "Certificate")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Add");
            Console.WriteLine(" 2) Change");
            Console.WriteLine(" 3) Delete");
            Console.WriteLine(" 0) Done");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1": // Add more CVDetails
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("              ADD");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" 1) A Skill");
                        Console.WriteLine(" 2) A Work Experience");
                        Console.WriteLine(" 3) An Education");
                        Console.WriteLine(" 4) An Activity");
                        Console.WriteLine(" 5) A Certificate");
                        Console.WriteLine(" 0) Done");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (GetUserInput())
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
                            Console.WriteLine("================================\n");
                            Console.WriteLine("            CHANGE");
                            Console.WriteLine("\n================================");
                            Console.WriteLine(" 1) A Skill");
                            Console.WriteLine(" 2) A Work Experience");
                            Console.WriteLine(" 3) An Education");
                            Console.WriteLine(" 4) An Activity");
                            Console.WriteLine(" 5) A Certificate");
                            Console.WriteLine(" 0) Done");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            switch (GetUserInput())
                            {
                                case "1":
                                    CVDetails = ChangeCVDetails(CVDetails, "Skill");
                                    break;
                                case "2":
                                    CVDetails = ChangeCVDetails(CVDetails, "Work Experience");
                                    break;
                                case "3":
                                    CVDetails = ChangeCVDetails(CVDetails, "Education");
                                    break;
                                case "4":
                                    CVDetails = ChangeCVDetails(CVDetails, "Activity");
                                    break;
                                case "5":
                                    CVDetails = ChangeCVDetails(CVDetails, "Certificate");
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
                            Console.WriteLine("================================\n");
                            Console.WriteLine("             DELETE");
                            Console.WriteLine("\n================================");
                            Console.WriteLine(" 1) A Skill");
                            Console.WriteLine(" 2) A Work Experience");
                            Console.WriteLine(" 3) An Education");
                            Console.WriteLine(" 4) An Activity");
                            Console.WriteLine(" 5) A Certificate");
                            Console.WriteLine(" 0) Done");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            string choice = GetUserInput();   
                            switch (choice)
                            {
                                case "1":
                                    CVDetails = DeleteCVDetails(CVDetails, "Skill");
                                    break;
                                case "2":
                                    CVDetails = DeleteCVDetails(CVDetails, "Work Experience");
                                    break;
                                case "3":
                                    CVDetails = DeleteCVDetails(CVDetails, "Education");
                                    break;
                                case "4":
                                    CVDetails = DeleteCVDetails(CVDetails, "Activity");
                                    break;
                                case "5":
                                    CVDetails = DeleteCVDetails(CVDetails, "Certificate");
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
        bool end = false;

        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("          SEARCH VIA");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Profession");
            Console.WriteLine(" 2) Salary Range");
            Console.WriteLine(" 3) City Address");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                    DisplaySearchedNews(SearchRecruitNewsViaProfession(), CandidateID);
                    break;
                case "2":
                    DisplaySearchedNews(SearchRecruitNewsViaSalaryRange(), CandidateID);
                    break;
                case "3":
                    DisplaySearchedNews(SearchRecruitNewsViaCity(), CandidateID);
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
    }

    public List<RecruitNews> SearchRecruitNewsViaProfession()
    {
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("          PROFESSION");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Seller");
            Console.WriteLine(" 2) Translator");
            Console.WriteLine(" 3) Journalist");
            Console.WriteLine(" 4) Post and Telecommunications");
            Console.WriteLine(" 5) Insurance");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                    return recruiterBL.GetNewsByProfession("Seller");
                case "2":
                    return recruiterBL.GetNewsByProfession("Translator");
                case "3":
                    return recruiterBL.GetNewsByProfession("Journalist");
                case "4":
                    return recruiterBL.GetNewsByProfession("Post and Telecommunications");
                case "5":
                    return recruiterBL.GetNewsByProfession("Insurance");
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }
        }
    }

    public List<RecruitNews> SearchRecruitNewsViaSalaryRange()
    {
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("            SALARY");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Below 3 million");
            Console.WriteLine(" 2) 3 - 5 million");
            Console.WriteLine(" 3) 5 - 7 million");
            Console.WriteLine(" 4) 7 - 10 million");
            Console.WriteLine(" 5) Higher than 10 million");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                    return recruiterBL.GetNewsBySalaryRange("Below 3 million");
                case "2":
                    return recruiterBL.GetNewsBySalaryRange("3 - 5 million");
                case "3":
                    return recruiterBL.GetNewsBySalaryRange("5 - 7 million");
                case "4":
                    return recruiterBL.GetNewsBySalaryRange("7 - 10 million");
                case "5":
                    return recruiterBL.GetNewsBySalaryRange("Higher than 10 million");
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }
        }
    }

    public List<RecruitNews> SearchRecruitNewsViaCity()
    {
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("             CITY");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Ha Noi");
            Console.WriteLine(" 2) Ho Chi Minh");
            Console.WriteLine(" 3) Binh Duong");
            Console.WriteLine(" 4) Bac Ninh");
            Console.WriteLine(" 5) Dong Nai");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                    return recruiterBL.GetNewsByCityAddress("Ha Noi");
                case "2":
                    return recruiterBL.GetNewsByCityAddress("Ho Chi Minh");
                case "3":
                    return recruiterBL.GetNewsByCityAddress("Binh Duong");
                case "4":
                    return recruiterBL.GetNewsByCityAddress("Bac Ninh");
                case "5":
                    return recruiterBL.GetNewsByCityAddress("Dong Nai");
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }
        }
    }

    public void DisplaySearchedNews(List<RecruitNews> recruitNews, int? CandidateID)
    {  
        if (recruitNews != null)
        {
            while (true)
            {
                var table = new ConsoleTable("Pos", "Name");

                int count = 0;
                Console.WriteLine("================================\n");
                Console.WriteLine("       RECRUITMENT NEWS");
                Console.WriteLine("\n================================");
                foreach (RecruitNews news in recruitNews)
                {
                    table.AddRow(++count, news.NewsName);
                }
                table.Write(Format.Alternative);
                Console.WriteLine("================================");
                Console.Write(" Enter the position of news you like to view or 0 to return: ");
                string choice = GetUserInput();

                if(choice == "0")
                {
                    break;
                }

                bool success = int.TryParse(choice, out int position);
                if(success)
                {
                    SearchedNewsDetails(recruitNews[position - 1], CandidateID);
                }
                else
                {
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                }
            }
        }
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" No results!");
        }
    }

    public void SearchedNewsDetails(RecruitNews news, int? CandidateID)
    {
        bool end = false;
        
        Recruiter recruiter = recruiterBL.GetRecruiterByNewsID(news.NewsID);
        if (recruiter != null)
        {
            while (true)
            {
                bool IsApplied = candidateBL.IsApplied(CandidateID, news.NewsID);
                Console.WriteLine("================================\n");
                Console.WriteLine("          NEWS DETAILS");
                Console.WriteLine("\n================================");
                Console.WriteLine(" Name: {0}", news.NewsName);
                Console.WriteLine(" Deadline: {0}", news.DeadLine);
                Console.WriteLine(" Recruiter: {0}", recruiter.Username);
                Console.WriteLine(" Recruiter's Number: {0}", recruiter.PhoneNum);
                Console.WriteLine(" SalaryRange: {0}", news.SalaryRange);
                Console.WriteLine(" Form of Employment: {0}", news.FormOfEmploy);
                Console.WriteLine(" Gender: {0}", news.Gender);
                Console.WriteLine(" Hiring Amount: {0}", news.HiringAmount);
                Console.WriteLine(" Hiring Position: {0}", news.HiringPosition);
                Console.WriteLine(" Required Experience: {0}", news.RequiredExp);
                Console.WriteLine(" City Address: {0}", news.CityAddress);
                Console.WriteLine(" Company: {0}", recruiter.CompanyName);
                Console.WriteLine(" Business Field: {0}", recruiter.BusinessField);

                Console.WriteLine("================================");
                if(IsApplied)
                {
                    Console.WriteLine(" Applied!");
                }
                else
                {
                    Console.WriteLine(" 1) Apply");
                }
                Console.WriteLine(" 0) Exit");
                Console.WriteLine("================================");
                Console.Write(" Enter the option number: ");
                switch (GetUserInput())
                {
                    case "1":
                        if(!IsApplied)
                        {
                            candidateBL.ApplyToNews(CandidateID, news.NewsID);
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
        }
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Couldn't retrieve all details of the recruitment news.");
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

    public string GetUserInput()
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

    public List<CVDetails> ChangeCVDetails(List<CVDetails> CVDetails, string type)
    {
        int count = 0;
        Console.WriteLine("================================");
        Console.Write(" Enter the number position of the {0}: ", type);
        string choice = GetUserInput();
        
        foreach (CVDetails detail in CVDetails)
        {
            if(detail.Title == type)
            {
                count++;
            }
            
            if(int.TryParse(choice, out int pos) && count == pos && detail.Title == type)
            {
                Console.WriteLine("================================");
                while (true)
                {
                    Console.Write(" Job Position: ");
                    detail.JobPosition = GetUserInput();
                    if(detail.JobPosition.Length > 100)
                    {
                        Console.WriteLine("\n Job Position is too long. Maximum characters allowed is 100\n");
                    }
                    else
                    {
                        break;
                    }
                }
                
                while (true)
                {
                    Console.Write(" From        : ");
                    detail.FromDate = GetUserInput();
                    if(detail.FromDate.Length > 50)
                    {
                        Console.WriteLine("\n From Date is too long. Maximum characters allowed is 50\n");
                    }
                    else
                    {
                        break;
                    }
                }
                
                while (true)
                {
                    Console.Write(" To          : ");
                    detail.ToDate = GetUserInput();
                    if(detail.ToDate.Length > 50)
                    {
                        Console.WriteLine("\n To Date is too long. Maximum characters allowed is 50\n");
                    }
                    else
                    {
                        break;
                    }
                }
                
                while (true)
                {
                    Console.Write(" Association : ");
                    detail.Association = GetUserInput();
                    if(detail.Association.Length > 100)
                    {
                        Console.WriteLine("\n Association is too long. Maximum characters allowed is 100\n");
                    }
                    else
                    {
                        break;
                    }
                }
                
                while (true)
                {
                    Console.Write(" Description : ");
                    detail.Description = GetUserInput();
                    if(detail.Description.Length > 5000)
                    {
                        Console.WriteLine("\n Description is too long. Maximum characters allowed is 5000\n");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return CVDetails;
    }

    public List<CVDetails> DeleteCVDetails(List<CVDetails> CVDetails, string type)
    {
        int count = 0;
        Console.WriteLine("================================");
        Console.Write(" Enter the number position of the {0}: ", type);
        string choice = GetUserInput();
        
        foreach (CVDetails detail in CVDetails)
        {
            if(detail.Title == type)
            {
                count++;
            }
            
            if(int.TryParse(choice, out int pos) && count == pos && detail.Title == type)
            {
                detail.JobPosition = "";
                detail.Title = "";
                detail.FromDate = "";
                detail.ToDate = "";
                detail.Association = "";
                detail.Description = "";
            }
        }

        return CVDetails;
    }

    // NCT
        public void RecruiterMenu(int? RecruiterID, string? username)   
    {
        while (true)
        {
            Recruiter recruiter = recruiterBL.GetRecruiterByID(RecruiterID);
            // RecruitNews recruitnew = recruiterBL.GetRecruitNewByID(RecruiterID);
            if(recruiter != null)
            {
            Console.WriteLine("================================\n");
            Console.WriteLine(" Username: {0}", username);
            Console.WriteLine("\n================================");
            if (recruiter.PhoneNum != null)
                {
                    Console.WriteLine(" 1) View Personal Information");
                }
            else
                {
                    Console.WriteLine(" 1) Insert Personal Information");
                }
            Console.WriteLine(" 2) Add Recruitment News");
            Console.WriteLine(" 3) View Your Recruitment News");
            Console.WriteLine(" 4) Search CVs");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                if (recruiter.PhoneNum != null)
                {
                    ViewProfileInformation(RecruiterID);
                }
                else
                {
                    CreateNewProfileInformation(RecruiterID);
                }  
                    break;
                case "2":
                    AddRecruitmentNews(RecruiterID);
                    break;
                case "3":
                    DisplayNewsForRecruter(recruiterBL.GetRecruitNewByID(RecruiterID), RecruiterID);
                    // ViewYourMenuRecruitmentNews(RecruiterID);
                    break;
                case "4":
                    SearchCVs(RecruiterID);
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
            else
            {
                Console.WriteLine("================================"); 
                Console.WriteLine(" Couldn't retrieve the user info. Unexpected problems might have occurred to the connection to the database.");
                break;
            }
        }
    }
    public void CreateNewProfileInformation(int? RecruiterID) 
    {
        string? CompanyName;
        string? PhoneNum;
        string? Position = "staff";
        string? CompanyDescription;
        string? BussinessSize;
        string? BussinessField;
        string? CompanyAddress;


        Console.WriteLine("================================\n");
        Console.WriteLine(" View Your Personal Information");
        Console.WriteLine("\n================================");

        while (true) // PhoneNum
        {
            Console.Write(" Phone Number       : ");
            PhoneNum = GetUserInput();
            if(PhoneNum.Length > 100)
            {
                Console.WriteLine("\n Phone Number is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // CompanyName
        {
            Console.Write(" Company Name       : ");
            CompanyName = GetUserInput();
            if(CompanyName.Length > 100)
            {
                Console.WriteLine("\n Company Name is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // CompanyAddress
        {
            Console.Write(" Company Address    : ");
            CompanyAddress = GetUserInput();
            if(CompanyAddress.Length > 100)
            {
                Console.WriteLine("\n Company Address is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // Position
        {
            bool end = true;
            Console.WriteLine("================================");
            Console.WriteLine("  YOUR POSITION IN THE COMPANY");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Staff");
            Console.WriteLine(" 2) Leader");
            Console.WriteLine(" 3) Deputy of Department");
            Console.WriteLine(" 4) Head of Department");
            Console.WriteLine(" 5) Vice Director");
            Console.WriteLine(" 6) Director");
            Console.WriteLine(" 7) CEO");
            Console.WriteLine("================================");
            Console.Write(" Enter only number here (1-7): ");
            switch (GetUserInput())
            {
                case "1":
                    Position = "Staff";
                    break;
                case "2":
                    Position = "Leader";
                    break;
                case "3":
                    Position = "Deputy of Department";
                    break;
                case "4":
                    Position = "Head of Department";
                    break;
                case "5":
                    Position = "Vice Director";
                    break;
                case "6":
                    Position = "Director";
                    break;
                case "7":
                    Position = "CEO";
                    break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    end = false;
                    break;
            }
            if (end == true)
            {
                break;
            }
        }
        while (true) // CompanyDescription
        {
            Console.Write(" Company Description: ");
            CompanyDescription = GetUserInput();
            if(CompanyDescription.Length > 100)
            {
                Console.WriteLine("\n Company Description is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // BussinessSize
        {
            Console.Write(" Bussiness Size     : ");
            BussinessSize = GetUserInput();
            if(BussinessSize.Length > 100)
            {
                Console.WriteLine("\n Bussiness Size is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // BussinessField
        {
            Console.Write(" Bussiness Field    : ");
            BussinessField = GetUserInput();
            if(BussinessField.Length > 100)
            {
                Console.WriteLine("\n Bussiness Field is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        
        Recruiter newProfile = new Recruiter(PhoneNum, Position, CompanyName, CompanyAddress, CompanyDescription, BussinessSize, BussinessField);
        recruiterBL.CreateNewProfile(newProfile, RecruiterID);
        
    }
    
    public void DisplayNewsForRecruter(List<RecruitNews> recruitNews, int? RecruiterID)
    {  
        if (recruitNews != null)
        {
            while (true)
            {
                var table = new ConsoleTable("Pos", "Name");

                int count = 0;
                Console.WriteLine("================================\n");
                Console.WriteLine("       YOUR RECRUITMENT NEWS");
                Console.WriteLine("\n================================");
                foreach (RecruitNews news in recruitNews)
                {
                    table.AddRow(++count, news.NewsName);
                }
                table.Write(Format.Alternative);
                Console.WriteLine("================================");
                Console.Write(" Enter the position of news you like to view or 0 to return: ");
                string choice = GetUserInput();

                if(choice == "0")
                {
                    break;
                }

                bool success = int.TryParse(choice, out int position);
                if(success)
                {
                    ViewYourRecruitmentNews(recruitNews[position - 1], RecruiterID);
                }
                else
                {
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                }
            }
        }
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" No results!");
        }
    }

    public void AddRecruitmentNews(int? RecruiterID){ 

        string? NewsName;
        string? DeadLine;
        string? SalaryRange;
        string? FormOfEmploy;
        string? Gender;
        string? HiringAmount;
        string? HiringPosition = "Staff";
        string? RequiredExp;
        string? CityAddress;
        string? Profession;
        bool IsOpen = true;
        bool end = false;
        
        Console.WriteLine("================================\n");
        Console.WriteLine("      CREATE RECRUITMENT NEW");
        Console.WriteLine("\n================================");

        while (true) // NewsName
        {
            Console.Write(" New Name         : ");
            NewsName = GetUserInput();
            if(NewsName.Length > 100)
            {
                Console.WriteLine("\n Name is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // DeadLine
        {
            Console.Write(" Deadline         : ");
            DeadLine = GetUserInput();
            if(DeadLine.Length > 100)
            {
                Console.WriteLine("\n Deadline is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // SalaryRange
        {
            Console.Write(" Salary Range     : ");
            SalaryRange = GetUserInput();
            if(SalaryRange.Length > 100)
            {
                Console.WriteLine("\n Salary Range is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // FormOfEmploy
        {
            Console.Write(" Form Of Employ   : ");
            FormOfEmploy = GetUserInput();
            if(FormOfEmploy.Length > 100)
            {
                Console.WriteLine("\n Form Of Employ is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // Gender
        {
            Console.Write(" Gender           : ");
            Gender = GetUserInput();
            if(Gender.Length > 100)
            {
                Console.WriteLine("\n Gender is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // HiringAmount
        {
            Console.Write(" Hiring Amount    : ");
            HiringAmount = GetUserInput();
            if(HiringAmount.Length > 100)
            {
                Console.WriteLine("\n Hiring Amount is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // HiringPosition
        {
            Console.WriteLine("================================");
            Console.WriteLine("          Hiring Position");
            Console.WriteLine("================================");
            Console.WriteLine(" 1) Staff");
            Console.WriteLine(" 2) Leader");
            Console.WriteLine(" 3) Deputy of Department");
            Console.WriteLine(" 4) Head of Department");
            Console.WriteLine(" 5) Vice Director");
            Console.WriteLine(" 6) Director");
            Console.WriteLine(" 7) CEO");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput()) 
            {
                case "1":
                    HiringPosition = "Staff";
                    end = true;
                    break;
                case "2":
                    HiringPosition = "Leader";
                    end = true;
                    break;
                case "3":
                    HiringPosition = "Deputy of Department";
                    end = true;
                    break;
                case "4":
                    HiringPosition = "Head of Department";
                    end = true;
                    break;
                case "5":
                    HiringPosition = "Vice Director";
                    end = true;
                    break;
                case "6":
                    HiringPosition = "Director";
                    end = true;
                    break;
                case "7":
                    HiringPosition = "CEO";
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
        while (true) // RequiredExp
        {
            Console.Write(" Required Exp     : ");

            RequiredExp = GetUserInput();
            if(RequiredExp.Length > 100)
            {
                Console.WriteLine("\n Required Exp is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // CityAddress
        {
            Console.Write(" City Address     : ");
            CityAddress = GetUserInput();
            if(CityAddress.Length > 100)
            {
                Console.WriteLine("\n City Address is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        while (true) // Profession
        {
            Console.Write(" Profession       : ");

            Profession = GetUserInput();
            if(Profession.Length > 100)
            {
                Console.WriteLine("\n Profession is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
    
        RecruitNews News = new RecruitNews(NewsName, DeadLine, FormOfEmploy, Gender, HiringAmount, 
                                            HiringPosition, RequiredExp, IsOpen, SalaryRange, CityAddress, Profession);
        recruiterBL.CreateRecruitment(News, RecruiterID);
    }

    public void ViewYourRecruitmentNews(RecruitNews news, int? RecruiterID){  
        // RecruitNews recruitnew = recruiterBL.GetRecruitNewByID(RecruiterID);
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        YOUR RECRUITMENT NEW");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1,News Name            : {0}", news.NewsName);
            Console.WriteLine(" 2,Deadline             : {0}", news.DeadLine);
            Console.WriteLine(" 3,FormOfEmploy         : {0}", news.FormOfEmploy);
            Console.WriteLine(" 4,Gender               : {0}", news.Gender);
            Console.WriteLine(" 5,Hiring Amount        : {0}", news.HiringAmount);
            Console.WriteLine(" 6,Hiring Position      : {0}", news.HiringPosition);
            Console.WriteLine(" 7,Required Experiences : {0}", news.RequiredExp);
            Console.WriteLine(" 8,Open Status          : {0}", news.IsOpen);
            Console.WriteLine(" 9,Salary Range         : {0}", news.SalaryRange);
            Console.WriteLine(" 10,City Address        : {0}", news.CityAddress);
            Console.WriteLine(" 11,Profession          : {0}", news.Profession);
            Console.WriteLine(" R, View Applied CV ");
            Console.WriteLine(" 0, Return ");
            Console.Write(" Enter the option number to change the details: ");
            
            switch (GetUserInput())
            {
                case "1": //NewsName
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" News Name       : ");
                        string NewsName = GetUserInput();
                        if(NewsName.Length > 100)
                        {
                            Console.WriteLine("\n News Name is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.NewsName = NewsName;
                            break;
                        }
                    }
                    break;
                case "2": //DeadLine
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Deadline       : ");
                        string DeadLine = GetUserInput();
                        if(DeadLine.Length > 100)
                        {
                            Console.WriteLine("\n News Name is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.DeadLine = DeadLine;
                            break;
                        }
                    }
                    break;
                case "3": //FormOfEmploy
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Form Of Employ       : ");
                        string FormOfEmploy = GetUserInput();
                        if(FormOfEmploy.Length > 100)
                        {
                            Console.WriteLine("\n Form Of Employ is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.FormOfEmploy = FormOfEmploy;
                            break;
                        }
                    }
                    break;
                case "4": //Gender
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Gender       : ");
                        string Gender = GetUserInput();
                        if(Gender.Length > 100)
                        {
                            Console.WriteLine("\n Gender is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.Gender = Gender;
                            break;
                        }
                    }
                    break;
                case "5": //HiringAmount
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Hiring Amount       : ");
                        string HiringAmount = GetUserInput();
                        if(HiringAmount.Length > 100)
                        {
                            Console.WriteLine("\n Hiring Amount is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.HiringAmount = HiringAmount;
                            break;
                        }
                    }
                    break;
                case "6": //HiringPosition
                    while (true) 
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine("          Hiring Position");
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) Staff");
                        Console.WriteLine(" 2) Leader");
                        Console.WriteLine(" 3) Deputy of Department");
                        Console.WriteLine(" 4) Head of Department");
                        Console.WriteLine(" 5) Vice Director");
                        Console.WriteLine(" 6) Director");
                        Console.WriteLine(" 7) CEO");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (GetUserInput()) 
                        {
                            case "1":
                                news.HiringPosition = "Staff";
                                end = true;
                                break;
                            case "2":
                                news.HiringPosition = "Leader";
                                end = true;
                                break;
                            case "3":
                                news.HiringPosition = "Deputy of Department";
                                end = true;
                                break;
                            case "4":
                                news.HiringPosition = "Head of Department";
                                end = true;
                                break;
                            case "5":
                                news.HiringPosition = "Vice Director";
                                end = true;
                                break;
                            case "6":
                                news.HiringPosition = "Director";
                                end = true;
                                break;
                            case "7":
                                news.HiringPosition = "CEO";
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
                case "7": //RequiredExp
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Required Experiences       : ");
                        string RequiredExp = GetUserInput();
                        if(RequiredExp.Length > 100)
                        {
                            Console.WriteLine("\n Required Experiences is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.RequiredExp = RequiredExp;
                            break;
                        }
                    }
                    break;
                case "8": //IsOpen
                    Console.WriteLine("================================");                
                    while (true)
                    {
                        bool endStatus = true;
                        Console.WriteLine("             STATUS");
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1, IsOpen  ");
                        Console.WriteLine(" 2, IsClose  ");
                        Console.WriteLine("================================");
                        Console.Write(" Enter only number here (1-2): ");
                            switch (GetUserInput())
                            {
                                case "1":
                                    news.IsOpen = true;
                                    break;
                                case "2":
                                    news.IsOpen = false;
                                    break;
                                default:
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                    endStatus = false;
                                    break;
                            }
                            if (endStatus == true)
                                {
                                    break;
                                }
                    }       
                    break;
                case "9": //SalaryRange
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Salary Range       : ");
                        string SalaryRange = GetUserInput();
                        if(SalaryRange.Length > 100)
                        {
                            Console.WriteLine("\n Salary Range is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.SalaryRange = SalaryRange;
                            break;
                        }
                    }
                    break;
                case "10": //CityAddress
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" City Address      : ");
                        string CityAddress = GetUserInput();
                        if(CityAddress.Length > 100)
                        {
                            Console.WriteLine("\n City Address is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.CityAddress = CityAddress;
                            break;
                        }
                    }
                    break;
                case "11": //Profession
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Profession       : ");
                        string Profession = GetUserInput();
                        if(Profession.Length > 100)
                        {
                            Console.WriteLine("\n Profession is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            news.Profession = Profession;
                            break;
                        }
                    }
                    break;
                case "R":
                    DisplaySearchedCVs(ViewAppliedCV(news.NewsID), RecruiterID);
                    break;
                case "0":
                    while(true)
                    {    
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) Confirm changes");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (GetUserInput())
                        {
                            case "1":
                                recruiterBL.UpdateNewsInfo(news);
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

    public List<CV> ViewAppliedCV (int NewsID){
        return candidateBL.GetCVAppliedInNews(NewsID);
    }

    public void SearchCVs(int? RecruiterID){
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("          SEARCH CV");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) CareerTitle");
            Console.WriteLine(" 2) Address");
            Console.WriteLine(" 3) Job Position");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                    DisplaySearchedCVs(SearchCVViaCareerTitle(RecruiterID), RecruiterID);
                    break;
                case "2":
                    DisplaySearchedCVs(SearchCVViaAddress(RecruiterID), RecruiterID);
                    break;
                case "3":
                    DisplaySearchedCVs(SearchCVViaJobPosition(), RecruiterID);
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
    }   

    public List<CV> SearchCVViaJobPosition() 
    {
        while (true)
        {
            // bool endsearch = false;
            Console.WriteLine("================================\n");
            Console.WriteLine("       SEARCH JOB POSITION");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Staff");
            Console.WriteLine(" 2) Leader");
            Console.WriteLine(" 3) Deputy of Department");
            Console.WriteLine(" 4) Head of Department");
            Console.WriteLine(" 5) Vice Director");
            Console.WriteLine(" 6) Director");
            Console.WriteLine(" 7) CEO");
            // Console.WriteLine(" 0) Back to search menu");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (GetUserInput())
            {
                case "1":
                    return candidateBL.GetCVByJobPosition("Staff");
                case "2":
                    return candidateBL.GetCVByJobPosition("Leader");
                case "3":
                    return candidateBL.GetCVByJobPosition("Deputy of Department");
                case "4":
                    return candidateBL.GetCVByJobPosition("Head of Department");
                case "5":
                    return candidateBL.GetCVByJobPosition("Vice Director");
                case "6":
                    return candidateBL.GetCVByJobPosition("Director");
                case "7":
                    return candidateBL.GetCVByJobPosition("CEO");
                // case "0":
                //     endsearch = true;
                //     break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }
            // if (endsearch == true)
            // {
            //     break;
            // }
        }
    }

    public List<CV> SearchCVViaCareerTitle(int? RecruiterID){
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("       SEARCH CAREER TITLE");
            Console.WriteLine("\n================================");
            Console.Write("Enter the key word or press 0 to back to search menu: ");
            string? keyword = GetUserInput();
            if (keyword == "0"){
                    SearchCVs(RecruiterID);
            }
            else {
                return candidateBL.GetCVByCareerTitle(keyword);
            }
            
        }
    }
    
    public List<CV> SearchCVViaAddress(int? RecruiterID){ 
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("         SEARCH ADDRESS");
            Console.WriteLine("\n================================");
            Console.Write("Enter the key word: ");
            string? keyword = GetUserInput();
            if (keyword == "0"){
                    SearchCVs(RecruiterID);
            }
            else {
                return candidateBL.GetCVByAddress(GetUserInput());
            }
        }
    }

    public void DisplaySearchedCVs(List<CV> cv, int? RecruiterID)
    {  
        if (cv != null)
        {
            while (true)
            {
                var table = new ConsoleTable("Pos", "Name");

                int count = 0;
                Console.WriteLine("================================\n");
                Console.WriteLine("                CV");
                Console.WriteLine("\n================================");
                foreach (CV cvs in cv)
                {
                    table.AddRow(++count, cvs.FullName);
                }
                table.Write(Format.Alternative);
                Console.WriteLine("================================");
                Console.Write(" Enter the position of news you like to view or 0 to return: ");
                string choice = GetUserInput();

                if(choice == "0")
                {
                    break;
                }

                bool success = int.TryParse(choice, out int position);
                if(success)
                {
                    SearchedCVInformation(cv[position - 1], RecruiterID);
                }
                else
                {
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                }
            }
        }
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" No results!");
        }
    }

    public void SearchedCVInformation(CV cv, int? RecruiterID)
    {
        bool end = false;
        if (cv != null)
        {
            while (true)
            {
            Console.WriteLine("================================\n");
            Console.WriteLine("            YOUR CV");
            Console.WriteLine("\n================================");
            Console.WriteLine(" Full Name       : {0}", cv.FullName);
            Console.WriteLine(" Career Title    : {0}", cv.CareerTitle);
            Console.WriteLine(" Career Objective: {0}", cv.CareerObjective);
            Console.WriteLine(" Date of Birth   : {0}", cv.BirthDate);
            Console.WriteLine(" Phone Number    : {0}", cv.PhoneNum);
            Console.WriteLine(" Email           : {0}", cv.Email);
            Console.WriteLine(" Social Media    : {0}", cv.SocialMedia);
            Console.WriteLine(" Address         : {0}", cv.PersonalAddress);
            Console.WriteLine(" \n Skills:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Skill")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Work Experiences:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Work Experience")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Educations:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Education")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Activities:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Activity")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine(" Certifications:\n");
            if(cv.CVDetails != null)
            {
                var table = new ConsoleTable("Job Position", "From", "To", "Association", "Description");
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Certificate")
                    {
                        table.AddRow(detail.JobPosition, detail.FromDate, detail.ToDate, detail.Association, detail.Description);  
                    }
                }
                table.Write(Format.Alternative);
            }
            Console.WriteLine("================================");
                Console.WriteLine(" 0) Exit");
                Console.WriteLine("================================");
                Console.Write(" Press 0 to exit: ");
                switch (GetUserInput())
                {
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
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Couldn't retrieve all details of the recruitment news.");
        }
    }

    public void ViewProfileInformation(int? RecruiterID){ 
        Recruiter recruiter = recruiterBL.GetRecruiterByID(RecruiterID);
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        PERSONAL INFORMATION   ");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1,CompanyName        : {0}", recruiter.CompanyName);
            Console.WriteLine(" 2,PhoneNum           : {0}", recruiter.PhoneNum);
            Console.WriteLine(" 3,Position           : {0}", recruiter.Position);
            Console.WriteLine(" 4,CompanyDescription : {0}", recruiter.CompanyDescription);
            Console.WriteLine(" 5,Bussiness Size     : {0}", recruiter.BusinessSize);
            Console.WriteLine(" 6,Bussiness Field    : {0}", recruiter.BusinessField);
            Console.WriteLine(" 7,Company Address    : {0}", recruiter.CompanyAddress);
            Console.Write(" Enter the option number to change the details or 0 to return: ");
            
            switch (GetUserInput())
            {
                case "1": // CompanyName
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Company Name       : ");
                        string CompanyName = GetUserInput();
                        if(CompanyName.Length > 100)
                        {
                            Console.WriteLine("\n Company Name is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.CompanyName = CompanyName;
                            break;
                        }
                    }
                    break;
                case "2": //Postion
                    while (true) 
                        {
                            bool endUpdate = true;
                            Console.WriteLine("================================\n");
                            Console.WriteLine("          UPDATE POSITION");
                            Console.WriteLine("\n================================");
                            Console.WriteLine(" 1) Staff");
                            Console.WriteLine(" 2) Leader");
                            Console.WriteLine(" 3) Deputy of Department");
                            Console.WriteLine(" 4) Head of Department");
                            Console.WriteLine(" 5) Vice Director");
                            Console.WriteLine(" 6) Director");
                            Console.WriteLine(" 7) CEO");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            switch (GetUserInput())
                            {
                                case "1":
                                    recruiter.Position = "Staff";;
                                    break;
                                case "2":
                                    recruiter.Position = "Leader";
                                    break;
                                case "3":
                                    recruiter.Position = "Deputy of Department";
                                    break;
                                case "4":
                                    recruiter.Position = "Head of Department";
                                    break;
                                case "5":
                                    recruiter.Position = "Vice Director";
                                    break;
                                case "6":
                                    recruiter.Position = "Director";
                                    break;
                                case "7":
                                    recruiter.Position = "CEO";
                                    break;
                                default:
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                    endUpdate = false;
                                    break;
                            }
                            if (endUpdate == true)
                            {
                                break;
                            }
                        }
                    break;
                case "3": // PhoneNum
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Phone Number     : ");
                        string PhoneNum = GetUserInput();
                        if(PhoneNum.Length > 100)
                        {
                            Console.WriteLine("\n Phone Number is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.PhoneNum = PhoneNum;
                            break;
                        }
                    }
                    break;
                case "4": // CompanyDescription
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Company Description       : ");
                        string CompanyDescription = GetUserInput();
                        if(CompanyDescription.Length > 100)
                        {
                            Console.WriteLine("\n Company Description is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.CompanyDescription = CompanyDescription;
                            break;
                        }
                    }
                    break;
                case "5": // BussinessSize
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Bussiness Size       : ");
                        string BussinessSize = GetUserInput();
                        if(BussinessSize.Length > 100)
                        {
                            Console.WriteLine("\n Bussiness Size is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.BusinessSize = BussinessSize;
                            break;
                        }
                    }
                    break;
                case "6": // BussinessField
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Bussiness Field       : ");
                        string BussinessField = GetUserInput();
                        if(BussinessField.Length > 100)
                        {
                            Console.WriteLine("\n Bussiness Field is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.BusinessField = BussinessField;
                            break;
                        }
                    }
                    break;
                case "7": // CompanyAddress
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Company Address       : ");
                        string CompanyAddress = GetUserInput();
                        if(CompanyAddress.Length > 100)
                        {
                            Console.WriteLine("\n Company Address is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.CompanyAddress = CompanyAddress;
                            break;
                        }
                    }
                    break;
                case "0":
                    while(true)
                    {    
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) Confirm changes");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (GetUserInput())
                        {
                            case "1":
                                recruiterBL.UpdatePersonalRecruitInfo(recruiter);
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
}