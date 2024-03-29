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
        Candidate candidate = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            
            if (reader.Read())
            {
                candidate = GetCandidateInfo(reader);
            }
        }
        catch{}
        
        DBHelper.CloseConnection();

        return candidate;
    }

    public Candidate GetCandidateInfo(MySqlDataReader reader)
    {
        Candidate candidate = new Candidate();
        if (!reader.IsDBNull(reader.GetOrdinal("CandidateID"))) candidate.CandidateID = reader.GetInt32("CandidateID");
        if (!reader.IsDBNull(reader.GetOrdinal("Name"))) candidate.Name = reader.GetString("Name");
        candidate.CandidateCV = GetCVByCandidateID(candidate.CandidateID);
        if (candidate.CandidateCV != null)
        {
            candidate.CandidateCV.CVDetails = GetCVDetailsByCVID(candidate.CandidateCV.CVID);
        }

        return candidate;
    }

    public CV GetCVByCandidateID(int CandidateID)
    {
        query = @"select * from CVs where CandidateID = " + CandidateID;
        
        DBHelper.CloseConnection();
        DBHelper.OpenConnection();
        reader = DBHelper.ExecQuery(query);

        CV cv = null!;
        if (reader.Read())
        {
            cv = GetCVInfo(reader);
        }

        return cv;
    }

    public int? AddNewCV(CV cv, int? CandidateID)
    {
        int? CVID = null;
        MySqlConnection connection = DBHelper.OpenConnection();
        if(connection != null)
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader = null!;

            try
            {
                // Insert into CVs
                cmd.CommandText = @"insert into CVs (CandidateID, FullName, CareerTitle, CareerObjective, Email, BirthDate, PhoneNum, PersonalAddress, SocialMedia)
                values (@CandidateID, @FullName, @CareerTitle, @CareerObjective, @Email, @BirthDate, @PhoneNum, @PersonalAddress, @SocialMedia);";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CandidateID", CandidateID);
                cmd.Parameters.AddWithValue("@FullName", cv.FullName);
                cmd.Parameters.AddWithValue("@CareerTitle", cv.CareerTitle);
                cmd.Parameters.AddWithValue("@Email", cv.Email);
                cmd.Parameters.AddWithValue("@BirthDate", cv.BirthDate);
                cmd.Parameters.AddWithValue("@PhoneNum", cv.PhoneNum);
                cmd.Parameters.AddWithValue("@CareerObjective", cv.CareerObjective);
                cmd.Parameters.AddWithValue("@PersonalAddress", cv.PersonalAddress);
                cmd.Parameters.AddWithValue("@SocialMedia", cv.SocialMedia);
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"select CVID from CVs where CandidateID = " + CandidateID;
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    CVID = reader.GetInt32("CVID");
                }
                reader.Close();

                // Insert into CVDetails
                if (cv.CVDetails != null && CVID != null)
                {
                    foreach (CVDetails detail in cv.CVDetails)
                    {
                        cmd.CommandText = @"insert into CVDetails (CVID, Association, Descriptions, FromDate, ToDate, Title, JobPosition)
                        values (@CVID, @Association, @Descriptions, @FromDate, @ToDate, @Title, @JobPosition);";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CVID", CVID);
                        cmd.Parameters.AddWithValue("@Association", detail.Association);
                        cmd.Parameters.AddWithValue("@Descriptions", detail.Description);
                        cmd.Parameters.AddWithValue("@FromDate", detail.FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", detail.ToDate);
                        cmd.Parameters.AddWithValue("@Title", detail.Title);
                        cmd.Parameters.AddWithValue("@JobPosition", detail.JobPosition);
                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
            catch 
            {
                CVID = null;
                try
                {
                    trans.Rollback();
                }
                catch{}
            }
            finally
            {
                DBHelper.CloseConnection();
            }
        }

        return CVID;
    }

    public bool UpdateCV(CV cv)
    {
        bool success = false;
        MySqlConnection connection = DBHelper.OpenConnection();
        if(connection != null)
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;

            try
            {
                // Update CVs
                cmd.CommandText = @"update CVs c
                set c.FullName = @FullName, c.CareerTitle = @CareerTitle,
                c.CareerObjective = @CareerObjective, c.Email = @Email,
                c.BirthDate = @BirthDate, c.PhoneNum = @PhoneNum,
                c.PersonalAddress = @PersonalAddress, c.SocialMedia = @SocialMedia
                where c.CVID = @CVID;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CVID", cv.CVID);
                cmd.Parameters.AddWithValue("@FullName", cv.FullName);
                cmd.Parameters.AddWithValue("@CareerTitle", cv.CareerTitle);
                cmd.Parameters.AddWithValue("@Email", cv.Email);
                cmd.Parameters.AddWithValue("@BirthDate", cv.BirthDate);
                cmd.Parameters.AddWithValue("@PhoneNum", cv.PhoneNum);
                cmd.Parameters.AddWithValue("@CareerObjective", cv.CareerObjective);
                cmd.Parameters.AddWithValue("@PersonalAddress", cv.PersonalAddress);
                cmd.Parameters.AddWithValue("@SocialMedia", cv.SocialMedia);
                cmd.ExecuteNonQuery();

                // Insert into or Update CVDetails
                if (cv.CVDetails != null)
                {
                    foreach (CVDetails detail in cv.CVDetails)
                    {
                        if (detail.DetailsID == null) // Insert into CVDetails
                        {
                            cmd.CommandText = @"insert into CVDetails (CVID, Association, Descriptions, FromDate, ToDate, Title, JobPosition)
                            values (@CVID, @Association, @Descriptions, @FromDate, @ToDate, @Title, @JobPosition);";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@CVID",cv.CVID);
                            cmd.Parameters.AddWithValue("@Association", detail.Association);
                            cmd.Parameters.AddWithValue("@Descriptions", detail.Description);
                            cmd.Parameters.AddWithValue("@FromDate", detail.FromDate);
                            cmd.Parameters.AddWithValue("@ToDate", detail.ToDate);
                            cmd.Parameters.AddWithValue("@Title", detail.Title);
                            cmd.Parameters.AddWithValue("@JobPosition", detail.JobPosition);
                            cmd.ExecuteNonQuery();
                        }
                        else // Update CVDetails
                        {
                            cmd.CommandText = @"update CVDetails c
                            set c.Association = @Association, c.Descriptions = @Descriptions,
                            c.FromDate = @FromDate, c.ToDate = @ToDate,
                            c.Title = @Title, c.JobPosition = @JobPosition
                            where c.DetailID = @DetailID;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@DetailID", detail.DetailsID);
                            cmd.Parameters.AddWithValue("@Association", detail.Association);
                            cmd.Parameters.AddWithValue("@Descriptions", detail.Description);
                            cmd.Parameters.AddWithValue("@FromDate", detail.FromDate);
                            cmd.Parameters.AddWithValue("@ToDate", detail.ToDate);
                            cmd.Parameters.AddWithValue("@Title", detail.Title);
                            cmd.Parameters.AddWithValue("@JobPosition", detail.JobPosition);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
                success = true;
            }
            catch 
            {
                success = false;
                try
                {
                    trans.Rollback();
                }
                catch{}
            }
            finally
            {
                DBHelper.CloseConnection();
            }
        }

        return success;
    }

    public static CV GetCVInfo(MySqlDataReader reader)
    {
        CV cv = new CV();
        if (!reader.IsDBNull(reader.GetOrdinal("CVID"))) cv.CVID = reader.GetInt32("CVID");
        if (!reader.IsDBNull(reader.GetOrdinal("FullName"))) cv.FullName = reader.GetString("FullName");
        if (!reader.IsDBNull(reader.GetOrdinal("CareerTitle"))) cv.CareerTitle = reader.GetString("CareerTitle");
        if (!reader.IsDBNull(reader.GetOrdinal("CareerObjective"))) cv.CareerObjective = reader.GetString("CareerObjective");
        if (!reader.IsDBNull(reader.GetOrdinal("BirthDate"))) cv.BirthDate = reader.GetDateTime("BirthDate");
        if (!reader.IsDBNull(reader.GetOrdinal("PhoneNum"))) cv.PhoneNum = reader.GetString("PhoneNum");
        if (!reader.IsDBNull(reader.GetOrdinal("Email"))) cv.Email = reader.GetString("Email");
        if (!reader.IsDBNull(reader.GetOrdinal("SocialMedia"))) cv.SocialMedia = reader.GetString("SocialMedia");
        if (!reader.IsDBNull(reader.GetOrdinal("PersonalAddress"))) cv.PersonalAddress = reader.GetString("PersonalAddress");

        return cv;
    }

    public static List<CVDetails> GetCVDetailsByCVID(int CVID)
    {
        string query = @"select * from CVDetails where CVID = " + CVID;
        
        DBHelper.CloseConnection();
        DBHelper.OpenConnection();
        MySqlDataReader reader = DBHelper.ExecQuery(query);

        List<CVDetails> CVDetails = null!;
        while (reader.Read())
        {
            if(CVDetails == null)
            {
                CVDetails = new List<CVDetails>();
            }
            CVDetails.Add(GetCVDetailsInfo(reader));
        }

        return CVDetails!;
    }

    public static CVDetails GetCVDetailsInfo(MySqlDataReader reader)
    {
        CVDetails cVDetails = new CVDetails();
        if (!reader.IsDBNull(reader.GetOrdinal("DetailID"))) cVDetails.DetailsID = reader.GetInt32("DetailID");
        if (!reader.IsDBNull(reader.GetOrdinal("CVID"))) cVDetails.CVID = reader.GetInt32("CVID");
        if (!reader.IsDBNull(reader.GetOrdinal("Title")))cVDetails.Title = reader.GetString("Title");
        if (!reader.IsDBNull(reader.GetOrdinal("JobPosition")))cVDetails.JobPosition = reader.GetString("JobPosition");
        if (!reader.IsDBNull(reader.GetOrdinal("FromDate")))cVDetails.FromDate = reader.GetDateTime("FromDate");
        if (!reader.IsDBNull(reader.GetOrdinal("ToDate")))cVDetails.ToDate = reader.GetDateTime("ToDate");
        if (!reader.IsDBNull(reader.GetOrdinal("Association"))) cVDetails.Association = reader.GetString("Association");
        if (!reader.IsDBNull(reader.GetOrdinal("Descriptions"))) cVDetails.Description = reader.GetString("Descriptions");
        return cVDetails;
    }

    public bool InsertToApplyCandidates(int? CandidateID, int NewsID)
    {
        MySqlCommand cmd = new MySqlCommand("sp_InsertToApplyCandidates", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CandidateID", CandidateID);
            cmd.Parameters["@CandidateID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@NewsID", NewsID);
            cmd.Parameters["@NewsID"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch{return false;}
        finally
        {
            DBHelper.CloseConnection();
        }

        return true;
    }


    public bool GetApplyStatusByNewsID(int? CandidateID, int NewsID)
    {
        bool IsApplied = false;
        query = @"select * from ApplyCandidates where CandidateID = " + CandidateID + " and  NewsID = " + NewsID;
        
        try
        {
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);

            if (reader.Read())
            {
                IsApplied = true;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Unexpected problems might have occurred. Couldn't check whether the user has applied or not.");
            Console.WriteLine("================================");
        }
        

        DBHelper.CloseConnection();

        return IsApplied;
    }
    
    public Recruiter GetRecruiterByNewsID(int NewsID)
    {
        query = @"select r.*, u.Name from Recruiters r inner join RecruitNews rn on r.RecruiterID = rn.RecruiterID inner join Users u on r.UserID = u.UserID where NewsID = " + NewsID;
        Recruiter recruiter = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);
            
            if (reader.Read())
            {
                recruiter = RecruiterDAL.GetRecruiterInfo(reader);
            }
        }
        catch{}
        

        DBHelper.CloseConnection();

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
                recruitNews.Add(RecruiterDAL.GetRecruitNewsInfo(reader));
            }

            if (recruitNews == null)
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
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve recruitment news.");
        }
        
        DBHelper.CloseConnection();

        return recruitNews!;
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
                recruitNews.Add(RecruiterDAL.GetRecruitNewsInfo(reader));
            }

            if (recruitNews == null)
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
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve recruitment news.");
        }
        
        DBHelper.CloseConnection();

        return recruitNews!;
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
                recruitNews.Add(RecruiterDAL.GetRecruitNewsInfo(reader));
            }

            if (recruitNews == null)
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
            Console.WriteLine(" Unexpected problems might have occurred. Couldn't retrieve recruitment news.");
        }
        
        DBHelper.CloseConnection();

        return recruitNews!;
    }
}
