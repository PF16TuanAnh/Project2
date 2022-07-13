using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class RecruiterDAL
{
    private string? query;
    private MySqlDataReader? reader;

    // public Candidate GetCandidateByUserID(int UserID)
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

    private Recruiter GetRecruiterInfo(MySqlDataReader reader)
    {
        Recruiter recruiter = new Recruiter();
        recruiter.RecruiterID = reader.GetInt32("RecruiterID");
        recruiter.PhoneNum = reader.GetString("PhoneNum");
        recruiter.Position = reader.GetString("Position");
        recruiter.CompanyName = reader.GetString("CompanyName");
        recruiter.CompanyDescription = reader.GetString("CompanyDescription");
        recruiter.BussinessSize = reader.GetString("BussinessSize");
        recruiter.BussinessField = reader.GetString("BussinessField");
        recruiter.Username = reader.GetString("Username");

        return recruiter;
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

    private RecruitNews GetRecruitNewsInfo(MySqlDataReader reader)
    {
        RecruitNews recruitNews = new RecruitNews();
        recruitNews.NewsID = reader.GetInt32("NewsID");
        recruitNews.NewsName = reader.GetString("NewsName");
        recruitNews.DeadLine = reader.GetString("Deadline");
        recruitNews.SalaryRange = reader.GetString("SalaryRange");
        recruitNews.FormOfEmploy = reader.GetString("FormOfEmploy");
        recruitNews.Gender = reader.GetString("Gender");
        recruitNews.HiringAmount = reader.GetString("HiringAmount");
        recruitNews.HiringPosition = reader.GetString("HiringPosition");
        recruitNews.RequiredExp = reader.GetString("RequiredExp");
        recruitNews.CityAddress = reader.GetString("CityAddress");
        recruitNews.Profession = reader.GetString("Profession");
        recruitNews.RecruiterID = reader.GetInt32("RecruiterID");
        return recruitNews;
    }
}