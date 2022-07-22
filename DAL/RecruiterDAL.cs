using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class RecruiterDAL
{
    private string? query;
    private MySqlDataReader? reader;

    public Recruiter GetRecruiterByNewsID(int NewsID)
    {
        query = @"select r.*, u.Username from Recruiters r inner join RecruitNews rn on r.RecruiterID = rn.RecruiterID inner join Users u on r.UserID = u.UserID where NewsID = " + NewsID;

        
        DBHelper.OpenConnection();
        
        reader = DBHelper.ExecQuery(query);

        Recruiter recruiter = null!;
        if (reader.Read())
        {
            recruiter = GetRecruiterInfo(reader);
        }

        DBHelper.CloseConnection();

        return recruiter;
    }

    private Recruiter GetRecruiterInfo(MySqlDataReader reader)
    {
        Recruiter recruiter = new Recruiter();
        if (!reader.IsDBNull(reader.GetOrdinal("RecruiterID"))) recruiter.RecruiterID = reader.GetInt32("RecruiterID");
        if (!reader.IsDBNull(reader.GetOrdinal("PhoneNum"))) recruiter.PhoneNum = reader.GetString("PhoneNum");
        if (!reader.IsDBNull(reader.GetOrdinal("Position"))) recruiter.Position = reader.GetString("Position");
        if (!reader.IsDBNull(reader.GetOrdinal("CompanyName"))) recruiter.CompanyName = reader.GetString("CompanyName");
        if (!reader.IsDBNull(reader.GetOrdinal("CompanyDescription"))) recruiter.CompanyDescription = reader.GetString("CompanyDescription");
        if (!reader.IsDBNull(reader.GetOrdinal("BussinessSize"))) recruiter.BussinessSize = reader.GetString("BussinessSize");
        if (!reader.IsDBNull(reader.GetOrdinal("BussinessField"))) recruiter.BussinessField = reader.GetString("BussinessField");
        if (!reader.IsDBNull(reader.GetOrdinal("Username"))) recruiter.Username = reader.GetString("Username");

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
        if (!reader.IsDBNull(reader.GetOrdinal("NewsID"))) recruitNews.NewsID = reader.GetInt32("NewsID");
        if (!reader.IsDBNull(reader.GetOrdinal("NewsName"))) recruitNews.NewsName = reader.GetString("NewsName");
        if (!reader.IsDBNull(reader.GetOrdinal("Deadline"))) recruitNews.DeadLine = reader.GetString("Deadline");
        if (!reader.IsDBNull(reader.GetOrdinal("SalaryRange"))) recruitNews.SalaryRange = reader.GetString("SalaryRange");
        if (!reader.IsDBNull(reader.GetOrdinal("FormOfEmploy")))  recruitNews.FormOfEmploy = reader.GetString("FormOfEmploy");
        if (!reader.IsDBNull(reader.GetOrdinal("Gender"))) recruitNews.Gender = reader.GetString("Gender");
        if (!reader.IsDBNull(reader.GetOrdinal("HiringAmount"))) recruitNews.HiringAmount = reader.GetString("HiringAmount");
        if (!reader.IsDBNull(reader.GetOrdinal("HiringPosition"))) recruitNews.HiringPosition = reader.GetString("HiringPosition");
        if (!reader.IsDBNull(reader.GetOrdinal("RequiredExp"))) recruitNews.RequiredExp = reader.GetString("RequiredExp");
        if (!reader.IsDBNull(reader.GetOrdinal("CityAddress"))) recruitNews.CityAddress = reader.GetString("CityAddress");
        if (!reader.IsDBNull(reader.GetOrdinal("Profession"))) recruitNews.Profession = reader.GetString("Profession");
        if (!reader.IsDBNull(reader.GetOrdinal("RecruiterID"))) recruitNews.RecruiterID = reader.GetInt32("RecruiterID");
        return recruitNews;
    }
}