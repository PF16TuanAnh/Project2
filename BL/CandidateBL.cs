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

    public void UpdateCVInfo(CV cv)
    {
        candidateDAL.UpdateCV(cv);
        if (cv.CVDetails != null)
        {
            foreach (CVDetails details in cv.CVDetails)
            {
                if(candidateDAL.GetCVDetailsByID(details.DetailsID) != null)
                {
                    candidateDAL.ChangeCVDetails(details);
                }
                else
                {
                    candidateDAL.InsertNewCVDetails(details);
                }
            }
        }
    }

    public void CreateNewCV(CV cv, int? CandidateID)
    {
        candidateDAL.InsertNewCV(cv, CandidateID);
        if (cv.CVDetails != null)
        {
            foreach (CVDetails details in cv.CVDetails)
            {
                candidateDAL.InsertNewCVDetails(details);
            }
        }
    }

    public void ApplyToNews(int? CandidateID, int NewsID)
    {
        candidateDAL.InsertToApplyCandidates(CandidateID, NewsID);
    }

    public bool IsApplied(int? CandidateID, int NewsID)
    {
        return candidateDAL.GetApplyStatusByNewsID(CandidateID, NewsID);
    }
}