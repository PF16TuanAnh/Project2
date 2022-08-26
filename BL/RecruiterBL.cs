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

    public int? CreateNewProfile(Recruiter profile, int? RecruiterID)
    {
        int? ProfileID = recruiterDAL.InsertNewProfile(profile, RecruiterID);
        if(ProfileID == null)
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't add profile information. Unexpected problems might have occurred.");
        }
        else
        {
            Console.Clear();
        }
        return ProfileID;
    }
    public int? CreateRecruitment(RecruitNews news, int? RecruiterID)
    {
        int? NewsID = recruiterDAL.InsertRecruitmentNew(news, RecruiterID);
        if(NewsID == null)
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't add the recruitment news. Unexpected problems might have occurred.");
        }
        else
        {
            Console.Clear();
        }
        return NewsID;
    }
    public Recruiter GetRecruiterByID(int? RecruiterID)
    {
        return recruiterDAL.GetRecruiterByID(RecruiterID);
    }
    public List<RecruitNews> GetRecruitNewByID(int? RecruiterID)
    {
        return recruiterDAL.GetRecruitNewsByRecruterID(RecruiterID);
    }
    public bool UpdateNewsInfo(RecruitNews news)
    {
        if(!recruiterDAL.UpdateNews(news))
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your recruitment news. Unexpected problems might have occurred.");
        }
        else
        {
            Console.Clear();
        }
        return true;
    }
    public bool UpdatePersonalRecruitInfo(Recruiter recruiter)
    {
        if(!recruiterDAL.UpdateRecruitInformation(recruiter))
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your information. Unexpected problems might have occurred.");
        }
        else
        {
            Console.Clear();
        }
        return true;
    }
    public List<CV> GetCVByJobPosition(string JobPosition)
    {
        return recruiterDAL.GetCVByJobPosition(JobPosition);
    }
    public List<CV> GetCVByCareerTitle(string CareerTitle)
    {
        return recruiterDAL.GetCVByCareerTitle(CareerTitle);
    }
    public List<CV> GetCVByAddress(string Address)
    {
        return recruiterDAL.GetCVByAddress(Address);
    }  
    public List<CV> GetCVAppliedInNews(int NewsID)
    {
        return recruiterDAL.GetCVAppliedInNews(NewsID);
    }
}
