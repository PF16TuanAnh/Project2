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
        {3, new Action<List<RecruitNews>>(SearchedRecruitNewsMenu)},
        {4, new Action<List<RecruitNews>>(ViewAddedNewsMenu)},
        {5, new Action<Recruiter>(ViewProfileMenu)},
        {6, new Action<List<CV>>(SearchedCVsMenu)},
        {7, new Action<CV>(ViewSelectedCVMenu)}
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
                        candidateController.SearchRecruitNews(CandidateID);
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
        personalInfoTable.AddRow("Job Position", cv.CareerTitle ?? "");
        personalInfoTable.AddRow("--------------------", "-------------------------------------------------------------------------------");
        personalInfoTable.AddRow("Date of Birth", cv.BirthDate ?? "");
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
                    certTable.AddRow(detail.JobPosition ?? "");  
                }
                else if(detail.Title == "Work Experience")
                {
                    expNo += 1;
                    if(expNo > 1)
                    {
                        expTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    expTable.AddRow(detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Education")
                {
                    eduNo += 1;
                    if(eduNo > 1)
                    {
                        eduTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    eduTable.AddRow(detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Activity")
                {
                    actNo += 1;
                    if(actNo > 1)
                    {
                        actTable.AddRow("--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    actTable.AddRow(detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
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

            AnsiConsole.Write(cvTable);
        }
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
                    certTable.AddRow(certNo.ToString(), detail.JobPosition ?? "");  
                }
                else if(detail.Title == "Work Experience")
                {
                    expNo += 1;
                    if(expNo > 1)
                    {
                        expTable.AddRow("----", "--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    expTable.AddRow(expNo.ToString(), detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Education")
                {
                    eduNo += 1;
                    if(eduNo > 1)
                    {
                        eduTable.AddRow("----", "--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    eduTable.AddRow(eduNo.ToString(), detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                }
                else if(detail.Title == "Activity")
                {
                    actNo += 1;
                    if(actNo > 1)
                    {
                        actTable.AddRow("----", "--------------------", "----------", "----------", "--------------------", "------------------------------");
                    }
                    actTable.AddRow(actNo.ToString(), detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
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
                if(certNo != 0)
                {
                    cvTable.AddRow("--------------------", "--------------------------------------------------------------------------------------------------------------");
                }
                cvTable.AddRow(new Markup("Work Experience"), expTable);
            }

            if(eduNo > 0)
            {
                if(expNo != 0)
                {
                    cvTable.AddRow("--------------------", "--------------------------------------------------------------------------------------------------------------");
                }
                cvTable.AddRow(new Markup("Education"), eduTable);
            }
            
            if(actNo > 0)
            {
                if(eduNo != 0)
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

    private static void SearchedRecruitNewsMenu(List<RecruitNews> recruitNews)
    {
        var table = new Table();
        table.AddColumn("Pos");
        table.AddColumn("Name");
        table.Columns[0].Width(4).Centered();
        table.Columns[1].Width(40);

        int count = 0;
        Console.WriteLine("==================================================");
        Console.WriteLine("                 RECRUITMENT NEWS");
        Console.WriteLine("==================================================");
        foreach (RecruitNews news in recruitNews)
        {
            count += 1;
            if(count > 1)
            {
                table.AddRow("----", "----------------------------------------");
            }
            table.AddRow(count.ToString(), news.NewsName);
        }
        table.LeftAligned();
        AnsiConsole.Write(table);
        Console.WriteLine("==================================================");
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

    private static void ViewSelectedCVMenu(CV cv)
    {
        Console.Clear();
        // create table for Full Name, Job Position, Date of Birth
        var table5 = new Table();
        table5.Title("[yellow]Personal Information[/]");
        table5.AddColumn("[red] Full Name[/]");
        table5.AddColumn("[red] Job Position[/]");
        table5.AddColumn("[red] Date of Birth[/]");
        table5.Columns[0].Width(35);
        table5.Columns[1].Width(35);
        table5.Columns[2].Width(33);
        table5.AddRow(cv.FullName ?? "", cv.CareerTitle ?? "", cv.BirthDate ?? "");
        table5.LeftAligned();

        // create table for Career's Objective
        var table6 = new Table();
        table6.Title("[yellow]Objective[/]");
        table6.AddColumn("[red]Career's Objective[/]");
        table6.Columns[0].Width(109);
        table6.AddRow(cv.CareerObjective ?? "");
        table6.LeftAligned();

        // create table for Contact Details
        var table7 = new Table();
        table7.Title("[yellow]Contact Details[/]");
        table7.AddColumn("[red] Phone Number[/]");
        table7.AddColumn("[red] Email Address[/]");
        table7.AddColumn("[red] Social Media[/]");
        table7.AddColumn("[red] Address[/]");
        table7.Columns[0].Width(15);
        table7.Columns[1].Width(25);
        table7.Columns[2].Width(30);
        table7.Columns[3].Width(30);
        table7.AddRow(cv.PhoneNum ?? "", cv.Email ?? "", cv.SocialMedia ?? "", cv.PersonalAddress ?? "");
        table7.LeftAligned();

        if (cv != null)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           SELECTED CV");
            Console.WriteLine("\n================================");
            AnsiConsole.Write(table5);
            AnsiConsole.Write(table7);
            AnsiConsole.Write(table6);

            if(cv.CVDetails != null)
            {
                int skillNo = 0;
                int certNo = 0;
                int expNo = 0;
                int eduNo = 0;
                int actNo = 0;

                // create table for Skills
                var table = new Table();
                table.Title("[yellow]Skills[/]");
                table.AddColumn("[blue]No[/]");
                table.AddColumn("[red]Skill Group Name[/]");
                table.AddColumn("[red]Skill Description[/]");
                table.Columns[0].Width(4).Centered();
                table.Columns[1].Width(20);
                table.Columns[2].Width(79);
                table.LeftAligned();

                // create table for Certifications
                var table4 = new Table();
                table4.Title("[yellow]Certifications[/]");
                table4.AddColumn("[blue]No[/]");
                table4.AddColumn("[red]Time[/]");
                table4.AddColumn("[red]Certification Name[/]");
                table4.Columns[0].Width(4).Centered();
                table4.Columns[1].Width(20);
                table4.Columns[2].Width(79);
                table4.LeftAligned();

                // create table for Work Experience
                var table1 = new Table();
                table1.Title("[yellow]Work Experience[/]");
                table1.AddColumn("[blue]No[/]");
                table1.AddColumn("[red]Title / Position[/]");
                table1.AddColumn("[red]From[/]");
                table1.AddColumn("[red]To[/]");
                table1.AddColumn("[red]Company Name[/]");
                table1.AddColumn("[red]Description[/]");
                table1.Columns[0].Width(4).Centered();
                table1.Columns[1].Width(20);
                table1.Columns[2].Width(10);
                table1.Columns[3].Width(10);
                table1.Columns[4].Width(20);
                table1.Columns[5].Width(30);
                table1.LeftAligned();

                // create table for Education
                var table2 = new Table();
                table2.Title("[yellow]Education[/]");
                table2.AddColumn("[blue]No[/]");
                table2.AddColumn("[red]Degree / Study Field[/]");
                table2.AddColumn("[red]From[/]");
                table2.AddColumn("[red]To[/]");
                table2.AddColumn("[red]School[/]");
                table2.AddColumn("[red]Description[/]");
                table2.Columns[0].Width(4).Centered();
                table2.Columns[1].Width(20);
                table2.Columns[2].Width(10);
                table2.Columns[3].Width(10);
                table2.Columns[4].Width(20);
                table2.Columns[5].Width(30);
                table2.LeftAligned();

                // create table for Activities
                var table3 = new Table();
                table3.Title("[yellow]Activities[/]");
                table3.AddColumn("[blue]No[/]");
                table3.AddColumn("[red]Title / Role[/]");
                table3.AddColumn("[red]From[/]");
                table3.AddColumn("[red]To[/]");
                table3.AddColumn("[red]Organization[/]");
                table3.AddColumn("[red]Description[/]");
                table3.Columns[0].Width(4).Centered();
                table3.Columns[1].Width(20);
                table3.Columns[2].Width(10);
                table3.Columns[3].Width(10);
                table3.Columns[4].Width(20);
                table3.Columns[5].Width(30);
                table3.LeftAligned();
                foreach (CVDetails detail in cv.CVDetails)
                {
                    if(detail.Title == "Skill")
                    {
                        skillNo += 1;
                        table.AddRow("[green]" + skillNo.ToString() + "[/]", detail.JobPosition ?? "", detail.Description ?? "");  
                    }
                    else if(detail.Title == "Certificate")
                    {
                        certNo += 1;
                        table4.AddRow("[green]" + certNo.ToString() + "[/]", detail.FromDate ?? "", detail.JobPosition ?? "");  
                    }
                    else if(detail.Title == "Work Experience")
                    {
                        expNo += 1;
                        table1.AddRow("[green]" + expNo.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                    }
                    else if(detail.Title == "Education")
                    {
                        eduNo += 1;
                        table2.AddRow("[green]" + eduNo.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                    }
                    else if(detail.Title == "Activity")
                    {
                        actNo += 1;
                        table3.AddRow("[green]" + actNo.ToString() + "[/]", detail.JobPosition ?? "", detail.FromDate ?? "", detail.ToDate ?? "", detail.Association ?? "", detail.Description ?? "");  
                    }
                }
                
                if(skillNo > 0)
                {
                    AnsiConsole.Write(table);
                }
                
                if(certNo > 0)
                {
                    AnsiConsole.Write(table4);
                }

                if(expNo > 0)
                {
                    AnsiConsole.Write(table1);
                }

                if(eduNo > 0)
                {
                    AnsiConsole.Write(table2);
                }
                
                if(actNo > 0)
                {
                    AnsiConsole.Write(table3);
                }
            }
            Console.Write(" Press any key to return: ");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred.");
        }
    }
}
