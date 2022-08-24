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

    // NCT 
    public void CreateNewProfile(Recruiter profile, int? RecruiterID)
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
    }
    public void CreateRecruitment(RecruitNews news, int? RecruiterID)
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
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your recruitment news. Unexpected problems might have occurred.");
        }
        else
        {
            Console.Clear();
        }
    }
    public void UpdatePersonalRecruitInfo(Recruiter recruiter)
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
