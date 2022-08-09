namespace Pl_Console;
using Persistence;
public class Menu
{
    public static Dictionary<int, Delegate> MainMenus = new Dictionary<int, Delegate>()
    {
        // add functions to print main menus here
        {1, new Action(StartMenu)},
        {2, new Action<int?>(CandidateMenu)},
        {3, new Action<int?>(RecruiterMenu)}
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

    // NCT
    public static void RecruiterMenu(int? RecruiterID)   
    {
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
                    recruiterController.ViewProfileInformation(RecruiterID);
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
}
