using DAL;
using Persistence;

namespace BL;

public class RecruiterBL
{
    private RecruiterDAL recruiterDAL;

    public RecruiterBL()
    {
        recruiterDAL = new RecruiterDAL();
    }

    public List<RecruitNews> GetNewsByProfession(string Profession)
    {
        return recruiterDAL.GetNewsByProfession(Profession);
    }

    public List<RecruitNews> GetNewsBySalaryRange(string SalaryRange)
    {
        return recruiterDAL.GetNewsBySalaryRange(SalaryRange);
    }

    public List<RecruitNews> GetNewsByCityAddress(string CityAddress)
    {
        return recruiterDAL.GetNewsByCityAddress(CityAddress);
    }

    public Recruiter GetRecruiterByNewsID(int NewsID)
    {
        return recruiterDAL.GetRecruiterByNewsID(NewsID);
    }
    // NCT 
    public void CreateNewProfile(Recruiter profile, int? RecruiterID)
    {
        int? ProfileID = recruiterDAL.InsertNewProfile(profile, RecruiterID);
        if(ProfileID == null)
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't create new profile. Unexpected problems might have occurred to the connection to the database.");
        }
    }
    public void CreateRecruitment(RecruitNews news, int? RecruiterID)
    {
        int? NewsID = recruiterDAL.InsertRecruitmentNew(news, RecruiterID);
        if(NewsID == null)
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't create the recruitment. Unexpected problems might have occurred to the connection to the database.");
        }
    }
    public Recruiter GetRecruiterByID(int? RecruiterID)
    {
        return recruiterDAL.GetRecruiterByID(RecruiterID);
    }
    public List<RecruitNews> GetRecruitNewByID(int? RecruiterID)
    {
        return recruiterDAL.GetRecruitNewsByRecruterID(RecruiterID);
    }
    public void UpdateNewsInfo(RecruitNews news)
    {
        if(!recruiterDAL.UpdateNews(news))
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your News. Unexpected problems might have occurred to the connection to the database.");
        }
    }
    public void UpdatePersonalRecruitInfo(Recruiter recruiter)
    {
        if(!recruiterDAL.UpdateRecruitInformation(recruiter))
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your information. Unexpected problems might have occurred to the connection to the database.");
        }
    }
}
