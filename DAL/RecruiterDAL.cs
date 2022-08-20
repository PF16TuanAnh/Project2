using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class RecruiterDAL
{
    private string? query;
    private MySqlDataReader? reader;

    public static Recruiter GetRecruiterInfo(MySqlDataReader reader)
    {
        Recruiter recruiter = new Recruiter();
        if (!reader.IsDBNull(reader.GetOrdinal("RecruiterID"))) recruiter.RecruiterID = reader.GetInt32("RecruiterID");
        if (!reader.IsDBNull(reader.GetOrdinal("PhoneNum"))) recruiter.PhoneNum = reader.GetString("PhoneNum");
        if (!reader.IsDBNull(reader.GetOrdinal("Position"))) recruiter.Position = reader.GetString("Position");
        if (!reader.IsDBNull(reader.GetOrdinal("CompanyName"))) recruiter.CompanyName = reader.GetString("CompanyName");
        if (!reader.IsDBNull(reader.GetOrdinal("CompanyDescription"))) recruiter.CompanyDescription = reader.GetString("CompanyDescription");
        if (!reader.IsDBNull(reader.GetOrdinal("BusinessSize"))) recruiter.BusinessSize = reader.GetString("BusinessSize");
        if (!reader.IsDBNull(reader.GetOrdinal("BusinessField"))) recruiter.BusinessField = reader.GetString("BusinessField");
        if (!reader.IsDBNull(reader.GetOrdinal("CompanyAddress"))) recruiter.CompanyAddress = reader.GetString("CompanyAddress");
        if (!reader.IsDBNull(reader.GetOrdinal("Name"))) recruiter.Name = reader.GetString("Name");

        return recruiter;
    }

    public static RecruitNews GetRecruitNewsInfo(MySqlDataReader reader)
    {
        RecruitNews recruitNews = new RecruitNews();
        if (!reader.IsDBNull(reader.GetOrdinal("NewsID"))) recruitNews.NewsID = reader.GetInt32("NewsID");
        if (!reader.IsDBNull(reader.GetOrdinal("NewsName"))) recruitNews.NewsName = reader.GetString("NewsName");
        if (!reader.IsDBNull(reader.GetOrdinal("Deadline"))) recruitNews.DeadLine = reader.GetDateTime("Deadline");
        if (!reader.IsDBNull(reader.GetOrdinal("SalaryRange"))) recruitNews.SalaryRange = reader.GetString("SalaryRange");
        if (!reader.IsDBNull(reader.GetOrdinal("FormOfEmploy")))  recruitNews.FormOfEmploy = reader.GetString("FormOfEmploy");
        if (!reader.IsDBNull(reader.GetOrdinal("Gender"))) recruitNews.Gender = reader.GetString("Gender");
        if (!reader.IsDBNull(reader.GetOrdinal("HiringAmount"))) recruitNews.HiringAmount = reader.GetString("HiringAmount");
        if (!reader.IsDBNull(reader.GetOrdinal("HiringPosition"))) recruitNews.HiringPosition = reader.GetString("HiringPosition");
        if (!reader.IsDBNull(reader.GetOrdinal("RequiredExp"))) recruitNews.RequiredExp = reader.GetString("RequiredExp");
        if (!reader.IsDBNull(reader.GetOrdinal("CityAddress"))) recruitNews.CityAddress = reader.GetString("CityAddress");
        if (!reader.IsDBNull(reader.GetOrdinal("Profession"))) recruitNews.Profession = reader.GetString("Profession");
        if (!reader.IsDBNull(reader.GetOrdinal("RecruiterID"))) recruitNews.RecruiterID = reader.GetInt32("RecruiterID");
        if (!reader.IsDBNull(reader.GetOrdinal("IsOpen"))) recruitNews.IsOpen = reader.GetBoolean("IsOpen");
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
            cmd.Parameters.AddWithValue("@Name", profile.Name);
            cmd.Parameters["@Name"].Direction = System.Data.ParameterDirection.Input;
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
            cmd.Parameters.AddWithValue("@BusinessSize", profile.BusinessSize);
            cmd.Parameters["@BusinessSize"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BusinessField", profile.BusinessField);
            cmd.Parameters["@BusinessField"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@ProfileID", MySqlDbType.Int32);
            cmd.Parameters["@ProfileID"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            ProfileID = (int) cmd.Parameters["@ProfileID"].Value;
        }
        catch {}
        finally
        {
            DBHelper.CloseConnection();
        }
        return ProfileID;
    }
    public Recruiter GetRecruiterByID(int? RecruiterID)
    {
        query = @"select u.Name, c.* from Recruiters c inner join Users u on c.UserID = u.UserID where RecruiterID = " + RecruiterID;
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
        List<RecruitNews> recruitNews = null!;
        try
        {
            DBHelper.CloseConnection();
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

            if(recruitNews == null)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" No results!");
            }
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't retrieve recruitment news. Unexpected problems might have occurred.");
        }
        
        DBHelper.CloseConnection();
        return recruitNews!;
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
        catch {}
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
        catch {return false;}
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
            cmd.Parameters.AddWithValue("@BusinessSize", recruiter.BusinessSize);
            cmd.Parameters["@BusinessSize"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BusinessField", recruiter.BusinessField);
            cmd.Parameters["@BusinessField"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch {return false;}
        finally
        {
            DBHelper.CloseConnection();
        }
        return true;
    }

    public List<CV> GetCVByJobPosition(string JobPosition)
    {
        query = @"select a.* from CVs a, CVDetails b where a.CVID = b.CVID and b.JobPosition like '%" + JobPosition + "%' and Title = 'Work Experience' group by CVID";
        List<CV> cv = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(cv == null)
                {
                    cv = new List<CV>();
                }
                cv.Add(CandidateDAL.GetCVInfo(reader));
            }

            if (cv != null)
            {
                foreach (CV item in cv)
                {
                    item.CVDetails = CandidateDAL.GetCVDetailsByCVID(item.CVID);
                }
            }

            if(cv == null)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" No results!");
            }
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve CVs.");
        }
        
        DBHelper.CloseConnection();

        return cv!;
    }
    
    public List<CV> GetCVByCareerTitle(string CareerTitle)
    {
        query = @"select * from CVs where CareerTitle like '%" + CareerTitle + "%'";
        List<CV> cv = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(cv == null)
                {
                    cv = new List<CV>();
                }
                cv.Add(CandidateDAL.GetCVInfo(reader));
            }

            if (cv != null)
            {
                foreach (CV item in cv)
                {
                    item.CVDetails = CandidateDAL.GetCVDetailsByCVID(item.CVID);
                }
            }

            if(cv == null)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" No results!");
            }
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve CVs.");
        }
        
        DBHelper.CloseConnection();

        return cv!;
    }
    
    public List<CV> GetCVByAddress(string Address)
    {
        query = @"select * from CVs where PersonalAddress like '%" + Address + "%'";
        List<CV> cv = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(cv == null)
                {
                    cv = new List<CV>();
                }
                cv.Add(CandidateDAL.GetCVInfo(reader));
            }

            if (cv != null)
            {
                foreach (CV item in cv)
                {
                    item.CVDetails = CandidateDAL.GetCVDetailsByCVID(item.CVID);
                }
            }

            if(cv == null)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" No results!");
            }
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve CVs.");
        }
        DBHelper.CloseConnection();
        return cv!;
    }
    public List<CV> GetCVAppliedInNews(int NewsID) 
    {
        query = @"select c.* from CVs c, ApplyCandidates a 
        where  a.CandidateID = c.CandidateID and  a.NewsID ='" + NewsID + "'";
        List<CV> cv = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            while (reader.Read())
            {
                if(cv == null)
                {
                    cv = new List<CV>();
                }
                cv.Add(CandidateDAL.GetCVInfo(reader));
            }

            if (cv != null)
            {
                foreach (CV item in cv)
                {
                    item.CVDetails = CandidateDAL.GetCVDetailsByCVID(item.CVID);
                }
            }

            if(cv == null)
            {
                Console.Clear();
                Console.WriteLine("================================"); 
                Console.WriteLine(" No results!");
            }
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve CVs.");
        }
        
        DBHelper.CloseConnection();

        return cv!;
    }
}
