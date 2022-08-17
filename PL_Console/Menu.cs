namespace Pl_Console;
using Persistence;
using ConsoleTables;
public class Menu
{
    public static Dictionary<int, Delegate> MainMenus = new Dictionary<int, Delegate>()
    {
        // add functions to print main menus here
        {1, new Action(StartMenu)},
        {2, new Action<int?>(CandidateMenu)},
        {3, new Action<int?>(RecruiterMenu)}
    };
    public static Dictionary<int, Delegate> SubMenus = new Dictionary<int, Delegate>()
    {
        // add functions to print sub menus here
        {1, new Action<CV>(ViewCVMenu)},
        {2, new Action<List<CVDetails>>(UpdateCVDetailsMenu)},
        {3, new Action<List<RecruitNews>>(SearchedRecruitNewsMenu)},
        {4, new Action<List<RecruitNews>>(ViewAddedNewsMenu)},
        {5, new Action<Recruiter>(ViewProfileMenu)},
        {6, new Action<List<CV>>(SearchedCVsMenu)}
    };
    private static UserController userController = new UserController();
    private static CandidateController candidateController = new CandidateController();
    private static RecruiterController recruiterController = new RecruiterController();

    public static void PrintMainMenu(int i, int? ID)
    {
        if(ID == null)
        {
            MainMenus[i].DynamicInvoke();
        }
        else
        {
            MainMenus[i].DynamicInvoke(ID);
        }
        
    }

    public static void PrintSubMenu(int i, Object? obj)
    {
        if(obj == null)
        {
            SubMenus[i].DynamicInvoke();
        }
        else
        {
            SubMenus[i].DynamicInvoke(obj);
        }
    }

    private static void StartMenu()
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    userController.LogIn();
                    break;
                case "2":
                    userController.Register();
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

    private static void CandidateMenu(int? CandidateID)
    {
        bool end = false;
        while (true)
        {
            Candidate candidate = candidateController.GetCandidate(CandidateID);
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
                switch (UserController.GetUserInput())
                {
                    case "1":
                        if (candidate.CandidateCV != null)
                        {
                            candidateController.ViewCV(candidate.CandidateCV);
                        }
                        else
                        {
                            candidateController.CreateCV(CandidateID);
                        }
                        break;
                    case "2":
                        candidateController.SearchRecruitNews(CandidateID);
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
            else
            {
                Console.WriteLine("================================"); 
                Console.WriteLine(" Couldn't retrieve the user info. Unexpected problems might have occurred to the connection to the database.");
                break;
            }
            
        }
    }

    // NCT
    private static void RecruiterMenu(int? RecruiterID)   
    {
        bool end = false;
        while (true)
        {
            Recruiter recruiter = recruiterController.GetRecruiter(RecruiterID);
            // RecruitNews recruitnew = recruiterBL.GetRecruitNewByID(RecruiterID);
            if(recruiter != null)
            {
            Console.WriteLine("================================\n");
            Console.WriteLine(" Username: {0}", recruiter.Username);
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
                switch (UserController.GetUserInput())
                {
                    case "1":
                    if (recruiter.PhoneNum != null)
                    {
                        recruiterController.ViewProfileInformation(recruiter);
                    }
                    else
                    {
                        recruiterController.CreateNewProfileInformation(RecruiterID);
                    }  
                        break;
                    case "2":
                        recruiterController.AddRecruitmentNews(RecruiterID);
                        break;
                    case "3":
                        recruiterController.DisplayNewsForRecruter(RecruiterID);
                        // ViewYourMenuRecruitmentNews(RecruiterID);
                        break;
                    case "4":
                        recruiterController.SearchCVs(RecruiterID);
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
            else
            {
                Console.WriteLine("================================"); 
                Console.WriteLine(" Couldn't retrieve the user info. Unexpected problems might have occurred to the connection to the database.");
                break;
            }
        }
    }

    private static void ViewCVMenu(CV cv)
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
    }

    private static void UpdateCVDetailsMenu(List<CVDetails>? CVDetails)
    {
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
    }

    private static void SearchedRecruitNewsMenu(List<RecruitNews> recruitNews)
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
    }

    private static void ViewAddedNewsMenu(List<RecruitNews> recruitNews)
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
    }

    private static void ViewProfileMenu(Recruiter recruiter)
    {
        Console.WriteLine("================================\n");
        Console.WriteLine("        PERSONAL INFORMATION   ");
        Console.WriteLine("\n================================");
        Console.WriteLine(" 1,CompanyName        : {0}", recruiter.CompanyName);
        Console.WriteLine(" 2,PhoneNum           : {0}", recruiter.PhoneNum);
        Console.WriteLine(" 3,Position           : {0}", recruiter.Position);
        Console.WriteLine(" 4,CompanyDescription : {0}", recruiter.CompanyDescription);
        Console.WriteLine(" 5,Business Size     : {0}", recruiter.BusinessSize);
        Console.WriteLine(" 6,Business Field    : {0}", recruiter.BusinessField);
        Console.WriteLine(" 7,Company Address    : {0}", recruiter.CompanyAddress);
        Console.Write(" Enter the option number to change the details or 0 to return: ");
    }

    private static void SearchedCVsMenu(List<CV> cv)
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
    }
}
