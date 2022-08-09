namespace Pl_Console;
using BL;
using Persistence;
using System.Text.RegularExpressions;
using ConsoleTables;

public class RecruiterController
{
    private readonly Regex NumericRegex = new Regex(@"^[0-9]*$");
    private RecruiterBL recruiterBL;

    public RecruiterController()
    {
        recruiterBL = new RecruiterBL();
    }

    public Recruiter GetRecruiter(int? RecruiterID)
    {
        return recruiterBL.GetRecruiterByID(RecruiterID);
    }

    public void CreateNewProfileInformation(int? RecruiterID) 
    {
        string? CompanyName;
        string? PhoneNum;
        string? Position = "staff";
        string? CompanyDescription;
        string? BusinessSize;
        string? BusinessField;
        string? CompanyAddress;


        Console.WriteLine("================================\n");
        Console.WriteLine(" View Your Personal Information");
        Console.WriteLine("\n================================");

        while (true) // PhoneNum
        {
            Console.Write(" Phone Number    : ");
            PhoneNum = UserController.GetUserInput();
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
        while (true) // CompanyName
        {
            Console.Write(" Company Name       : ");
            CompanyName = UserController.GetUserInput();
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
            CompanyAddress = UserController.GetUserInput();
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
            switch (UserController.GetUserInput())
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
            CompanyDescription = UserController.GetUserInput();
            if(CompanyDescription.Length > 5000)
            {
                Console.WriteLine("\n Company Description is too long. Maximum characters allowed is 5000\n");
            }
            else
            {
                break;
            }
        }
        while (true) // BusinessSize
        {
            Console.Write(" Business Size     : ");
            BusinessSize = UserController.GetUserInput();
            if(BusinessSize.Length > 50)
            {
                Console.WriteLine("\n Business Size is too long. Maximum characters allowed is 50\n");
            }
            else
            {
                break;
            }
        }
        while (true) // BusinessField
        {
            Console.Write(" Business Field    : ");
            BusinessField = UserController.GetUserInput();
            if(BusinessField.Length > 100)
            {
                Console.WriteLine("\n Business Field is too long. Maximum characters allowed is 100\n");
            }
            else
            {
                break;
            }
        }
        
        Recruiter newProfile = new Recruiter(PhoneNum, Position, CompanyName, CompanyAddress, CompanyDescription, BusinessSize, BusinessField);
        recruiterBL.CreateNewProfile(newProfile, RecruiterID);
        
    }
    
    public void DisplayNewsForRecruter(int? RecruiterID)
    {  
        List<RecruitNews> recruitNews = recruiterBL.GetRecruitNewByID(RecruiterID);
        if (recruitNews != null)
        {
            while (true)
            {
                Menu.PrintSubMenu(4, recruitNews);
                string choice = UserController.GetUserInput();

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
        string? SalaryRange = "Below 3 million";
        string? FormOfEmploy;
        string? Gender;
        string? HiringAmount;
        string? HiringPosition = "Staff";
        string? RequiredExp;
        string? CityAddress = "Ha Noi";
        string? Profession = "Seller";
        bool end = false;
        
        Console.WriteLine("================================\n");
        Console.WriteLine("      CREATE RECRUITMENT NEW");
        Console.WriteLine("\n================================");

        while (true) // NewsName
        {
            Console.Write(" News Name         : ");
            NewsName = UserController.GetUserInput();
            if(NewsName.Length > 200)
            {
                Console.WriteLine("\n Name is too long. Maximum characters allowed is 200\n");
            }
            else
            {
                break;
            }
        }
        while (true) // DeadLine
        {
            Console.Write(" Deadline         : ");
            DeadLine = UserController.GetUserInput();
            if(DeadLine.Length > 50)
            {
                Console.WriteLine("\n Deadline is too long. Maximum characters allowed is 50\n");
            }
            else
            {
                break;
            }
        }
        while (true) // SalaryRange
        {
            bool endSalaryRange = false;
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    SalaryRange = "Below 3 million";
                    endSalaryRange = true;
                    break;
                case "2":
                    SalaryRange = "3 - 5 million";
                    endSalaryRange = true;
                    break;
                case "3":
                    SalaryRange =  "5 - 7 million";
                    endSalaryRange = true;
                    break;
                case "4":
                    SalaryRange =  "7 - 10 million";
                    endSalaryRange = true;
                    break;
                case "5":
                    SalaryRange = "Higher than 10 million";
                    endSalaryRange = true;
                    break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }

            if (endSalaryRange == true)
            {
                break;
            }
        }
        while (true) // FormOfEmploy
        {
            Console.Write(" Form Of Employ   : ");
            FormOfEmploy = UserController.GetUserInput();
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
            Gender = UserController.GetUserInput();
            if(Gender.Length > 50)
            {
                Console.WriteLine("\n Gender is too long. Maximum characters allowed is 50\n");
            }
            else
            {
                break;
            }
        }
        while (true) // HiringAmount
        {
            Console.Write(" Hiring Amount    : ");
            HiringAmount = UserController.GetUserInput();
            if(HiringAmount.Length > 30)
            {
                Console.WriteLine("\n Hiring Amount is too long. Maximum characters allowed is 30\n");
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
            switch (UserController.GetUserInput()) 
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

            RequiredExp = UserController.GetUserInput();
            if(RequiredExp.Length > 200)
            {
                Console.WriteLine("\n Required Exp is too long. Maximum characters allowed is 200\n");
            }
            else
            {
                break;
            }
        }
        while (true) // CityAddress
        {
            bool endAddress = false;
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

            switch (UserController.GetUserInput())
            {
                case "1":
                    CityAddress = "Ha Noi";
                    endAddress = true;
                    break;
                case "2":
                    CityAddress = "Ho Chi Minh";
                    endAddress = true;
                    break;
                case "3":
                    CityAddress =  "Binh Duong";
                    endAddress = true;
                    break;
                case "4":
                    CityAddress =  "Bac Ninh";
                    endAddress = true;
                    break;
                case "5":
                    CityAddress = "Dong Nai";
                    endAddress = true;
                    break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }

            if (endAddress == true)
            {
                break;
            }
        }
        while (true) // Profession
        {
            bool endProfession = false;
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

            switch (UserController.GetUserInput())
            {
                case "1":
                    Profession = "Seller";
                    endProfession = true;
                    break;
                case "2":
                    Profession = "Translator";
                    endProfession = true;
                    break;
                case "3":
                    Profession =  "Journalist";
                    endProfession = true;
                    break;
                case "4":
                    Profession =  "Post and Telecommunications";
                    endProfession = true;
                    break;
                case "5":
                    Profession = "Insurance";
                    endProfession = true;
                    break;
                default:
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid choice! Please re-enter your option.");
                    break;
            }

            if (endProfession == true)
            {
                break;
            }
        }
    
        RecruitNews News = new RecruitNews(NewsName, DeadLine, FormOfEmploy, Gender, HiringAmount, 
                                            HiringPosition, RequiredExp, SalaryRange, CityAddress, Profession);
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
            
            switch (UserController.GetUserInput())
            {
                case "1": //NewsName
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" News Name       : ");
                        string NewsName = UserController.GetUserInput();
                        if(NewsName.Length > 200)
                        {
                            Console.WriteLine("\n News Name is too long. Maximum characters allowed is 200\n");
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
                        string DeadLine = UserController.GetUserInput();
                        if(DeadLine.Length > 50)
                        {
                            Console.WriteLine("\n News Name is too long. Maximum characters allowed is 50\n");
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
                        string FormOfEmploy = UserController.GetUserInput();
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
                        string Gender = UserController.GetUserInput();
                        if(Gender.Length > 50)
                        {
                            Console.WriteLine("\n Gender is too long. Maximum characters allowed is 50\n");
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
                        string HiringAmount = UserController.GetUserInput();
                        if(HiringAmount.Length > 30)
                        {
                            Console.WriteLine("\n Hiring Amount is too long. Maximum characters allowed is 30\n");
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
                        switch (UserController.GetUserInput()) 
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
                        string RequiredExp = UserController.GetUserInput();
                        if(RequiredExp.Length > 200)
                        {
                            Console.WriteLine("\n Required Experiences is too long. Maximum characters allowed is 200\n");
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
                        Console.WriteLine(" 1, Open  ");
                        Console.WriteLine(" 2, Close  ");
                        Console.WriteLine("================================");
                        Console.Write(" Enter only number here (1-2): ");
                            switch (UserController.GetUserInput())
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
                    while (true)
                    {
                        bool endSalaryRange = false;
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
                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                news.SalaryRange = "Below 3 million";
                                endSalaryRange = true;
                                break;
                            case "2":
                                news.SalaryRange = "3 - 5 million";
                                endSalaryRange = true;
                                break;
                            case "3":
                                news.SalaryRange =  "5 - 7 million";
                                endSalaryRange = true;
                                break;
                            case "4":
                                news.SalaryRange =  "7 - 10 million";
                                endSalaryRange = true;
                                break;
                            case "5":
                                news.SalaryRange = "Higher than 10 million";
                                endSalaryRange = true;
                                break;
                            default:
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                break;
                        }

                        if (endSalaryRange == true)
                        {
                            break;
                        }
                    }
                    break;
                case "10": //CityAddress
                    while (true)
                    {
                        bool endAddress = false;
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

                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                news.CityAddress = "Ha Noi";
                                endAddress = true;
                                break;
                            case "2":
                                news.CityAddress = "Ho Chi Minh";
                                endAddress = true;
                                break;
                            case "3":
                                news.CityAddress =  "Binh Duong";
                                endAddress = true;
                                break;
                            case "4":
                                news.CityAddress =  "Bac Ninh";
                                endAddress = true;
                                break;
                            case "5":
                                news.CityAddress = "Dong Nai";
                                endAddress = true;
                                break;
                            default:
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                break;
                        }

                        if (endAddress == true)
                        {
                            break;
                        }
                    }
                    break;
                case "11": //Profession
                    while (true) // Profession
                    {
                        bool endProfession = false;
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

                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                news.Profession = "Seller";
                                endProfession = true;
                                break;
                            case "2":
                                news.Profession = "Translator";
                                endProfession = true;
                                break;
                            case "3":
                                news.Profession =  "Journalist";
                                endProfession = true;
                                break;
                            case "4":
                                news.Profession =  "Post and Telecommunications";
                                endProfession = true;
                                break;
                            case "5":
                                news.Profession = "Insurance";
                                endProfession = true;
                                break;
                            default:
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid choice! Please re-enter your option.");
                                break;
                        }

                        if (endProfession == true)
                        {
                            break;
                        }
                    }
                    break;
                case "R" or "r":
                    DisplaySearchedCVs(recruiterBL.GetCVAppliedInNews(news.NewsID), RecruiterID);
                    break;
                case "0":
                    while(true)
                    {    
                        Console.WriteLine("================================");
                        Console.WriteLine(" 1) Confirm changes");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (UserController.GetUserInput())
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    DisplaySearchedCVs(SearchCVViaCareerTitle(RecruiterID), RecruiterID);
                    break;
                case "2":
                    DisplaySearchedCVs(SearchCVViaAddress(RecruiterID), RecruiterID);
                    break;
                case "3":
                    DisplaySearchedCVs(SearchCVViaJobPosition(RecruiterID), RecruiterID);
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

    public List<CV> SearchCVViaJobPosition(int? RecruiterID) 
    {
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("       SEARCH JOB POSITION");
            Console.WriteLine("\n================================");
            Console.Write("Enter the key word: ");
            string? keyword = UserController.GetUserInput();
            if (keyword == "0"){
                    SearchCVs(RecruiterID);
            }
            else {
                return recruiterBL.GetCVByJobPosition(keyword);
            }
        }
    }

    public List<CV> SearchCVViaCareerTitle(int? RecruiterID){
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("       SEARCH CAREER TITLE");
            Console.WriteLine("\n================================");
            Console.Write("Enter the key word or press 0 to back to search menu: ");
            string? keyword = UserController.GetUserInput();
            if (keyword == "0"){
                    SearchCVs(RecruiterID);
            }
            else {
                return recruiterBL.GetCVByCareerTitle(keyword);
            }
            
        }
    }
    
    public List<CV> SearchCVViaAddress(int? RecruiterID){ 
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("         SEARCH ADDRESS");
            Console.WriteLine("\n================================");
            Console.Write("Enter the key word or press 0 to back to search menu: ");
            string? keyword = UserController.GetUserInput();
            if (keyword == "0"){
                    SearchCVs(RecruiterID);
            }
            else {
                return recruiterBL.GetCVByAddress(keyword);
            }
        }
    }

    public void DisplaySearchedCVs(List<CV> cv, int? RecruiterID)
    {  
        if (cv != null)
        {
            while (true)
            {
                Menu.PrintSubMenu(6, cv);
                string choice = UserController.GetUserInput();

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
                switch (UserController.GetUserInput())
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

    public void ViewProfileInformation(Recruiter recruiter){ 
        bool end = false;
        while (true)
        {
            Menu.PrintSubMenu(5, recruiter);
            
            switch (UserController.GetUserInput())
            {
                case "1": // CompanyName
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Company Name       : ");
                        string CompanyName = UserController.GetUserInput();
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
                            switch (UserController.GetUserInput())
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
                        string PhoneNum = UserController.GetUserInput();
                        if(PhoneNum.Length > 10)
                        {
                            Console.WriteLine("\n Phone Number is too long. Maximum characters allowed is 10\n");
                        }
                        else if (string.IsNullOrEmpty(PhoneNum))
                        {
                            Console.WriteLine("\n Phone Number can't be left empty.\n");
                        }
                        else
                        {
                            if (NumericRegex.IsMatch(PhoneNum))
                            {
                                recruiter.PhoneNum = PhoneNum;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n Phone number is numbers only.\n");
                            }
                        }
                    }
                    break;
                case "4": // CompanyDescription
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Company Description       : ");
                        string CompanyDescription = UserController.GetUserInput();
                        if(CompanyDescription.Length > 5000)
                        {
                            Console.WriteLine("\n Company Description is too long. Maximum characters allowed is 5000\n");
                        }
                        else
                        {
                            recruiter.CompanyDescription = CompanyDescription;
                            break;
                        }
                    }
                    break;
                case "5": // BusinessSize
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Business Size       : ");
                        string BusinessSize = UserController.GetUserInput();
                        if(BusinessSize.Length > 50)
                        {
                            Console.WriteLine("\n Business Size is too long. Maximum characters allowed is 50\n");
                        }
                        else
                        {
                            recruiter.BusinessSize = BusinessSize;
                            break;
                        }
                    }
                    break;
                case "6": // BusinessField
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Business Field       : ");
                        string BusinessField = UserController.GetUserInput();
                        if(BusinessField.Length > 100)
                        {
                            Console.WriteLine("\n Business Field is too long. Maximum characters allowed is 100\n");
                        }
                        else
                        {
                            recruiter.BusinessField = BusinessField;
                            break;
                        }
                    }
                    break;
                case "7": // CompanyAddress
                    Console.WriteLine("================================");
                    while (true)
                    {
                        Console.Write(" Company Address       : ");
                        string CompanyAddress = UserController.GetUserInput();
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
                        switch (UserController.GetUserInput())
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
