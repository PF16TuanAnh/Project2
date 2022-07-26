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

    private Candidate GetCandidateInfo(MySqlDataReader reader)
    {
        Candidate candidate = new Candidate();
        if (!reader.IsDBNull(reader.GetOrdinal("CandidateID"))) candidate.CandidateID = reader.GetInt32("CandidateID");
        if (!reader.IsDBNull(reader.GetOrdinal("Username"))) candidate.Username = reader.GetString("Username");
        candidate.CandidateCV = GetCVByCandidateID(candidate.CandidateID);

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

    public bool UpdateCV(CV cv)
    {
        MySqlCommand cmd = new MySqlCommand("sp_UpdateCV", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CVID", cv.CVID);
            cmd.Parameters["@CVID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@FullName", cv.FullName);
            cmd.Parameters["@FullName"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CareerTitle", cv.CareerTitle);
            cmd.Parameters["@CareerTitle"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CareerObjective", cv.CareerObjective);
            cmd.Parameters["@CareerObjective"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Email", cv.Email);
            cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BirthDate", cv.BirthDate);
            cmd.Parameters["@BirthDate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PhoneNum", cv.PhoneNum);
            cmd.Parameters["@PhoneNum"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PersonalAddress", cv.PersonalAddress);
            cmd.Parameters["@PersonalAddress"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@SocialMedia", cv.SocialMedia);
            cmd.Parameters["@SocialMedia"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch {return false;}
        finally
        {
            DBHelper.CloseConnection();
        }
        return true;
    }

    public int? InsertNewCV(CV cv, int? CandidateID)
    {
        MySqlCommand cmd = new MySqlCommand("sp_InsertCV", DBHelper.OpenConnection());
        int? CVID = null;

        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CandidateID", CandidateID);
            cmd.Parameters["@CandidateID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@FullName", cv.FullName);
            cmd.Parameters["@FullName"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CareerTitle", cv.CareerTitle);
            cmd.Parameters["@CareerTitle"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CareerObjective", cv.CareerObjective);
            cmd.Parameters["@CareerObjective"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Email", cv.Email);
            cmd.Parameters["@Email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@BirthDate", cv.BirthDate);
            cmd.Parameters["@BirthDate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PhoneNum", cv.PhoneNum);
            cmd.Parameters["@PhoneNum"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PersonalAddress", cv.PersonalAddress);
            cmd.Parameters["@PersonalAddress"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@SocialMedia", cv.SocialMedia);
            cmd.Parameters["@SocialMedia"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CVID", MySqlDbType.Int32);
            cmd.Parameters["@CVID"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            CVID = (int) cmd.Parameters["@CVID"].Value;
        }
        catch {}
        finally
        {
            DBHelper.CloseConnection();
        }
        return CVID;
    }

    private CV GetCVInfo(MySqlDataReader reader)
    {
        CV cv = new CV();
        if (!reader.IsDBNull(reader.GetOrdinal("CVID"))) cv.CVID = reader.GetInt32("CVID");
        if (!reader.IsDBNull(reader.GetOrdinal("FullName"))) cv.FullName = reader.GetString("FullName");
        if (!reader.IsDBNull(reader.GetOrdinal("CareerTitle"))) cv.CareerTitle = reader.GetString("CareerTitle");
        if (!reader.IsDBNull(reader.GetOrdinal("CareerObjective"))) cv.CareerObjective = reader.GetString("CareerObjective");
        if (!reader.IsDBNull(reader.GetOrdinal("BirthDate"))) cv.BirthDate = reader.GetString("BirthDate");
        if (!reader.IsDBNull(reader.GetOrdinal("PhoneNum"))) cv.PhoneNum = reader.GetString("PhoneNum");
        if (!reader.IsDBNull(reader.GetOrdinal("Email"))) cv.Email = reader.GetString("Email");
        if (!reader.IsDBNull(reader.GetOrdinal("SocialMedia"))) cv.SocialMedia = reader.GetString("SocialMedia");
        if (!reader.IsDBNull(reader.GetOrdinal("PersonalAddress"))) cv.PersonalAddress = reader.GetString("PersonalAddress");
        cv.CVDetails = GetCVDetailsByCVID(cv.CVID);

        return cv;
    }

    public CVDetails GetCVDetailsByID(int DetailID)
    {
        
        query = @"select * from CVDetails where DetailID = " +  DetailID;
        CVDetails cVDetails = null!;
        
        try
        {
            DBHelper.OpenConnection();
        
            reader = DBHelper.ExecQuery(query);

            
            if (reader.Read())
            {
                cVDetails = GetCVDetailsInfo(reader);
            }
        }
        catch
        {
            return new CVDetails();
        }
        

        DBHelper.CloseConnection();

        return cVDetails;
    }

    public List<CVDetails> GetCVDetailsByCVID(int CVID)
    {
        query = @"select * from CVDetails where CVID = " + CVID;
        
        DBHelper.CloseConnection();
        DBHelper.OpenConnection();
        reader = DBHelper.ExecQuery(query);

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

    public bool InsertNewCVDetails(CVDetails cVDetails, int? CVID)
    {
        MySqlCommand cmd = new MySqlCommand("sp_InsertCVDetails", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CVID", CVID);
            cmd.Parameters["@CVID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Association", cVDetails.Association);
            cmd.Parameters["@Association"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Descriptions", cVDetails.Description);
            cmd.Parameters["@Descriptions"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@FromDate", cVDetails.FromDate);
            cmd.Parameters["@FromDate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@ToDate", cVDetails.ToDate);
            cmd.Parameters["@ToDate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Title", cVDetails.Title);
            cmd.Parameters["@Title"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@JobPosition", cVDetails.JobPosition);
            cmd.Parameters["@JobPosition"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch
        {
            return false;
        }
        finally
        {
            DBHelper.CloseConnection();
        }
        return true;
    }

    public void ChangeCVDetails(CVDetails cVDetails)
    {
        MySqlCommand cmd = new MySqlCommand("sp_ChangeCVDetails", DBHelper.OpenConnection());
        try
        {   
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DetailID", cVDetails.DetailsID);
            cmd.Parameters["@DetailID"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Association", cVDetails.Association);
            cmd.Parameters["@Association"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Descriptions", cVDetails.Description);
            cmd.Parameters["@Descriptions"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@FromDate", cVDetails.FromDate);
            cmd.Parameters["@FromDate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@ToDate", cVDetails.ToDate);
            cmd.Parameters["@ToDate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Title", cVDetails.Title);
            cmd.Parameters["@Title"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@JobPosition", cVDetails.JobPosition);
            cmd.Parameters["@JobPosition"].Direction = System.Data.ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch {}
        finally
        {
            DBHelper.CloseConnection();
        }
    }

    private CVDetails GetCVDetailsInfo(MySqlDataReader reader)
    {
        CVDetails cVDetails = new CVDetails();
        if (!reader.IsDBNull(reader.GetOrdinal("DetailID"))) cVDetails.DetailsID = reader.GetInt32("DetailID");
        if (!reader.IsDBNull(reader.GetOrdinal("CVID"))) cVDetails.CVID = reader.GetInt32("CVID");
        if (!reader.IsDBNull(reader.GetOrdinal("Title")))cVDetails.Title = reader.GetString("Title");
        if (!reader.IsDBNull(reader.GetOrdinal("JobPosition")))cVDetails.JobPosition = reader.GetString("JobPosition");
        if (!reader.IsDBNull(reader.GetOrdinal("FromDate")))cVDetails.FromDate = reader.GetString("FromDate");
        if (!reader.IsDBNull(reader.GetOrdinal("ToDate")))cVDetails.ToDate = reader.GetString("ToDate");
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
        catch{}
        

        DBHelper.CloseConnection();

        return IsApplied;
    }
}