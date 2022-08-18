namespace Pl_Console;
using Persistence;
using Spectre.Console;
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
                Console.WriteLine(" User: {0}", candidate.Name);
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
            Console.WriteLine(" User: {0}", recruiter.Name);
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
        if(cv.CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Skill[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Skill")
                {
                    i += 1;
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
            
        }
        
        if(cv.CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Work Experience[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Work Experience")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
        }
        
        if(cv.CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Education[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Education")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
        }
        
        if(cv.CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Education[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Activity")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
        }
        
        if(cv.CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Certificate[/]");
            table.AddColumn("[red]Date[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Certificate")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
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
        if(CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Skill[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in CVDetails)
            {
                if(detail.Title == "Skill")
                {
                    i += 1;
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
            
        }
        
        if(CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Work Experience[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in CVDetails)
            {
                if(detail.Title == "Work Experience")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
        }
        
        if(CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Education[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in CVDetails)
            {
                if(detail.Title == "Education")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
        }
        
        if(CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Education[/]");
            table.AddColumn("[red]From[/]");
            table.AddColumn("[red]To[/]");
            table.AddColumn("[red]Association[/]");
            table.AddColumn("[red]Description[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            table.Columns[3].Width(10);
            table.Columns[4].Width(20);
            table.Columns[5].Width(30);
            foreach (CVDetails detail in CVDetails)
            {
                if(detail.Title == "Activity")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
        }
        
        if(CVDetails != null)
        {
            int i = 0;
            var table = new Table();
            table.AddColumn("[blue]Pos[/]");
            table.AddColumn("[red]Certificate[/]");
            table.AddColumn("[red]Date[/]");
            table.Columns[0].Width(4).Centered();
            table.Columns[1].Width(20);
            table.Columns[2].Width(10);
            foreach (CVDetails detail in CVDetails)
            {
                if(detail.Title == "Certificate")
                {
                    table.AddRow("[green]" + i.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "");  
                }
            }
            table.LeftAligned();
            AnsiConsole.Write(table);
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
        var table = new Table();
        table.AddColumn("[blue]Pos[/]");
        table.AddColumn("[red]Name[/]");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(40);

        int count = 0;
        Console.WriteLine("================================\n");
        Console.WriteLine("       RECRUITMENT NEWS");
        Console.WriteLine("\n================================");
        foreach (RecruitNews news in recruitNews)
        {
            count = ++count;
            table.AddRow("[green]" + count.ToString() + "[/]", news.NewsName ?? "");
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
        Console.WriteLine("================================");
        Console.Write(" Enter the position of news you like to view or 0 to return: ");
    }

    private static void ViewAddedNewsMenu(List<RecruitNews> recruitNews)
    {
        var table = new Table();
        table.AddColumn("[blue]Pos[/]");
        table.AddColumn("[red]Name[/]");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(40);

        int count = 0;
        Console.WriteLine("================================\n");
        Console.WriteLine("       YOUR RECRUITMENT NEWS");
        Console.WriteLine("\n================================");
        foreach (RecruitNews news in recruitNews)
        {
            count = ++count;
            table.AddRow("[green]" + count.ToString() + "[/]", news.NewsName ?? "");
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
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
        Console.WriteLine(" 5,Business Size      : {0}", recruiter.BusinessSize);
        Console.WriteLine(" 6,Business Field     : {0}", recruiter.BusinessField);
        Console.WriteLine(" 7,Company Address    : {0}", recruiter.CompanyAddress);
        Console.Write(" Enter the option number to change the details or 0 to return: ");
    }

    private static void SearchedCVsMenu(List<CV> cv)
    {
        var table = new Table();
        table.AddColumn("[blue]Pos[/]");
        table.AddColumn("[red]Name[/]");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(40);

        int count = 0;
        Console.WriteLine("================================\n");
        Console.WriteLine("                CV");
        Console.WriteLine("\n================================");
        foreach (CV cvs in cv)
        {
            count = ++count;
            table.AddRow("[green]" + count.ToString() + "[/]", cvs.FullName ?? "");
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
        Console.WriteLine("================================");
        Console.Write(" Enter the position of news you like to view or 0 to return: ");
    }
}
