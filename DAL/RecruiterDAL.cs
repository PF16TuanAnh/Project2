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

    public List<RecruitNews> GetNewsBySalaryRange(string SalaryRange)
    {
        query = @"select * from RecruitNews where SalaryRange = '" + SalaryRange + "'" + " and IsOpen = true";

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        List<RecruitNews> recruitNews = null!;
        while (reader.Read())
        {
            if(recruitNews == null)
            {
                recruitNews = new List<RecruitNews>();
            }
            recruitNews.Add(GetRecruitNewsInfo(reader));
        }

        DBHelper.CloseConnection();

        return recruitNews;
    }

    public List<RecruitNews> GetNewsByCityAddress(string CityAddress)
    {
        query = @"select * from RecruitNews where CityAddress = '" + CityAddress + "'" + " and IsOpen = true";

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        List<RecruitNews> recruitNews = null!;
        while (reader.Read())
        {
            if(recruitNews == null)
            {
                recruitNews = new List<RecruitNews>();
            }
            recruitNews.Add(GetRecruitNewsInfo(reader));
        }

        DBHelper.CloseConnection();

        return recruitNews;
    }

    public List<RecruitNews> GetNewsByProfession(string Profession)
    {
        query = @"select * from RecruitNews where Profession = '" + Profession + "'" + " and IsOpen = true";

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        List<RecruitNews> recruitNews = null!;
        while (reader.Read())
        {
            if(recruitNews == null)
            {
                recruitNews = new List<RecruitNews>();
            }
            recruitNews.Add(GetRecruitNewsInfo(reader));
        }

        DBHelper.CloseConnection();

        return recruitNews;
    }

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