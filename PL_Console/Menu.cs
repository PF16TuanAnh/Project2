namespace Pl_Console;
using Persistence;
public static class Menu
{
    public static Dictionary<int, Delegate> MainMenus = new Dictionary<int, Delegate>()
    {
        // add functions to print main menus here
        {1, new Action(StartMenu)},
        {2, new Action<int?>(CandidateMenu)},
        {3, new Action<int?>(RecruiterMenu)}
    };
    public static Dictionary<int, Action> SubMenus = new Dictionary<int, Action>()
    {
        // add functions to print sub menus here
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

    public static void PrintSubMenu(int i)
    {
        SubMenus[i]();
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

    private static void RecruiterMenu(int? RecruiterID)
    {
        string username = "temp";
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine(" Username: {0}", username);
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) View Personal Information");
            Console.WriteLine(" 2) Add Recruitment News");
            Console.WriteLine(" 3) View Your Recruitment News");
            Console.WriteLine(" 4) Search CVs");
            Console.WriteLine(" 0) Exit");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (UserController.GetUserInput())
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
}