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
        Recruiter recruiter = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);
            
            if (reader.Read())
            {
                recruiter = GetRecruiterInfo(reader);
            }
        }
        catch{}
        

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
        if (!reader.IsDBNull(reader.GetOrdinal("BussinessSize"))) recruiter.BusinessSize = reader.GetString("BussinessSize");
        if (!reader.IsDBNull(reader.GetOrdinal("BussinessField"))) recruiter.BusinessField = reader.GetString("BussinessField");
        if (!reader.IsDBNull(reader.GetOrdinal("CompanyAddress"))) recruiter.CompanyAddress = reader.GetString("CompanyAddress");

        return recruiter;
    }

    public List<RecruitNews> GetNewsBySalaryRange(string SalaryRange)
    {
        query = @"select * from RecruitNews where SalaryRange = '" + SalaryRange + "'" + " and IsOpen = true";
        List<RecruitNews> recruitNews = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(recruitNews == null)
                {
                    recruitNews = new List<RecruitNews>();
                }
                recruitNews.Add(GetRecruitNewsInfo(reader));
            }
        }
        catch
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Couldn't retrieve recruitment news.");
        }
        
        DBHelper.CloseConnection();

        return recruitNews;
    }

    public List<RecruitNews> GetNewsByCityAddress(string CityAddress)
    {
        query = @"select * from RecruitNews where CityAddress = '" + CityAddress + "'" + " and IsOpen = true";
        List<RecruitNews> recruitNews = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(recruitNews == null)
                {
                    recruitNews = new List<RecruitNews>();
                }
                recruitNews.Add(GetRecruitNewsInfo(reader));
            }
        }
        catch
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Couldn't retrieve recruitment news.");
        }
        
        DBHelper.CloseConnection();

        return recruitNews;
    }

    public List<RecruitNews> GetNewsByProfession(string Profession)
    {
        query = @"select * from RecruitNews where Profession = '" + Profession + "'" + " and IsOpen = true";
        List<RecruitNews> recruitNews = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(recruitNews == null)
                {
                    recruitNews = new List<RecruitNews>();
                }
                recruitNews.Add(GetRecruitNewsInfo(reader));
            }
        }
        catch
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Couldn't retrieve recruitment news.");
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
    // NCT 
    public int? InsertNewProfile(Recruiter profile, int? RecruiterID) 
    {
        MySqlCommand cmd = new MySqlCommand("sp_InsertProfile", DBHelper.OpenConnection());
        int? ProfileID = null;
        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecruiterID", RecruiterID);
            cmd.Parameters["@RecruiterID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PhoneNum", profile.PhoneNum);
            cmd.Parameters["@PhoneNum"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Position", profile.Position);
            cmd.Parameters["@Position"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CompanyName", profile.CompanyName);
            cmd.Parameters["@CompanyName"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CompanyDescription", profile.CompanyDescription);
            cmd.Parameters["@CompanyDescription"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CompanyAddress", profile.CompanyAddress);
            cmd.Parameters["@CompanyAddress"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BussinessSize", profile.BusinessSize);
            cmd.Parameters["@BussinessSize"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BussinessField", profile.BusinessField);
            cmd.Parameters["@BussinessField"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@ProfileID", MySqlDbType.Int32);
            cmd.Parameters["@ProfileID"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            ProfileID = (int) cmd.Parameters["@ProfileID"].Value;
        }
        catch (Exception e){throw e;}
        finally
        {
            DBHelper.CloseConnection();
        }
        return ProfileID;
    }
    public Recruiter GetRecruiterByID(int? RecruiterID)
    {
        query = @"select * from Recruiters c inner join Users u on c.UserID = u.UserID where RecruiterID = " + RecruiterID;
        Recruiter recruiter = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            
            if (reader.Read())
            {
                recruiter = GetRecruiterInfo(reader);
            }
        }
        catch{}
        
        DBHelper.CloseConnection();

        return recruiter;
    }
    public List<RecruitNews> GetRecruitNewsByRecruterID(int? RecruiterID){
        query = @"select * from RecruitNews where RecruiterID = " + RecruiterID;
        List<RecruitNews> recruiternew = null!;
        try
        {
            DBHelper.CloseConnection();
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
        {
            if(recruiternew == null)
            {
                recruiternew = new List<RecruitNews>();
            }
            recruiternew.Add(GetRecruitNewsInfo(reader));
        }
            // if (reader.Read())
            // {
            //     recruiternew = GetRecruitNewsInfo(reader);
            // }
        }
        catch{}
        
        DBHelper.CloseConnection();
        return recruiternew;
    }
    public int? InsertRecruitmentNew(RecruitNews news, int? RecruiterID) 
    {
        MySqlCommand cmd = new MySqlCommand("sp_InsertRecruitmentNew", DBHelper.OpenConnection());
        int? NewsID = null;
        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecruiterID", RecruiterID);
            cmd.Parameters["@RecruiterID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@NewsName", news.NewsName);
            cmd.Parameters["@NewsName"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Deadline", news.DeadLine);
            cmd.Parameters["@Deadline"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@FormOfEmploy", news.FormOfEmploy);
            cmd.Parameters["@FormOfEmploy"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Gender", news.Gender);
            cmd.Parameters["@Gender"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@HiringAmount", news.HiringAmount);
            cmd.Parameters["@HiringAmount"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@HiringPosition", news.HiringPosition);
            cmd.Parameters["@HiringPosition"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@RequiredExp", news.RequiredExp);
            cmd.Parameters["@RequiredExp"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@IsOpen", news.IsOpen);
            cmd.Parameters["@IsOpen"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@SalaryRange", news.SalaryRange);
            cmd.Parameters["@SalaryRange"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CityAddress", news.CityAddress); 
            cmd.Parameters["@CityAddress"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Profession", news.Profession);
            cmd.Parameters["@Profession"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@NewsID", MySqlDbType.Int32);
            cmd.Parameters["@NewsID"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            NewsID = (int) cmd.Parameters["@NewsID"].Value;
        }
        catch (Exception e){throw e;}
        finally
        {
            DBHelper.CloseConnection();
        }
        return NewsID;
    }
    public bool UpdateNews(RecruitNews news)  
    {
        MySqlCommand cmd = new MySqlCommand("sp_UpdateNews", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewsID", news.NewsID);
            cmd.Parameters["@NewsID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@NewsName", news.NewsName);
            cmd.Parameters["@NewsName"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Deadline", news.DeadLine);
            cmd.Parameters["@Deadline"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@FormOfEmploy", news.FormOfEmploy);
            cmd.Parameters["@FormOfEmploy"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Gender", news.Gender);
            cmd.Parameters["@Gender"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@HiringAmount", news.HiringAmount);
            cmd.Parameters["@HiringAmount"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@HiringPosition", news.HiringPosition);
            cmd.Parameters["@HiringPosition"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@RequiredExp", news.RequiredExp);
            cmd.Parameters["@RequiredExp"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@IsOpen", news.IsOpen);
            cmd.Parameters["@IsOpen"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@SalaryRange", news.SalaryRange);
            cmd.Parameters["@SalaryRange"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CityAddress", news.CityAddress); 
            cmd.Parameters["@CityAddress"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Profession", news.Profession);
            cmd.Parameters["@Profession"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch (Exception e){throw e;}
        finally
        {
            DBHelper.CloseConnection();
        }
        return true;
    }
    public bool UpdateRecruitInformation(Recruiter recruiter) 
    {
        MySqlCommand cmd = new MySqlCommand("sp_UpdateRecruitInfor", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RecruiterID", recruiter.RecruiterID);
            cmd.Parameters["@RecruiterID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PhoneNum", recruiter.PhoneNum);
            cmd.Parameters["@PhoneNum"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Position", recruiter.Position);
            cmd.Parameters["@Position"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CompanyName", recruiter.CompanyName);
            cmd.Parameters["@CompanyName"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CompanyDescription", recruiter.CompanyDescription);
            cmd.Parameters["@CompanyDescription"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CompanyAddress", recruiter.CompanyAddress);
            cmd.Parameters["@CompanyAddress"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BussinessSize", recruiter.BusinessSize);
            cmd.Parameters["@BussinessSize"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BussinessField", recruiter.BusinessField);
            cmd.Parameters["@BussinessField"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch (Exception e){throw e;}
        finally
        {
            DBHelper.CloseConnection();
        }
        return true;
    }
}