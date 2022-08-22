namespace Pl_Console;
using BL;
using Persistence;
using System.Text.RegularExpressions;
using System.Globalization;

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
        Console.Clear();
        string? CompanyName;
        string? PhoneNum;
        string? Position = "staff";
        string? CompanyDescription;
        string? BusinessSize;
        string? BusinessField;
        string? CompanyAddress;

        while (true) // PhoneNum
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        INSERT INFORMATION");
            Console.WriteLine("\n================================");
            Console.Write(" Phone Number: ");
            PhoneNum = UserController.GetUserInput();
            if (string.IsNullOrEmpty(PhoneNum))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Phone Number can't be left empty.");
            }
            else if(PhoneNum.Length != 10)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Phone Number must be 10 numerics.");
            }
            else
            {
                if (NumericRegex.IsMatch(PhoneNum))
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Phone Number must be numbers only.");
                }
            }
        }

        Console.Clear();
        while (true) // CompanyName
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        INSERT INFORMATION");
            Console.WriteLine("\n================================");
            Console.Write(" Company Name: ");
            CompanyName = UserController.GetUserInput();
            if(CompanyName.Length > 100)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Company Name is too long. Maximum characters allowed is 100.");
            }
            else if (string.IsNullOrEmpty(CompanyName))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Company Name can't be left empty.");
            }
            else
            {
                break;
            }
        }

        Console.Clear();
        while (true) // CompanyAddress
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        INSERT INFORMATION");
            Console.WriteLine("\n================================");
            Console.Write(" Company Address: ");
            CompanyAddress = UserController.GetUserInput();
            if(CompanyAddress.Length > 100)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Company Address is too long. Maximum characters allowed is 100.");
            }
            else if (string.IsNullOrEmpty(CompanyAddress))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Company Address can't be left empty.");
            }
            else
            {
                break;
            }
        }

        Console.Clear();
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
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    end = false;
                    break;
            }
            if (end == true)
            {
                break;
            }
        }

        Console.Clear();
        while (true) // CompanyDescription
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        INSERT INFORMATION");
            Console.WriteLine("\n================================");
            Console.Write(" Company Description: ");
            CompanyDescription = UserController.GetUserInput();
            if(CompanyDescription.Length > 5000)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" Company Description is too long. Maximum characters allowed is 5000.");
            }
            else
            {
                break;
            }
        }

        Console.Clear();
        while (true) // BusinessSize
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        INSERT INFORMATION");
            Console.WriteLine("\n================================");
            Console.Write(" Business Size: ");
            BusinessSize = UserController.GetUserInput();
            if(BusinessSize.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" Business Size is too long. Maximum characters allowed is 50.");
            }
            else if (string.IsNullOrEmpty(BusinessSize))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Business Size can't be left empty.");
            }
            else
            {
                break;
            }
        }

        Console.Clear();
        while (true) // BusinessField
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("        INSERT INFORMATION");
            Console.WriteLine("\n================================");
            Console.Write(" Business Field: ");
            BusinessField = UserController.GetUserInput();
            if(BusinessField.Length > 100)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" Business Field is too long. Maximum characters allowed is 100.");
            }
            else if (string.IsNullOrEmpty(BusinessField))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Business Field can't be left empty.");
            }
            else
            {
                break;
            }
        }
        
        Recruiter newProfile = new Recruiter(PhoneNum, Position, CompanyName, CompanyDescription, CompanyAddress, BusinessSize, BusinessField);
        recruiterBL.CreateNewProfile(newProfile, RecruiterID);
        
    }
    
    public void DisplayNewsForRecruter(int? RecruiterID)
    {
        List<RecruitNews> recruitNews = recruiterBL.GetRecruitNewByID(RecruiterID);
        if (recruitNews != null)
        {
            Console.Clear();
            while (true)
            {
                Menu.PrintSubMenu(4, recruitNews);
                Console.Write(" Enter the position of news you like to view or 0 to return: ");
                string choice = UserController.GetUserInput();

                if(choice == "0")
                {
                    Console.Clear();
                    break;
                }

                bool success = int.TryParse(choice, out int position);
                if(success)
                {
                    if((position - 1) < recruitNews.Count() && position > 0)
                    {
                        Console.Clear();
                        ViewRecruitmentNews(recruitNews[position - 1], RecruiterID);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" Your chosen target doesn't exist.");
                        Console.WriteLine("================================");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    Console.WriteLine("================================");
                }
            }
        }
    }

    public void AddRecruitmentNews(int? RecruiterID)
    { 
        Console.Clear();
        string? NewsName;
        DateTime DeadLine = new DateTime();
        string? SalaryRange = "Below 3 million";
        string? FormOfEmploy = "Working full-time";
        string? Gender;
        string? HiringAmount;
        string? HiringPosition = "Staff";
        string? RequiredExp;
        string? CityAddress = "Ha Noi";
        string? Profession = "Seller";
        bool end = false;

        string _DeadLine;
        
        while (true) // NewsName
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("      CREATE RECRUITMENT NEW");
            Console.WriteLine("\n================================");
            Console.WriteLine(" Enter 0 to cancel.");
            Console.Write(" News Name: ");
            NewsName = UserController.GetUserInput();
            
            if (NewsName == "0")
            {
                break;
            }

            if(NewsName.Length > 200)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Name is too long. Maximum characters allowed is 200.");
            }
            else if (String.IsNullOrEmpty(NewsName))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" News Name cannot be left empty.");
            }
            else
            {
                break;
            }
        }

        if(NewsName != "0")
        {
            Console.Clear();
            while (true) // DeadLine
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("      CREATE RECRUITMENT NEW");
                Console.WriteLine("\n================================");
                Console.WriteLine(" Enter 0 to cancel.");
                Console.Write(" Deadline (dd/MM/yyyy): ");
                _DeadLine = UserController.GetUserInput();
                
                if (_DeadLine == "0")
                {
                    break;
                }

                try
                {
                    DeadLine = DateTime.ParseExact(_DeadLine, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    break;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" The correct formart is dd/MM/yyyy.");
                }
            }
        }
        else
        {
            _DeadLine = "0";
        }

        if(_DeadLine != "0")
        {
            Console.Clear();
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
                Console.WriteLine(" 0) Cancel");
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
                    case "0":
                        SalaryRange = "0";
                        endSalaryRange = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid input! Please re-enter your option.");
                        break;
                }

                if (endSalaryRange == true)
                {
                    break;
                }
            }
        }
        else
        {
            SalaryRange = "0";
        }

        if(SalaryRange != "0")
        {
            Console.Clear();
            while (true) // FormOfEmploy
            {
                bool endForm = false;
                Console.WriteLine("================================\n");
                Console.WriteLine("        FORM OF EMPLOYMENT");
                Console.WriteLine("\n================================");
                Console.WriteLine(" 1) Working full-time");
                Console.WriteLine(" 2) Working part-time");
                Console.WriteLine(" 0) Cancel");
                Console.WriteLine("================================");
                Console.Write(" Enter the option number: ");
                switch (UserController.GetUserInput())
                {
                    case "1":
                        FormOfEmploy = "Working full-time";
                        endForm = true;
                        break;
                    case "2":
                        FormOfEmploy = "Working part-time";
                        endForm = true;
                        break;
                    case "0":
                        FormOfEmploy = "0";
                        endForm = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid input! Please re-enter your option.");
                        break;
                }

                if (endForm == true)
                {
                    break;
                }
            }
        }
        else
        {
            FormOfEmploy = "0";
        }

        if(FormOfEmploy != "0")
        {
            Console.Clear();
            while (true) // Gender
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("      CREATE RECRUITMENT NEW");
                Console.WriteLine("\n================================");
                Console.WriteLine(" Enter 0 to cancel.");
                Console.Write(" Gender: ");
                Gender = UserController.GetUserInput();
                
                if (Gender == "0")
                {
                    break;
                }

                if(Gender.Length > 50)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Gender is too long. Maximum characters allowed is 50.");
                }
                else if (String.IsNullOrEmpty(Gender))
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Gender cannot be left empty.");
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            Gender = "0";
        }

        if(Gender != "0")
        {
            Console.Clear();
            while (true) // HiringAmount
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("      CREATE RECRUITMENT NEW");
                Console.WriteLine("\n================================");
                Console.WriteLine(" Enter 0 to cancel.");
                Console.Write(" Hiring Amount: ");
                HiringAmount = UserController.GetUserInput();
                
                if (HiringAmount == "0")
                {
                    break;
                }

                if(HiringAmount.Length > 30)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Hiring Amount is too long. Maximum characters allowed is 30.");
                }
                else if (String.IsNullOrEmpty(HiringAmount))
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Hiring Amount cannot be left empty.");
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            HiringAmount = "0";
        }

        if(HiringAmount != "0")
        {
            Console.Clear();
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
                Console.WriteLine(" 0) Cancel");
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
                    case "0":
                        HiringPosition = "0";
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
        }
        else
        {
            HiringPosition = "0";
        }

        if(HiringPosition != "0")
        {
            Console.Clear();
            while (true) // RequiredExp
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("      CREATE RECRUITMENT NEW");
                Console.WriteLine("\n================================");
                Console.WriteLine(" Enter 0 to cancel.");
                Console.Write(" Required Experience: ");
                RequiredExp = UserController.GetUserInput();
                
                if (RequiredExp == "0")
                {
                    break;
                }

                if(RequiredExp.Length > 200)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Required Experience is too long. Maximum characters allowed is 200.");
                }
                else if (String.IsNullOrEmpty(RequiredExp))
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Required Experience cannot be left empty.");
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            RequiredExp = "0";
        }

        if(RequiredExp != "0")
        {
            Console.Clear();
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
                Console.WriteLine(" 0) Cancel");
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
                    case "0":
                        CityAddress = "0";
                        endAddress = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid input! Please re-enter your option.");
                        break;
                }

                if (endAddress == true)
                {
                    break;
                }
            }
        }
        else
        {
            CityAddress = "0";
        }

        if(CityAddress != "0")
        {
            Console.Clear();
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
                Console.WriteLine(" 0) Cancel");
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
                    case "0":
                        Profession = "0";
                        endProfession = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid input! Please re-enter your option.");
                        break;
                }

                if (endProfession == true)
                {
                    break;
                }
            }
        }
        else
        {
            Profession = "0";
        }

        if (NewsName != "0" && _DeadLine != "0" && FormOfEmploy != "0" && Gender != "0" && HiringAmount != "0" && HiringPosition != "0" && RequiredExp != "0" &&
         SalaryRange != "0" && CityAddress != "0" && Profession != "0")
        {
            RecruitNews News = new RecruitNews(NewsName, DeadLine, FormOfEmploy, Gender, HiringAmount, 
                                            HiringPosition, RequiredExp, SalaryRange, CityAddress, Profession);
        recruiterBL.CreateRecruitment(News, RecruiterID);
        }
        else
        {
            Console.Clear();
        }
    }

    public void ViewRecruitmentNews(RecruitNews news, int? RecruiterID)
    {
        bool end = false;
        while(true)
        {
            Menu.PrintSubMenu(8, news);
            Console.Write(" Enter 1 to Update the details or View applied candidates, 0 to return: ");
            switch (UserController.GetUserInput())
            {
                case "1":
                    UpdateRecruitmentNews(news, RecruiterID);
                    break;
                case "0":
                    Console.Clear();
                    while(true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("        CONFIRM DETAILS");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" If there are changes, you can choose Confirm to save them.");
                        Console.WriteLine(" 1) Confirm");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                Console.Clear();
                                recruiterBL.UpdateNewsInfo(news);
                                end = true;
                                break;
                            case "0":
                                Console.Clear();
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
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("============================================="); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    Console.WriteLine("============================================="); 
                    break;
            }

            if (end == true)
            {
                break;
            }
        }
    }

    public void UpdateRecruitmentNews(RecruitNews news, int? RecruiterID)
    {
        Console.Clear();  
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("             UPDATE");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) News Name");
            Console.WriteLine(" 2) Deadline");
            Console.WriteLine(" 3) Form Of Employment");
            Console.WriteLine(" 4) Gender");
            Console.WriteLine(" 5) Hiring Amount");
            Console.WriteLine(" 6) Hiring Position");
            Console.WriteLine(" 7) Required Experience");
            Console.WriteLine(" 8) Salary Range");
            Console.WriteLine(" 9) City Address");
            Console.WriteLine(" 10) Profession");
            Console.WriteLine(" 11) Status");
            Console.WriteLine(" R) View Applied CV");
            Console.WriteLine(" 0) Return ");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number to change the details: ");
            
            switch (UserController.GetUserInput())
            {
                case "1": //NewsName
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old News Name: {0}", news.NewsName);
                        Console.Write(" News Name: ");
                        string NewsName = UserController.GetUserInput();
                        
                        if (NewsName == "0")
                        {
                            break;
                        }

                        if(NewsName.Length > 200)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" News Name is too long. Maximum characters allowed is 200.");
                        }
                        else if (String.IsNullOrEmpty(NewsName))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" News Name cannot be left empty.");
                        }
                        else
                        {
                            news.NewsName = NewsName;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "2": //DeadLine
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Deadline: {0}", news.DeadLine.ToString("dd/MM/yyyy"));
                        Console.Write(" Deadline (dd/MM/yyyy): ");
                        string DeadLine = UserController.GetUserInput();
                        
                        if (DeadLine == "0")
                        {
                            break;
                        }

                        try
                        {
                            news.DeadLine = DateTime.ParseExact(DeadLine, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            break;
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" The correct formart is dd/MM/yyyy.");
                        }
                    }
                    Console.Clear();
                    break;
                case "3": //FormOfEmploy
                    Console.Clear();
                    while (true)
                    {
                        bool endForm = false;
                        Console.WriteLine("================================\n");
                        Console.WriteLine("        FORM OF EMPLOYMENT");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Old Form Of Employment: {0}", news.FormOfEmploy);
                        Console.WriteLine(" 1) Working full-time");
                        Console.WriteLine(" 2) Working part-time");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                news.FormOfEmploy = "Working full-time";
                                endForm = true;
                                break;
                            case "2":
                                news.FormOfEmploy = "Working part-time";
                                endForm = true;
                                break;
                            case "0":
                                endForm = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                break;
                        }

                        if (endForm == true)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "4": //Gender
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Gender: {0}", news.Gender);
                        Console.Write(" Gender: ");
                        string Gender = UserController.GetUserInput();
                        
                        if (Gender == "0")
                        {
                            break;
                        }

                        if(Gender.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Gender is too long. Maximum characters allowed is 50.");
                        }
                        else if (String.IsNullOrEmpty(Gender))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Gender cannot be left empty.");
                        }
                        else
                        {
                            news.Gender = Gender;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "5": //HiringAmount
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Hiring Amount: {0}", news.HiringAmount);
                        Console.Write(" Hiring Amount: ");
                        string HiringAmount = UserController.GetUserInput();
                        
                        if (HiringAmount == "0")
                        {
                            break;
                        }

                        if(HiringAmount.Length > 30)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Hiring Amount is too long. Maximum characters allowed is 30.");
                        }
                        else if (String.IsNullOrEmpty(HiringAmount))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Hiring Amount cannot be left empty.");
                        }
                        else
                        {
                            news.HiringAmount = HiringAmount;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "6": //HiringPosition
                    Console.Clear();
                    while (true) 
                    {
                        bool endPos = false;
                        Console.WriteLine("================================");
                        Console.WriteLine("          Hiring Position");
                        Console.WriteLine("================================");
                        Console.WriteLine(" Old Hiring Positio: {0}", news.HiringPosition);
                        Console.WriteLine(" 1) Staff");
                        Console.WriteLine(" 2) Leader");
                        Console.WriteLine(" 3) Deputy of Department");
                        Console.WriteLine(" 4) Head of Department");
                        Console.WriteLine(" 5) Vice Director");
                        Console.WriteLine(" 6) Director");
                        Console.WriteLine(" 7) CEO");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (UserController.GetUserInput()) 
                        {
                            case "1":
                                news.HiringPosition = "Staff";
                                endPos = true;
                                break;
                            case "2":
                                news.HiringPosition = "Leader";
                                endPos = true;
                                break;
                            case "3":
                                news.HiringPosition = "Deputy of Department";
                                endPos = true;
                                break;
                            case "4":
                                news.HiringPosition = "Head of Department";
                                endPos = true;
                                break;
                            case "5":
                                news.HiringPosition = "Vice Director";
                                endPos = true;
                                break;
                            case "6":
                                news.HiringPosition = "Director";
                                endPos = true;
                                break;
                            case "7":
                                news.HiringPosition = "CEO";
                                endPos = true;
                                break;
                            case "0":
                                endPos = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                break;
                        }

                        if (endPos == true)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "7": //RequiredExp
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Required Experience: {0}", news.HiringAmount);
                        Console.Write(" Required Experience: ");
                        string RequiredExp = UserController.GetUserInput();
                        
                        if (RequiredExp == "0")
                        {
                            break;
                        }

                        if(RequiredExp.Length > 200)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Required Experience is too long. Maximum characters allowed is 200.");
                        }
                        else if (String.IsNullOrEmpty(RequiredExp))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Required Experience cannot be left empty.");
                        }
                        else
                        {
                            news.RequiredExp = RequiredExp;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "11": //IsOpen
                    Console.Clear();              
                    while (true)
                    {
                        bool endStatus = true;
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             STATUS");
                        Console.WriteLine("\n================================");
                        if(news.IsOpen)
                        {
                            Console.WriteLine(" Old Status: Open");
                        }
                        else
                        {
                            Console.WriteLine(" Old Status: Closed");
                        }
                        Console.WriteLine(" 1) Open");
                        Console.WriteLine(" 2) Closed");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter option number: ");
                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                news.IsOpen = true;
                                break;
                            case "2":
                                news.IsOpen = false;
                                break;
                            case "0":
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                endStatus = false;
                                break;
                        }
                        if (endStatus == true)
                        {
                            break;
                        }
                    }
                    Console.Clear();      
                    break;
                case "8": //SalaryRange
                    Console.Clear();
                    while (true)
                    {
                        bool endSalaryRange = false;
                        Console.WriteLine("================================\n");
                        Console.WriteLine("            SALARY");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Old Salary Range: {0}", news.SalaryRange);
                        Console.WriteLine(" 1) Below 3 million");
                        Console.WriteLine(" 2) 3 - 5 million");
                        Console.WriteLine(" 3) 5 - 7 million");
                        Console.WriteLine(" 4) 7 - 10 million");
                        Console.WriteLine(" 5) Higher than 10 million");
                        Console.WriteLine(" 0) Cancel");
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
                            case "0":
                                endSalaryRange = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                break;
                        }

                        if (endSalaryRange == true)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "9": //CityAddress
                    Console.Clear();
                    while (true)
                    {
                        bool endAddress = false;
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             CITY");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Old City Address: {0}", news.CityAddress);
                        Console.WriteLine(" 1) Ha Noi");
                        Console.WriteLine(" 2) Ho Chi Minh");
                        Console.WriteLine(" 3) Binh Duong");
                        Console.WriteLine(" 4) Bac Ninh");
                        Console.WriteLine(" 5) Dong Nai");
                        Console.WriteLine(" 0) Cancel");
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
                            case "0":
                                endAddress = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                break;
                        }

                        if (endAddress == true)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "10": //Profession
                    Console.Clear();
                    while (true) // Profession
                    {
                        bool endProfession = false;
                        Console.WriteLine("================================\n");
                        Console.WriteLine("          PROFESSION");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Old Profession: {0}", news.Profession);
                        Console.WriteLine(" 1) Seller");
                        Console.WriteLine(" 2) Translator");
                        Console.WriteLine(" 3) Journalist");
                        Console.WriteLine(" 4) Post and Telecommunications");
                        Console.WriteLine(" 5) Insurance");
                        Console.WriteLine(" 0) Cancel");
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
                            case "0":
                                endProfession = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                break;
                        }

                        if (endProfession == true)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "R" or "r":
                    DisplaySearchedCVs(recruiterBL.GetCVAppliedInNews(news.NewsID), RecruiterID);
                    break;
                case "0":
                    Console.Clear();
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
        
    }

    public void SearchCVs(int? RecruiterID)
    {
        Console.Clear();
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("          SEARCH CV");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) CareerTitle");
            Console.WriteLine(" 2) Address");
            Console.WriteLine(" 3) Job Tilte / Position (Work Experience)");
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
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    break;
            }
            if (end == true)
            {
                Console.Clear();
                break;
            }
        }
    }   

    public List<CV> SearchCVViaJobPosition(int? RecruiterID) 
    {
        Console.Clear();
        Console.WriteLine("================================\n");
        Console.WriteLine("    SEARCH JOB TITLE/POSITION");
        Console.WriteLine("\n================================");
        Console.Write(" Search for: ");
        string? keyword = UserController.GetUserInput();
        return recruiterBL.GetCVByJobPosition(keyword);
    }

    public List<CV> SearchCVViaCareerTitle(int? RecruiterID){
        Console.Clear();
        Console.WriteLine("================================\n");
        Console.WriteLine("      SEARCH CAREER TITLE");
        Console.WriteLine("\n================================");
        Console.Write(" Search for: ");
        string? keyword = UserController.GetUserInput();
        return recruiterBL.GetCVByCareerTitle(keyword);
    }
    
    public List<CV> SearchCVViaAddress(int? RecruiterID){ 
        Console.Clear();
        Console.WriteLine("================================\n");
        Console.WriteLine("         SEARCH ADDRESS");
        Console.WriteLine("\n================================");
        Console.Write(" Search for: ");
        string? keyword = UserController.GetUserInput();
        return recruiterBL.GetCVByAddress(keyword);
    }

    public void DisplaySearchedCVs(List<CV> cv, int? RecruiterID)
    {  
        if (cv != null)
        {
            Console.Clear();
            while (true)
            {
                Menu.PrintSubMenu(6, cv);
                string choice = UserController.GetUserInput();

                if(choice == "0")
                {
                    Console.Clear();
                    break;
                }

                bool success = int.TryParse(choice, out int position);
                if(success)
                {
                    if((position - 1) < cv.Count() && position > 0)
                    {
                        Menu.PrintSubMenu(7, cv[position - 1]);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" Your chosen target doesn't exist.");
                        Console.WriteLine("================================");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    Console.WriteLine("================================");
                }
            }
        }
    }

    public void UpdateProfileInformation(Recruiter recruiter)
    { 
        Console.Clear();
        bool end = false;
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("             UPDATE");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) CompanyName");
            Console.WriteLine(" 2) Position");
            Console.WriteLine(" 3) PhoneNum");
            Console.WriteLine(" 4) CompanyDescription");
            Console.WriteLine(" 5) Business Size");
            Console.WriteLine(" 6) Business Field");
            Console.WriteLine(" 7) Company Address");
            Console.WriteLine(" 0) Return");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (UserController.GetUserInput())
            {
                case "1": // CompanyName
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Company Name: {0}", recruiter.CompanyName);
                        Console.Write(" Company Name: ");
                        string CompanyName = UserController.GetUserInput();
                        
                        if (CompanyName == "0")
                        {
                            break;
                        }

                        if(CompanyName.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Company Name is too long. Maximum characters allowed is 100.");
                        }
                        else if (string.IsNullOrEmpty(CompanyName))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Company Name can't be left empty.");
                        }
                        else
                        {
                            recruiter.CompanyName = CompanyName;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "2": //Postion
                    Console.Clear();
                    while (true) 
                        {
                            bool endUpdate = true;
                            Console.WriteLine("================================\n");
                            Console.WriteLine("          UPDATE POSITION");
                            Console.WriteLine("\n================================");
                            Console.WriteLine(" Old Position: {0}", recruiter.Position);
                            Console.WriteLine(" 1) Staff");
                            Console.WriteLine(" 2) Leader");
                            Console.WriteLine(" 3) Deputy of Department");
                            Console.WriteLine(" 4) Head of Department");
                            Console.WriteLine(" 5) Vice Director");
                            Console.WriteLine(" 6) Director");
                            Console.WriteLine(" 7) CEO");
                            Console.WriteLine(" 0) Cancel");
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
                                case "0":
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                                    endUpdate = false;
                                    break;
                            }
                            if (endUpdate == true)
                            {
                                break;
                            }
                        }
                        Console.Clear();
                    break;
                case "3": // PhoneNum
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Phone Number: {0}", recruiter.PhoneNum);
                        Console.Write(" Phone Number: ");
                        string PhoneNum = UserController.GetUserInput();
                        
                        if (PhoneNum == "0")
                        {
                            break;
                        }

                        if (string.IsNullOrEmpty(PhoneNum))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Phone Number can't be left empty.");
                        }
                        else if(PhoneNum.Length != 10)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Phone Number must be 10 numerics.");
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
                                Console.Clear();
                                Console.WriteLine("================================");
                                Console.WriteLine(" Phone number must be numbers only.");
                            }
                        }
                    }
                    Console.Clear();
                    break;
                case "4": // CompanyDescription
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Company Description: {0}", recruiter.CompanyDescription);
                        Console.Write(" Company Description: ");
                        string CompanyDescription = UserController.GetUserInput();
                        
                        if (CompanyDescription == "0")
                        {
                            break;
                        }

                        if(CompanyDescription.Length > 5000)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Company Description is too long. Maximum characters allowed is 5000.");
                        }
                        else
                        {
                            recruiter.CompanyDescription = CompanyDescription;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "5": // BusinessSize
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Business Size: {0}", recruiter.BusinessSize);
                        Console.Write(" Business Size: ");
                        string BusinessSize = UserController.GetUserInput();
                        
                        if (BusinessSize == "0")
                        {
                            break;
                        }

                        if(BusinessSize.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Business Size is too long. Maximum characters allowed is 50.");
                        }
                        else if (string.IsNullOrEmpty(BusinessSize))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Business Size can't be left empty.");
                        }
                        else
                        {
                            recruiter.BusinessSize = BusinessSize;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "6": // BusinessField
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Business Field: {0}", recruiter.BusinessField);
                        Console.Write(" Business Field: ");
                        string BusinessField = UserController.GetUserInput();
                        
                        if (BusinessField == "0")
                        {
                            break;
                        }

                        if(BusinessField.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Business Field is too long. Maximum characters allowed is 100.");
                        }
                        else if (string.IsNullOrEmpty(BusinessField))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Business Field can't be left empty.");
                        }
                        else
                        {
                            recruiter.BusinessField = BusinessField;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "7": // CompanyAddress
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("             UPDATE");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" Enter 0 to cancel.");
                        Console.WriteLine(" Old Company Address: {0}", recruiter.CompanyAddress);
                        Console.Write(" Company Address: ");
                        string CompanyAddress = UserController.GetUserInput();
                        
                        if (CompanyAddress == "0")
                        {
                            break;
                        }

                        if(CompanyAddress.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Company Address is too long. Maximum characters allowed is 100.");
                        }
                        else if (string.IsNullOrEmpty(CompanyAddress))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Company Address can't be left empty.");
                        }
                        else
                        {
                            recruiter.CompanyAddress = CompanyAddress;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "0":
                    Console.Clear();
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
    }

    public void ViewProfileInformation(Recruiter recruiter)
    {
        Console.Clear();
        bool end = false;
        while(true)
        {
            Menu.PrintSubMenu(5, recruiter);
            Console.Write(" Enter 1 to Update your profile or 0 to return: ");
            switch (UserController.GetUserInput())
            {
                case "1":
                    UpdateProfileInformation(recruiter);
                    break;
                case "0":
                    Console.Clear();
                    while(true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("        CONFIRM DETAILS");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" If there are changes, you can choose Confirm to save them.");
                        Console.WriteLine(" 1) Confirm");
                        Console.WriteLine(" 0) Cancel");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                Console.Clear();
                                recruiterBL.UpdatePersonalRecruitInfo(recruiter);
                                end = true;
                                break;
                            case "0":
                                Console.Clear();
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
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("============================================="); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    Console.WriteLine("============================================="); 
                    break;
            }

            if (end == true)
            {
                break;
            }
        }
    }
}
