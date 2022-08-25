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
        {2, new Action<List<CVDetails>>(ViewCVDetailsMenu)},
        {3, new Action<List<RecruitNews>, int>(SearchedRecruitNewsMenu)},
        {4, new Action<List<RecruitNews>, int>(ViewAddedNewsMenu)},
        {5, new Action<Recruiter>(ViewProfileMenu)},
        {6, new Action<List<CV>, int>(SearchedCVsMenu)},
        {7, new Action<CV>(ViewSelectedCVMenu)},
        {8, new Action<RecruitNews>(AddedRecruitNewsMenu)}
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

    public static void PrintSubMenu(int i, Object? obj, Object? obj2)
    {
        if(obj == null && obj2 == null)
        {
            SubMenus[i].DynamicInvoke();
        }
        if(obj2 == null)
        {
            SubMenus[i].DynamicInvoke(obj);
        }
        else
        {
            SubMenus[i].DynamicInvoke(obj, obj2);
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
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }

            if (end == true)
            {
                Console.Clear();
                break;
            }
        }
    }

    private static void CandidateMenu(int? CandidateID)
    {
        Console.Clear();
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
                            Console.Clear();
                            candidateController.ViewCV(candidate.CandidateCV);
                        }
                        else
                        {
                            Console.Clear();
                            candidateController.CreateCV(CandidateID);
                        }
                        break;
                    case "2":
                        Console.Clear();
                        if(candidate.CandidateCV != null)
                        {
                            candidateController.SearchRecruitNews(CandidateID, true);
                        }
                        else
                        {
                            candidateController.SearchRecruitNews(CandidateID, false);
                        }
                        break;
                    case "0":
                        end = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid choice! Please re-enter your option.");
                        break;
                }

                if (end == true)
                {
                    Console.Clear();
                    break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" Couldn't retrieve the user info. Unexpected problems might have occurred.");
                break;
            }
        }
    }

    private static void RecruiterMenu(int? RecruiterID)   
    {
        Console.Clear();
        bool end = false;
        while (true)
        {
            Recruiter recruiter = recruiterController.GetRecruiter(RecruiterID);
            if(recruiter != null)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine(" User: {0}", recruiter.Name);
                Console.WriteLine("\n================================");
                if (!String.IsNullOrEmpty(recruiter.PhoneNum))
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
                        if (!String.IsNullOrEmpty(recruiter.PhoneNum))
                        {
                            recruiterController.ViewProfileInformation(recruiter);
                        }
                        else
                        {
                            recruiterController.CreateNewProfileInformation(RecruiterID);
                        }  
                        break;
                    case "2":
                        if(!String.IsNullOrEmpty(recruiter.PhoneNum))
                        {
                            recruiterController.AddRecruitmentNews(RecruiterID);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Please insert your personal information first.");
                        }
                        break;
                    case "3":
                        recruiterController.DisplayNewsForRecruter(RecruiterID);
                        break;
                    case "4":
                        recruiterController.SearchCVs(RecruiterID);
                        break;
                    case "0":
                        end = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid choice! Please re-enter your option.");
                        break;
                }

                if (end == true)
                {
                    Console.Clear();
                    break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" Couldn't retrieve the user info. Unexpected problems might have occurred.");
                break;
            }
        }
    }

    private static void ViewCVMenu(CV cv)
    {
        // Create table to hold all details of the CV
        var cvTable = new Table();
        cvTable.Border(TableBorder.Ascii);
        cvTable.Title("YOUR CV");
        cvTable.HideHeaders();
        cvTable.AddColumn("Title");
        cvTable.AddColumn(new TableColumn("Content"));
        cvTable.Columns[0].Centered();
        cvTable.Columns[0].Width(20);
        cvTable.Columns[1].LeftAligned();

        // create table for Personal Information
        var personalInfoTable = new Table();
        personalInfoTable.AddColumn("Title");
        personalInfoTable.AddColumn("Content");
        personalInfoTable.HideHeaders();
        personalInfoTable.Columns[0].Width(20);
        personalInfoTable.Columns[1].Width(79);
        personalInfoTable.LeftAligned();
        personalInfoTable.AddRow("Full Name", cv.FullName);
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Career Title", cv.CareerTitle ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Date of Birth", cv.BirthDate?.ToString("dd/MM/yyyy") ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Phone Number", cv.PhoneNum);
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Email Address", cv.Email);
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Social Media", cv.SocialMedia ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Address", cv.PersonalAddress);

        // create table for Career's Objective
        var objectiveTable = new Table();
        objectiveTable.AddColumn("Career's Objective");
        objectiveTable.HideHeaders();
        objectiveTable.Columns[0].Width(102);
        objectiveTable.AddRow(cv.CareerObjective ?? "");
        objectiveTable.LeftAligned();

        // add table Personal Information and table Career's Objective to table CV
        cvTable.AddRow(new Markup("Personal Information"), personalInfoTable);
        cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
        cvTable.AddRow(new Markup("Career's Objective"), objectiveTable);

        if(cv.CVDetails != null)
        {
            int skillNo = 0;
            int certNo = 0;
            int expNo = 0;
            int eduNo = 0;
            int actNo = 0;

            // create table for Skills
            var skillTable = new Table();
            skillTable.AddColumn("Skill Group Name");
            skillTable.AddColumn("Skill Description");
            skillTable.Columns[0].Width(20);
            skillTable.Columns[1].Width(79);
            skillTable.LeftAligned();


            // create table for Certifications
            var certTable = new Table();
            certTable.AddColumn("Time");
            certTable.AddColumn("Certification Name");
            certTable.Columns[0].Width(20);
            certTable.Columns[1].Width(79);
            certTable.LeftAligned();

            // create table for Work Experience
            var expTable = new Table();
            expTable.AddColumn("Title / Position");
            expTable.AddColumn("From");
            expTable.AddColumn("To");
            expTable.AddColumn("Company Name");
            expTable.AddColumn("Description");
            expTable.Columns[0].Width(20);
            expTable.Columns[1].Width(10);
            expTable.Columns[2].Width(10);
            expTable.Columns[3].Width(20);
            expTable.Columns[4].Width(30);
            expTable.LeftAligned();

            // create table for Education
            var eduTable = new Table();
            eduTable.AddColumn("Degree / Study Field");
            eduTable.AddColumn("From");
            eduTable.AddColumn("To");
            eduTable.AddColumn("School");
            eduTable.AddColumn("Description");
            eduTable.Columns[0].Width(20);
            eduTable.Columns[1].Width(10);
            eduTable.Columns[2].Width(10);
            eduTable.Columns[3].Width(20);
            eduTable.Columns[4].Width(30);
            eduTable.LeftAligned();

            // create table for Activities
            var actTable = new Table();
            actTable.AddColumn("Title / Role");
            actTable.AddColumn("From");
            actTable.AddColumn("To");
            actTable.AddColumn("Organization");
            actTable.AddColumn("Description");
            actTable.Columns[0].Width(20);
            actTable.Columns[1].Width(10);
            actTable.Columns[2].Width(10);
            actTable.Columns[3].Width(20);
            actTable.Columns[4].Width(30);
            actTable.LeftAligned();

            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Skill")
                {
                    skillNo += 1;
                    if(skillNo > 1)
                    {
                        skillTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
                    }
                    
                    skillTable.AddRow(detail.JobPosition ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Certificate")
                {
                    certNo += 1;
                    if(certNo > 1)
                    {
                        certTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
                    }
                    certTable.AddRow(detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.JobPosition ?? "");  
                }
                else if(detail.Title == "Work Experience")
                {
                    expNo += 1;
                    if(expNo > 1)
                    {
                        expTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    expTable.AddRow( detail.JobPosition ?? "", detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Education")
                {
                    eduNo += 1;
                    if(eduNo > 1)
                    {
                        eduTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    eduTable.AddRow( detail.JobPosition ?? "", detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Activity")
                {
                    actNo += 1;
                    if(actNo > 1)
                    {
                        actTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    actTable.AddRow( detail.JobPosition ?? "", detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            
            if(skillNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Skills"), skillTable);
            }
            
            if(certNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Certifications"), certTable);
            }

            if(expNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Work Experience"), expTable);
            }

            if(eduNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Education"), eduTable);
            }
            
            if(actNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Activities"), actTable);
            }
        }
        AnsiConsole.Write(cvTable);
    }

    private static void ViewCVDetailsMenu(List<CVDetails>? CVDetails)
    {
        Console.Clear();

        // Create table to hold all details of the CV
        var cvTable = new Table();
        cvTable.Border(TableBorder.Ascii);
        cvTable.Title("YOUR CV");
        cvTable.HideHeaders();
        cvTable.AddColumn("Title");
        cvTable.AddColumn(new TableColumn("Content"));
        cvTable.Columns[0].Centered();
        cvTable.Columns[0].Width(20);
        cvTable.Columns[1].LeftAligned();

        if(CVDetails != null)
        {
            int skillNo = 0;
            int certNo = 0;
            int expNo = 0;
            int eduNo = 0;
            int actNo = 0;

            // create table for Skills
            var skillTable = new Table();
            skillTable.AddColumn("Pos");
            skillTable.AddColumn("Skill Group Name");
            skillTable.AddColumn("Skill Description");
            skillTable.Columns[0].Width(4).Centered();
            skillTable.Columns[1].Width(20);
            skillTable.Columns[2].Width(79);
            skillTable.LeftAligned();


            // create table for Certifications
            var certTable = new Table();
            certTable.AddColumn("Pos");
            certTable.AddColumn("Time");
            certTable.AddColumn("Certification Name");
            certTable.Columns[0].Width(4).Centered();
            certTable.Columns[1].Width(20);
            certTable.Columns[2].Width(79);
            certTable.LeftAligned();

            // create table for Work Experience
            var expTable = new Table();
            expTable.AddColumn("Pos");
            expTable.AddColumn("Title / Position");
            expTable.AddColumn("From");
            expTable.AddColumn("To");
            expTable.AddColumn("Company Name");
            expTable.AddColumn("Description");
            expTable.Columns[0].Width(4).Centered();
            expTable.Columns[1].Width(20);
            expTable.Columns[2].Width(10);
            expTable.Columns[3].Width(10);
            expTable.Columns[4].Width(20);
            expTable.Columns[5].Width(30);
            expTable.LeftAligned();

            // create table for Education
            var eduTable = new Table();
            eduTable.AddColumn("Pos");
            eduTable.AddColumn("Degree / Study Field");
            eduTable.AddColumn("From");
            eduTable.AddColumn("To");
            eduTable.AddColumn("School");
            eduTable.AddColumn("Description");
            eduTable.Columns[0].Width(4).Centered();
            eduTable.Columns[1].Width(20);
            eduTable.Columns[2].Width(10);
            eduTable.Columns[3].Width(10);
            eduTable.Columns[4].Width(20);
            eduTable.Columns[5].Width(30);
            eduTable.LeftAligned();

            // create table for Activities
            var actTable = new Table();
            actTable.AddColumn("Pos");
            actTable.AddColumn("Title / Role");
            actTable.AddColumn("From");
            actTable.AddColumn("To");
            actTable.AddColumn("Organization");
            actTable.AddColumn("Description");
            actTable.Columns[0].Width(4).Centered();
            actTable.Columns[1].Width(20);
            actTable.Columns[2].Width(10);
            actTable.Columns[3].Width(10);
            actTable.Columns[4].Width(20);
            actTable.Columns[5].Width(30);
            actTable.LeftAligned();

            foreach (CVDetails detail in CVDetails)
            {
                if(detail.Title == "Skill")
                {
                    skillNo += 1;
                    if(skillNo > 1)
                    {
                        skillTable.AddRow("----", "--------------------", "-------------------------------------------------------------------------------");
                    }
                    
                    skillTable.AddRow(skillNo.ToString(), detail.JobPosition ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Certificate")
                {
                    certNo += 1;
                    if(certNo > 1)
                    {
                        certTable.AddRow("----", "--------------------", "-------------------------------------------------------------------------------");
                    }
                    certTable.AddRow(certNo.ToString(), detail.FromDate?.ToString("dd/MM/yyyy") ?? "",  detail.JobPosition ?? "");  
                }
                else if(detail.Title == "Work Experience")
                {
                    expNo += 1;
                    if(expNo > 1)
                    {
                        expTable.AddRow("----", "--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    expTable.AddRow(expNo.ToString(), detail.JobPosition ?? "",  detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Education")
                {
                    eduNo += 1;
                    if(eduNo > 1)
                    {
                        eduTable.AddRow("----", "--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    eduTable.AddRow(eduNo.ToString(), detail.JobPosition ?? "",  detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Activity")
                {
                    actNo += 1;
                    if(actNo > 1)
                    {
                        actTable.AddRow("----", "--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    actTable.AddRow(actNo.ToString(), detail.JobPosition ?? "",  detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            
            if(skillNo > 0)
            {
                cvTable.AddRow(new Markup("Skills"), skillTable);
            }
            
            if(certNo > 0)
            {
                if(skillNo != 0)
                {
                    cvTable.AddRow("--------------------", "--------------------------------------------------------------------------------------------------------------");
                }
                cvTable.AddRow(new Markup("Certifications"), certTable);
            }

            if(expNo > 0)
            {
                if(certNo != 0 || skillNo != 0)
                {
                    cvTable.AddRow("--------------------", "--------------------------------------------------------------------------------------------------------------");
                }
                cvTable.AddRow(new Markup("Work Experience"), expTable);
            }

            if(eduNo > 0)
            {
                if(expNo != 0 || certNo != 0 || skillNo != 0)
                {
                    cvTable.AddRow("--------------------", "--------------------------------------------------------------------------------------------------------------");
                }
                cvTable.AddRow(new Markup("Education"), eduTable);
            }
            
            if(actNo > 0)
            {
                if(eduNo != 0 || expNo != 0 || certNo != 0 || skillNo != 0)
                {
                    cvTable.AddRow("--------------------", "--------------------------------------------------------------------------------------------------------------");
                }
                cvTable.AddRow(new Markup("Activities"), actTable);
            }

            AnsiConsole.Write(cvTable);
        }
        Console.Write(" Press any key to return: ");
        Console.ReadKey();
        Console.Clear();
    }

    private static void SearchedRecruitNewsMenu(List<RecruitNews> recruitNews, int page)
    {
        var table = new Table();
        int maxRow = page * 5;
        table.AddColumn("Pos");
        table.AddColumn("Name");
        table.AddColumn("Profession");
        table.AddColumn("City Address");
        table.AddColumn("Salary Range");
        table.Title("RECRUITMENT NEWS");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(25);
        table.Columns[2].Width(20);
        table.Columns[3].Width(20);
        table.Columns[4].Width(20);

        int count = 0;
        foreach (RecruitNews news in recruitNews)
        {
            count += 1;
            if (count > (maxRow - 5) && count <= maxRow)
            {
                if(count > (maxRow - 4))
                {
                    table.AddRow("----", "-------------------------", "--------------------", "--------------------", "--------------------");
                }
                table.AddRow(count.ToString(), news.NewsName, news.Profession, news.CityAddress, news.SalaryRange);
            }
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
    }

    private static void ViewAddedNewsMenu(List<RecruitNews> recruitNews, int page)
    {
        var table = new Table();
        int maxRow = page * 5;
        table.AddColumn("Pos");
        table.AddColumn("Name");
        table.AddColumn("Profession");
        table.AddColumn("City Address");
        table.AddColumn("Salary Range");
        table.Title("YOUR RECRUITMENT NEWS");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(25);
        table.Columns[2].Width(20);
        table.Columns[3].Width(20);
        table.Columns[4].Width(20);

        int count = 0;
        foreach (RecruitNews news in recruitNews)
        {
            count += 1;
            if (count > (maxRow - 5) && count <= maxRow)
            {
                if(count > (maxRow - 4))
                {
                    table.AddRow("----", "-------------------------", "--------------------", "--------------------", "--------------------");
                }
                table.AddRow(count.ToString(), news.NewsName, news.Profession, news.CityAddress, news.SalaryRange);
            }
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
    }

    public static void AddedRecruitNewsMenu(RecruitNews news)
    {
        var profileTable = new Table();
        profileTable.Title("NEWS DETAILS");
        profileTable.HideHeaders();
        profileTable.AddColumn("Title");
        profileTable.AddColumn(new TableColumn("Content"));
        profileTable.Columns[0].Width(20);
        profileTable.LeftAligned();
        profileTable.Columns[1].Width(40);
        profileTable.AddRow("News Name", news.NewsName);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Deadline", news.DeadLine.ToString("dd/MM/yyyy"));
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Form Of Employment", news.FormOfEmploy);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Gender", news.Gender);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Hiring Amount", news.HiringAmount);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Hiring Position", news.HiringPosition);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Required Experience", news.RequiredExp);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Salary Range", news.SalaryRange);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("City Address", news.CityAddress);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Profession", news.Profession);
        profileTable.AddRow("--------------------", "----------------------------------------");
        if(news.IsOpen == true)
        {
            profileTable.AddRow("Status", "Open");
        }
        else
        {
            profileTable.AddRow("Status", "Closed");
        }
        
        AnsiConsole.Write(profileTable);
    }

    private static void ViewProfileMenu(Recruiter recruiter)
    {
        var profileTable = new Table();
        profileTable.Title("PERSONAL INFORMATION");
        profileTable.HideHeaders();
        profileTable.AddColumn("Title");
        profileTable.AddColumn(new TableColumn("Content"));
        profileTable.Columns[0].Width(20);
        profileTable.LeftAligned();
        profileTable.Columns[1].Width(40);
        profileTable.AddRow("Name", recruiter.Name);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Position", recruiter.Position);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Phone Number", recruiter.PhoneNum);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Company", recruiter.CompanyName);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Company Address", recruiter.CompanyAddress);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Business Field", recruiter.BusinessField);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Business Size", recruiter.BusinessSize);
        profileTable.AddRow("--------------------", "----------------------------------------");
        profileTable.AddRow("Company Description", recruiter.CompanyDescription ?? "");

        AnsiConsole.Write(profileTable);
    }

    private static void SearchedCVsMenu(List<CV> cv, int page)
    {
        var table = new Table();
        int maxRow = page * 5;
        table.AddColumn("Pos");
        table.AddColumn("Name");
        table.AddColumn("Date of Birth");
        table.AddColumn("Career Title");
        table.AddColumn("Address");
        table.Title("CVs");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(25);
        table.Columns[2].Width(10);
        table.Columns[3].Width(25);
        table.Columns[4].Width(30);

        int count = 0;
        foreach (CV cvs in cv)
        {
            count += 1;
            if (count > (maxRow - 5) && count <= maxRow)
            {
                if(count > (maxRow - 4))
                {
                    table.AddRow("----", "-------------------------", "----------", "-------------------------", "------------------------------");
                }
                table.AddRow(count.ToString(), cvs.FullName ?? "", cvs.BirthDate?.ToString("dd/MM/yyyy") ?? "", cvs.CareerTitle ?? "", cvs.PersonalAddress);
            }
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
    }

    private static void ViewSelectedCVMenu(CV cv)
    {
        Console.Clear();
        // Create table to hold all details of the CV
        var cvTable = new Table();
        cvTable.Border(TableBorder.Ascii);
        cvTable.Title("VIEW CV");
        cvTable.HideHeaders();
        cvTable.AddColumn("Title");
        cvTable.AddColumn(new TableColumn("Content"));
        cvTable.Columns[0].Centered();
        cvTable.Columns[0].Width(20);
        cvTable.Columns[1].LeftAligned();

        // create table for Personal Information
        var personalInfoTable = new Table();
        personalInfoTable.AddColumn("Title");
        personalInfoTable.AddColumn("Content");
        personalInfoTable.HideHeaders();
        personalInfoTable.Columns[0].Width(20);
        personalInfoTable.Columns[1].Width(79);
        personalInfoTable.LeftAligned();
        personalInfoTable.AddRow("Full Name", cv.FullName);
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Career Title", cv.CareerTitle ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Date of Birth", cv.BirthDate?.ToString("dd/MM/yyyy") ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Phone Number", cv.PhoneNum);
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Email Address", cv.Email);
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Social Media", cv.SocialMedia ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Address", cv.PersonalAddress);

        // create table for Career's Objective
        var objectiveTable = new Table();
        objectiveTable.AddColumn("Career's Objective");
        objectiveTable.HideHeaders();
        objectiveTable.Columns[0].Width(102);
        objectiveTable.AddRow(cv.CareerObjective ?? "");
        objectiveTable.LeftAligned();

        // add table Personal Information and table Career's Objective to table CV
        cvTable.AddRow(new Markup("Personal Information"), personalInfoTable);
        cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
        cvTable.AddRow(new Markup("Career's Objective"), objectiveTable);

        if(cv.CVDetails != null)
        {
            int skillNo = 0;
            int certNo = 0;
            int expNo = 0;
            int eduNo = 0;
            int actNo = 0;

            // create table for Skills
            var skillTable = new Table();
            skillTable.AddColumn("Skill Group Name");
            skillTable.AddColumn("Skill Description");
            skillTable.Columns[0].Width(20);
            skillTable.Columns[1].Width(79);
            skillTable.LeftAligned();


            // create table for Certifications
            var certTable = new Table();
            certTable.AddColumn("Time");
            certTable.AddColumn("Certification Name");
            certTable.Columns[0].Width(20);
            certTable.Columns[1].Width(79);
            certTable.LeftAligned();

            // create table for Work Experience
            var expTable = new Table();
            expTable.AddColumn("Title / Position");
            expTable.AddColumn("From");
            expTable.AddColumn("To");
            expTable.AddColumn("Company Name");
            expTable.AddColumn("Description");
            expTable.Columns[0].Width(20);
            expTable.Columns[1].Width(10);
            expTable.Columns[2].Width(10);
            expTable.Columns[3].Width(20);
            expTable.Columns[4].Width(30);
            expTable.LeftAligned();

            // create table for Education
            var eduTable = new Table();
            eduTable.AddColumn("Degree / Study Field");
            eduTable.AddColumn("From");
            eduTable.AddColumn("To");
            eduTable.AddColumn("School");
            eduTable.AddColumn("Description");
            eduTable.Columns[0].Width(20);
            eduTable.Columns[1].Width(10);
            eduTable.Columns[2].Width(10);
            eduTable.Columns[3].Width(20);
            eduTable.Columns[4].Width(30);
            eduTable.LeftAligned();

            // create table for Activities
            var actTable = new Table();
            actTable.AddColumn("Title / Role");
            actTable.AddColumn("From");
            actTable.AddColumn("To");
            actTable.AddColumn("Organization");
            actTable.AddColumn("Description");
            actTable.Columns[0].Width(20);
            actTable.Columns[1].Width(10);
            actTable.Columns[2].Width(10);
            actTable.Columns[3].Width(20);
            actTable.Columns[4].Width(30);
            actTable.LeftAligned();

            foreach (CVDetails detail in cv.CVDetails)
            {
                if(detail.Title == "Skill")
                {
                    skillNo += 1;
                    if(skillNo > 1)
                    {
                        skillTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
                    }
                    
                    skillTable.AddRow(detail.JobPosition ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Certificate")
                {
                    certNo += 1;
                    if(certNo > 1)
                    {
                        certTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
                    }
                    certTable.AddRow(detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.JobPosition ?? "");  
                }
                else if(detail.Title == "Work Experience")
                {
                    expNo += 1;
                    if(expNo > 1)
                    {
                        expTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    expTable.AddRow( detail.JobPosition ?? "", detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Education")
                {
                    eduNo += 1;
                    if(eduNo > 1)
                    {
                        eduTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    eduTable.AddRow( detail.JobPosition ?? "", detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Activity")
                {
                    actNo += 1;
                    if(actNo > 1)
                    {
                        actTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    actTable.AddRow( detail.JobPosition ?? "", detail.FromDate?.ToString("dd/MM/yyyy") ?? "", detail.ToDate?.ToString("dd/MM/yyyy") ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
            }
            
            if(skillNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Skills"), skillTable);
            }
            
            if(certNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Certifications"), certTable);
            }

            if(expNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Work Experience"), expTable);
            }

            if(eduNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Education"), eduTable);
            }
            
            if(actNo > 0)
            {
                cvTable.AddRow("--------------------", "----------------------------------------------------------------------------------------------------------");
                cvTable.AddRow(new Markup("Activities"), actTable);
            }
        }
        AnsiConsole.Write(cvTable);
        Console.Write(" Press any key to return: ");
        Console.ReadKey();
        Console.Clear();
    }
}
