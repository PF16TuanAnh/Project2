using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class CandidateDAL
{
    private string? query;
    private MySqlDataReader? reader;

    public Candidate GetCandidateByID(int? CandidateID)
    {
        query = @"select * from Candidates c inner join Users u on c.UserID = u.UserID where CandidateID = " + CandidateID;

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        Candidate candidate = null!;
        if (reader.Read())
        {
            candidate = GetCandidateInfo(reader);
        }

        DBHelper.CloseConnection();

        return candidate;
    }

    private Candidate GetCandidateInfo(MySqlDataReader reader)
    {
        Candidate candidate = new Candidate();
        candidate.CandidateID = reader.GetInt32("CandidateID");
        candidate.Username = reader.GetString("Username");
        // candidate.CandidateCV = GetCVByID(candidate.CandidateID)

        return candidate;
    }

    // public CV GetCVByID(int CandidateID)
    // {
    //     query = @"select emp_no, first_name, last_name from employees where emp_no = " + empId;

        
    //     DBHelper.OpenConnection();
        
    //     reader = DBHelper.ExecQuery(query);

    //     Employee employee = null!;
    //     if (reader.Read())
    //     {
    //         employee = GetEmployeeInfo(reader);
    //     }

    //     DBHelper.CloseConnection();

    //     return employee;
    // }

    private CV GetCVInfo(MySqlDataReader reader)
    {
        CV cv = new CV();
        cv.CVID = reader.GetInt32("CVID");
        cv.FullName = reader.GetString("FullName");
        cv.CareerTitle = reader.GetString("CareerTitle");
        cv.CareerObjective = reader.GetString("CareerObjective");
        cv.BirthDate = reader.GetString("BirthDate");
        cv.PhoneNum = reader.GetString("PhoneNum");
        cv.Email = reader.GetString("Email");
        cv.SocialMedia = reader.GetString("SocialMedia");
        cv.PersonalAddress = reader.GetString("PersonalAddress");
        //cv.CVDetails = 

        return cv;
    }

    // public List<CVDetails> GetCVDetailsByID(int CVID)
    // {
    //     query = @"select emp_no, first_name, last_name from employees where emp_no = " + empId;

        
    //     DBHelper.OpenConnection();
        
    //     reader = DBHelper.ExecQuery(query);

    //     List<CVDetails> CVDetails = null!;
    //     while (reader.Read())
    //     {
    //         if(CVDetails == null)
    //         {
    //             CVDetails = new List<CVDetails>();
    //         }
    //         CVDetails.Add(GetCVDetailsInfo(reader));
    //     }

    //     DBHelper.CloseConnection();

    //     return CVDetails!;
    // }

    private CVDetails GetCVDetailsInfo(MySqlDataReader reader)
    {
        CVDetails cVDetails = new CVDetails();
        cVDetails.DetailsID = reader.GetInt32("DetailID");
        cVDetails.Title = reader.GetString("Title");
        cVDetails.JobPosition = reader.GetString("JobPosition");
        cVDetails.FromDate = reader.GetString("FromDate");
        cVDetails.ToDate = reader.GetString("ToDate");
        cVDetails.Association = reader.GetString("Association");
        cVDetails.Description = reader.GetString("Descriptions");

        return cVDetails;
    }
}