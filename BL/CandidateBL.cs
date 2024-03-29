using DAL;
using Persistence;

namespace BL;

public class CandidateBL
{
    private CandidateDAL candidateDAL;

    public CandidateBL()
    {
        candidateDAL = new CandidateDAL();
    }

    public Candidate GetCandidateByID(int? CandidateID)
    {
        return candidateDAL.GetCandidateByID(CandidateID);
    }

    public bool UpdateCVInfo(CV cv)
    {
        if(!candidateDAL.UpdateCV(cv))
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your CV. Unexpected problems might have occurred.");
            return false;
        }
        return true;
    }

    public int? CreateNewCV(CV cv, int? CandidateID)
    {
        int? CVID = candidateDAL.AddNewCV(cv, CandidateID);
        if(CVID == null)
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't create new CV. Unexpected problems.");
        }
        return CVID;
    }

    public bool ApplyToNews(int? CandidateID, int NewsID)
    {
        if (!candidateDAL.InsertToApplyCandidates(CandidateID, NewsID))
        {
            Console.Clear();
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't apply to the recruitment news. Unexpected problems might have occurred.");
            return false;
        }
        return true;
    }

    public bool IsApplied(int? CandidateID, int NewsID)
    {
        return candidateDAL.GetApplyStatusByNewsID(CandidateID, NewsID);
    }
    
    public List<RecruitNews> GetNewsByProfession(string Profession)
    {
        return candidateDAL.GetNewsByProfession(Profession);
    }

    public List<RecruitNews> GetNewsBySalaryRange(string SalaryRange)
    {
        return candidateDAL.GetNewsBySalaryRange(SalaryRange);
    }

    public List<RecruitNews> GetNewsByCityAddress(string CityAddress)
    {
        return candidateDAL.GetNewsByCityAddress(CityAddress);
    }

    public Recruiter GetRecruiterByNewsID(int NewsID)
    {
        return candidateDAL.GetRecruiterByNewsID(NewsID);
    }
}
