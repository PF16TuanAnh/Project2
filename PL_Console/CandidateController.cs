namespace Pl_Console;
using BL;
using Persistence;
using System.Text.RegularExpressions;
using Spectre.Console;

public class CandidateController
{
    private readonly Regex NumericRegex = new Regex(@"^[0-9]*$");
    private CandidateBL candidateBL;

    public CandidateController()
    {
        candidateBL = new CandidateBL();
    }

    public Candidate GetCandidate(int? CandidateID)
    {
        return candidateBL.GetCandidateByID(CandidateID);
    }

    public void CreateCV(int? CandidateID)
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

        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Full Name       : ");
            FullName = UserController.GetUserInput();
            if(FullName.Length > 100)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Full Name is too long. Maximum characters allowed is 100.");
            }
            else if (String.IsNullOrEmpty(FullName))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Full Name cannot be left empty.");
            }
            else
            {
                break;
            }
        }

        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Career Title    : ");
            CareerTitle = UserController.GetUserInput();
            if(CareerTitle.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Career Title is too long. Maximum characters allowed is 50.");
            }
            else
            {
                break;
            }
        }
        
        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Career Objective: ");
            CareerObjective = UserController.GetUserInput();
            if(CareerObjective.Length > 5000)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Career Objective is too long. Maximum characters allowed is 5000.");
            }
            else if (String.IsNullOrEmpty(CareerObjective))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Career Objective cannot be left empty.");
            }
            else
            {
                break;
            }
        }
        
        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Date of Birth   : ");
            BirthDate = UserController.GetUserInput();
            if(BirthDate.Length > 20)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Date of Birth is too long. Maximum characters allowed is 20.");
            }
            else
            {
                break;
            }
        }
        
        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Phone Number    : ");
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
                Console.WriteLine(" Phone Number must be 10 characters long.");
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
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Email           : ");
            Email = UserController.GetUserInput();
            if(Email.Length > 100)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Email is too long. Maximum characters allowed is 100.");
            }
            else if (String.IsNullOrEmpty(Email))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Email can't be left empty.");
            }
            else
            {
                if (UserController.IsValidEmail(Email))
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Invalid email format! Please re-enter your email.");
                }
            }
        }

        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Social Media    : ");
            SocialMedia = UserController.GetUserInput();
            if(SocialMedia.Length > 2000)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Social Media is too long. Maximum characters allowed is 2000.");
            }
            else
            {
                break;
            }
        }
        
        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.Write(" Address         : ");
            PersonalAddress = UserController.GetUserInput();
            if(PersonalAddress.Length > 5000)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Address is too long. Maximum characters allowed is 5000.");
            }
            else if (String.IsNullOrEmpty(PersonalAddress))
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine(" Address can't be left empty.");
            }
            else
            {
                break;
            }
        }

        Console.Clear();
        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           CREATE CV");
            Console.WriteLine("\n================================");
            Console.WriteLine(" 1) Add a Skill");
            Console.WriteLine(" 2) Add a Work Experience");
            Console.WriteLine(" 3) Add an Education");
            Console.WriteLine(" 4) Add an Activity");
            Console.WriteLine(" 5) Add a Certification");
            Console.WriteLine(" 0) Done");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (UserController.GetUserInput())
            {
                case "1":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(1));
                    Console.Clear();
                    break;
                case "2":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(2));
                    Console.Clear();
                    break;
                case "3":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(3));
                    Console.Clear();
                    break;
                case "4":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(4));
                    Console.Clear();
                    break;
                case "5":
                    if (CVDetails == null)
                    {
                        CVDetails = new List<CVDetails>();
                    }
                    CVDetails.Add(AddCVDetails(5));
                    Console.Clear();
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

        CV newCV = new CV(FullName, CareerTitle, CareerObjective, BirthDate, PhoneNum, Email, SocialMedia, PersonalAddress, CVDetails);
        candidateBL.CreateNewCV(newCV, CandidateID);
    }

    public CVDetails AddCVDetails(int type)
    {
        Console.Clear();
        string Title = "Skill";
        string? JobPosition;
        string? FromDate = null;
        string? ToDate = null;
        string? Association = null;
        string? Description = null;
        
        switch (type)
        {
            case 1:
                Title = "Skill";
                break;
            case 2:
                Title = "Work Experience";
                break;
            case 3:
                Title = "Education";
                break;
            case 4:
                Title = "Activity";
                break;
            case 5:
                Title = "Certificate";
                break;
            default:
                break;
        }

        if(Title == "Skill" || Title == "Certificate")
        {
            if (Title == "Skill")
            {
                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("              ADD");
                    Console.WriteLine("\n================================");
                    Console.Write(" Skill Group Name: ");
                    JobPosition = UserController.GetUserInput();
                    if(JobPosition.Length > 100)
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" The name is too long. Maximum characters allowed is 100.");
                    }
                    else if (String.IsNullOrEmpty(JobPosition))
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" You cannot left the name empty.");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Clear();
                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("              ADD");
                    Console.WriteLine("\n================================");
                    Console.Write(" Skill Description: ");
                    Description = UserController.GetUserInput();
                    if(Description.Length > 5000)
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" Description is too long. Maximum characters allowed is 5000.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("              ADD");
                    Console.WriteLine("\n================================");
                    Console.Write(" Time: ");
                    FromDate = UserController.GetUserInput();
                    if(FromDate.Length > 50)
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" Time is too long. Maximum characters allowed is 50.");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Clear();
                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("              ADD");
                    Console.WriteLine("\n================================");
                    Console.Write(" Certification Name: ");
                    JobPosition = UserController.GetUserInput();
                    if(JobPosition.Length > 100)
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" The name is too long. Maximum characters allowed is 100.");
                    }
                    else if (String.IsNullOrEmpty(JobPosition))
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" You cannot left the name empty.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            while (true)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("              ADD");
                Console.WriteLine("\n================================");
                if(Title == "Work Experience")
                {
                    Console.Write(" Title / Position: ");
                }
                else if (Title == "Education")
                {
                    Console.Write(" Degree / Study Field: ");
                }
                else
                {
                    Console.Write(" Title / Role: ");
                }
                
                JobPosition = UserController.GetUserInput();
                if(JobPosition.Length > 100)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" The name is too long. Maximum characters allowed is 100.");
                }
                else if (String.IsNullOrEmpty(JobPosition))
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" You cannot left the name empty.");
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            while (true)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("              ADD");
                Console.WriteLine("\n================================");
                Console.Write(" From: ");
                FromDate = UserController.GetUserInput();
                if(FromDate.Length > 50)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" From Date is too long. Maximum characters allowed is 50.");
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            while (true)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("              ADD");
                Console.WriteLine("\n================================");
                Console.Write(" To: ");
                ToDate = UserController.GetUserInput();
                if(ToDate.Length > 50)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" To Date is too long. Maximum characters allowed is 50.");
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            while (true)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("              ADD");
                Console.WriteLine("\n================================");
                if(Title == "Work Experience")
                {
                    Console.Write(" Company Name: ");
                }
                else if (Title == "Education")
                {
                    Console.Write(" School Name: ");
                }
                else
                {
                    Console.Write(" Organization Name: ");
                }
                Association = UserController.GetUserInput();
                if(Association.Length > 100)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" The name is too long. Maximum characters allowed is 100.");
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            while (true)
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("              ADD");
                Console.WriteLine("\n================================");
                Console.Write(" Description: ");
                Description = UserController.GetUserInput();
                if(Description.Length > 5000)
                {
                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine(" Description is too long. Maximum characters allowed is 5000.");
                }
                else
                {
                    break;
                }
            }
        }
        
        return new CVDetails(Title, JobPosition, FromDate, ToDate, Association, Description);
    }

    public void ViewCV(CV cv)
    {
        bool end = false;

        while(true)
        {
            Menu.PrintSubMenu(1, cv);
            Console.Write(" Enter 1 to edit your CV or 0 to return: ");
            switch (UserController.GetUserInput())
            {
                case "1":
                    UpdateCV(cv);
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
                                candidateBL.UpdateCVInfo(cv);
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

    public void UpdateCV(CV cv)
    {
        Console.Clear();
        bool end = false;

        while (true)
        {
            Console.WriteLine("================================\n");
            Console.WriteLine("           UPDATE CV");
            Console.WriteLine("\n================================");
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Full Name       : ");
                        string FullName = UserController.GetUserInput();
                        if(FullName.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Full Name is too long. Maximum characters allowed is 100.");
                        }
                        else if (String.IsNullOrEmpty(FullName))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Full Name cannot be left empty.");
                        }
                        else
                        {
                            cv.FullName = FullName;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Career Title       : ");
                        string CareerTitle = UserController.GetUserInput();
                        if(CareerTitle.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(". Career Title is too long. Maximum characters allowed is 50.");
                        }
                        else
                        {
                            cv.CareerTitle = CareerTitle;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Career Objective: ");
                        string CareerObjective = UserController.GetUserInput();
                        if(CareerObjective.Length > 5000)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Career Objective is too long. Maximum characters allowed is 5000.");
                        }
                        else if (String.IsNullOrEmpty(CareerObjective))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Career Objective be left empty.");
                        }
                        else
                        {
                            cv.CareerObjective = CareerObjective;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "4":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Date of Birth   : ");
                        string BirthDate = UserController.GetUserInput();
                        if(BirthDate.Length > 20)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Date of Birth is too long. Maximum characters allowed is 20.");
                        }
                        else
                        {
                            cv.BirthDate = BirthDate;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "5":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Phone Number    : ");
                        string? _PhoneNum = UserController.GetUserInput();

                        if (string.IsNullOrEmpty(_PhoneNum))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Phone Number can't be left empty.");
                        }
                        else if(_PhoneNum.Length != 10)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Phone Number is too long. Maximum characters allowed is 10.");
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
                                Console.Clear();
                                Console.WriteLine("================================");
                                Console.WriteLine(" Phone number must be numbers only.");
                            }
                        }
                    }
                    Console.Clear();
                    break;
                case "6":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Email           : ");
                        string Email = UserController.GetUserInput();
                        if(Email.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Email is too long. Maximum characters allowed is 100");
                        }
                        else if (String.IsNullOrEmpty(Email))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Email cannot be left empty.");
                        }
                        else
                        {
                            if (UserController.IsValidEmail(Email))
                            {
                                cv.Email = Email;
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("================================");
                                Console.WriteLine(" Invalid email format! Please re-enter your email.");
                            }
                        }
                    }
                    Console.Clear();
                    break;
                case "7":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Social Media    : ");
                        string SocialMedia = UserController.GetUserInput();
                        if(SocialMedia.Length > 2000)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Social Media is too long. Maximum characters allowed is 2000.");
                        }
                        else
                        {
                            cv.SocialMedia = SocialMedia;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "8":
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("           UPDATE CV");
                        Console.WriteLine("\n================================");
                        Console.Write(" Address         : ");
                        string PersonalAddress = UserController.GetUserInput();
                        if(PersonalAddress.Length > 5000)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Address is too long. Maximum characters allowed is 5000.");
                        }
                        else if (String.IsNullOrEmpty(PersonalAddress))
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Address cannot be left empty.");
                        }
                        else
                        {
                            cv.PersonalAddress = PersonalAddress;
                            break;
                        }
                    }
                    Console.Clear();
                    break;
                case "9":
                    Console.Clear();
                    cv.CVDetails = UpdateCVDetails(cv.CVDetails);
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
                Console.Clear();
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
            Console.WriteLine(" 1) View ");
            Console.WriteLine(" 2) Add");
            Console.WriteLine(" 3) Change");
            Console.WriteLine(" 4) Delete");
            Console.WriteLine(" 0) Done");
            Console.WriteLine("================================");
            Console.Write(" Enter the option number: ");
            switch (UserController.GetUserInput())
            {
                case "1":
                    Menu.PrintSubMenu(2, CVDetails);
                    break;
                case "2": // Add more CVDetails
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("              ADD");
                        Console.WriteLine("\n================================");
                        Console.WriteLine(" 1) A Skill");
                        Console.WriteLine(" 2) A Work Experience");
                        Console.WriteLine(" 3) An Education");
                        Console.WriteLine(" 4) An Activity");
                        Console.WriteLine(" 5) A Certification");
                        Console.WriteLine(" 0) Done");
                        Console.WriteLine("================================");
                        Console.Write(" Enter the option number: ");
                        switch (UserController.GetUserInput())
                        {
                            case "1":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(1));
                                Console.Clear();
                                break;
                            case "2":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(2));
                                Console.Clear();
                                break;
                            case "3":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(3));
                                Console.Clear();
                                break;
                            case "4":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(4));
                                Console.Clear();
                                break;
                            case "5":
                                if (CVDetails == null)
                                {
                                    CVDetails = new List<CVDetails>();
                                }
                                CVDetails.Add(AddCVDetails(5));
                                Console.Clear();
                                break;
                            case "0":
                                done = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("================================"); 
                                Console.WriteLine(" Invalid input! Please re-enter your option.");
                                break;
                        }

                        if (done == true)
                        {
                            Console.Clear();
                            break;
                        }
                    }
                    break;
                case "3": // Change info of different CVDetails
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
                            Console.WriteLine(" 5) A Certification");
                            Console.WriteLine(" 0) Done");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            switch (UserController.GetUserInput())
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
                                    done = true;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                                    break;
                            }

                            if (done == true)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" There's nothing to change!");
                    }
                    break;
                case "4": // 'Delete' CVDetails
                    if(CVDetails != null)
                    {
                        Console.Clear();
                        while (true)
                        {
                            Console.WriteLine("================================\n");
                            Console.WriteLine("             DELETE");
                            Console.WriteLine("\n================================");
                            Console.WriteLine(" 1) A Skill");
                            Console.WriteLine(" 2) A Work Experience");
                            Console.WriteLine(" 3) An Education");
                            Console.WriteLine(" 4) An Activity");
                            Console.WriteLine(" 5) A Certification");
                            Console.WriteLine(" 0) Done");
                            Console.WriteLine("================================");
                            Console.Write(" Enter the option number: ");
                            string choice = UserController.GetUserInput();   
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
                                    done = true;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("================================"); 
                                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                                    break;
                            }

                            if (done == true)
                            {
                                Console.Clear();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" There's nothing to delete!");
                    }
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

        return CVDetails!;
    }

    public List<CVDetails> ChangeCVDetails(List<CVDetails> CVDetails, string type)
    {
        Console.Clear();
        int count = 0;
        Console.WriteLine("================================\n");
        Console.WriteLine("            CHANGE");
        Console.WriteLine("\n================================");
        Console.Write(" Enter the number position of the {0}: ", type);
        string choice = UserController.GetUserInput();
        
        foreach (CVDetails detail in CVDetails)
        {
            if(detail.Title == type)
            {
                count++;
            }
            
            if(int.TryParse(choice, out int pos) && count == pos && detail.Title == type)
            {
                Console.Clear();
                while (true)
                {
                    Console.WriteLine("================================\n");
                    Console.WriteLine("            CHANGE");
                    Console.WriteLine("\n================================");
                    if(type == "Work Experience")
                    {
                        Console.Write(" Title / Position: ");
                    }
                    else if (type == "Education")
                    {
                        Console.Write(" Degree / Study Field: ");
                    }
                    else if (type == "Activity")
                    {
                        Console.Write(" Title / Role: ");
                    }
                    else if (type == "Skill")
                    {
                        Console.Write(" Skill Group Name: ");
                    }
                    else
                    {
                        Console.Write(" Certification Name: ");
                    }
                    detail.JobPosition = UserController.GetUserInput();
                    if(detail.JobPosition.Length > 100)
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" The name is too long. Maximum characters allowed is 100.");
                    }
                    else if (String.IsNullOrEmpty(detail.JobPosition))
                    {
                        Console.Clear();
                        Console.WriteLine("================================");
                        Console.WriteLine(" You cannot left the name empty.");
                    }
                    else
                    {
                        break;
                    }
                }

                if(type != "Skill")
                {
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("            CHANGE");
                        Console.WriteLine("\n================================");
                        if (type == "Certificate")
                        {
                            Console.Write(" Time: ");
                        }
                        else
                        {
                            Console.Write(" From: ");
                        }
                        detail.FromDate = UserController.GetUserInput();
                        if(detail.FromDate.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" From Date is too long. Maximum characters allowed is 50.");
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (type != "Skill" && type != "Certificate")
                {
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("            CHANGE");
                        Console.WriteLine("\n================================");
                        Console.Write(" To: ");
                        detail.ToDate = UserController.GetUserInput();
                        if(detail.ToDate.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" To Date is too long. Maximum characters allowed is 50.");
                        }
                        else
                        {
                            break;
                        }
                    }

                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("            CHANGE");
                        Console.WriteLine("\n================================");
                        if(detail.Title == "Work Experience")
                        {
                            Console.Write(" Company Name: ");
                        }
                        else if (detail.Title == "Education")
                        {
                            Console.Write(" School Name: ");
                        }
                        else
                        {
                            Console.Write(" Organization Name: ");
                        }
                        detail.Association = UserController.GetUserInput();
                        if(detail.Association.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" The name is too long. Maximum characters allowed is 100.");
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (type != "Certificate")
                {
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("================================\n");
                        Console.WriteLine("            CHANGE");
                        Console.WriteLine("\n================================");
                        Console.Write(" Description: ");
                        detail.Description = UserController.GetUserInput();
                        if(detail.Description.Length > 5000)
                        {
                            Console.Clear();
                            Console.WriteLine("================================");
                            Console.WriteLine(" Description is too long. Maximum characters allowed is 5000.");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return CVDetails;
    }

    public List<CVDetails> DeleteCVDetails(List<CVDetails> CVDetails, string type)
    {
        Console.Clear();
        bool isDeleted = false; 
        int count = 0;
        Console.WriteLine("================================\n");
        Console.WriteLine("             DELETE");
        Console.WriteLine("\n================================");
        Console.Write(" Enter the number position of the {0}: ", type);
        string choice = UserController.GetUserInput();
        
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

                isDeleted = true;
            }
        }

        if (isDeleted == false)
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine(" Couldn't find the target to delete.");
        }
        else
        {
            Console.Clear();
        }
        
        return CVDetails;
    }

    public void SearchRecruitNews(int? CandidateID)
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    Console.Clear();
                    DisplaySearchedNews(SearchRecruitNewsViaProfession(), CandidateID);
                    break;
                case "2":
                    Console.Clear();
                    DisplaySearchedNews(SearchRecruitNewsViaSalaryRange(), CandidateID);
                    break;
                case "3":
                    Console.Clear();
                    DisplaySearchedNews(SearchRecruitNewsViaCity(), CandidateID);
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    return candidateBL.GetNewsByProfession("Seller");
                case "2":
                    return candidateBL.GetNewsByProfession("Translator");
                case "3":
                    return candidateBL.GetNewsByProfession("Journalist");
                case "4":
                    return candidateBL.GetNewsByProfession("Post and Telecommunications");
                case "5":
                    return candidateBL.GetNewsByProfession("Insurance");
                default:
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    return candidateBL.GetNewsBySalaryRange("Below 3 million");
                case "2":
                    return candidateBL.GetNewsBySalaryRange("3 - 5 million");
                case "3":
                    return candidateBL.GetNewsBySalaryRange("5 - 7 million");
                case "4":
                    return candidateBL.GetNewsBySalaryRange("7 - 10 million");
                case "5":
                    return candidateBL.GetNewsBySalaryRange("Higher than 10 million");
                default:
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
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
            switch (UserController.GetUserInput())
            {
                case "1":
                    return candidateBL.GetNewsByCityAddress("Ha Noi");
                case "2":
                    return candidateBL.GetNewsByCityAddress("Ho Chi Minh");
                case "3":
                    return candidateBL.GetNewsByCityAddress("Binh Duong");
                case "4":
                    return candidateBL.GetNewsByCityAddress("Bac Ninh");
                case "5":
                    return candidateBL.GetNewsByCityAddress("Dong Nai");
                default:
                    Console.Clear();
                    Console.WriteLine("================================"); 
                    Console.WriteLine(" Invalid input! Please re-enter your option.");
                    break;
            }
        }
    }

    public void DisplaySearchedNews(List<RecruitNews> recruitNews, int? CandidateID)
    {
        if (recruitNews != null)
        {
            Console.Clear();
            while (true)
            {
                Menu.PrintSubMenu(3, recruitNews);
                string choice = UserController.GetUserInput();

                if(choice == "0")
                {
                    Console.Clear();
                    break;
                }

                bool success = int.TryParse(choice, out int position);
                if(success)
                {
                    Console.Clear();
                    SearchedNewsDetails(recruitNews[position - 1], CandidateID);
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

    public void SearchedNewsDetails(RecruitNews news, int? CandidateID)
    {
        bool end = false;
        
        Recruiter recruiter = candidateBL.GetRecruiterByNewsID(news.NewsID);
        if (recruiter != null)
        {
            // Create table to hold all details of the news
            var newsTable = new Table();
            newsTable.Title("Recruitment News");
            newsTable.HideHeaders();
            newsTable.AddColumn("Title");
            newsTable.AddColumn(new TableColumn("Content"));
            newsTable.Columns[0].Width(20);
            newsTable.LeftAligned();
            newsTable.Columns[1].Width(40);
            newsTable.AddRow("Name", news.NewsName);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Recruiter", recruiter.Name);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Recruiter's Number", recruiter.PhoneNum);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Deadline", news.DeadLine);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Salary Range", news.SalaryRange);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Form of Employment", news.FormOfEmploy ?? "");
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Gender", news.Gender ?? "");
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Hiring Amount", news.HiringAmount);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Hiring Position", news.HiringPosition);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Required Experience", news.RequiredExp ?? "");
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("City Address", news.CityAddress);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Company", recruiter.CompanyName);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Company Address", recruiter.CompanyAddress);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Business Field", recruiter.BusinessField);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Business Size", recruiter.BusinessSize);
            newsTable.AddRow("--------------------", "----------------------------------------");
            newsTable.AddRow("Company Description", recruiter.CompanyDescription ?? "");

            while (true)
            {
                AnsiConsole.Write(newsTable);

                Console.WriteLine("================================");
                bool IsApplied = candidateBL.IsApplied(CandidateID, news.NewsID);
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
                switch (UserController.GetUserInput())
                {
                    case "1":
                        if(!IsApplied)
                        {
                            Console.Clear();
                            candidateBL.ApplyToNews(CandidateID, news.NewsID);
                        }
                        break;
                    case "0":
                        Console.Clear();
                        end = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Invalid input! Please re-enter your option.");
                        Console.WriteLine("================================"); 
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
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve all details of the recruitment news.");
        }
    }
}
