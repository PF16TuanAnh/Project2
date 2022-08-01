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
        if(candidateDAL.UpdateCV(cv))
        {
            if (cv.CVDetails != null)
            {
                foreach (CVDetails details in cv.CVDetails)
                {
                    CVDetails? check = candidateDAL.GetCVDetailsByID(details.DetailsID);
                    if(check != null && check.Description != null)
                    {
                        if(!candidateDAL.ChangeCVDetails(details))
                        {
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Parts of the info couldn't be updated.");
                            break;
                        }
                    }
                    else if (check == null)
                    {
                        if(!candidateDAL.InsertNewCVDetails(details, cv.CVID))
                        {
                            Console.WriteLine("================================"); 
                            Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Parts of the info couldn't be updated.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Parts of the info couldn't be updated.");
                        break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't update your CV. Unexpected problems might have occurred to the connection to the database.");
        }
    }

    public void CreateNewCV(CV cv, int? CandidateID)
    {
        int? CVID = candidateDAL.InsertNewCV(cv, CandidateID);
        if(CVID != null)
        {
            if (cv.CVDetails != null)
            {
                foreach (CVDetails details in cv.CVDetails)
                {
                    if(!candidateDAL.InsertNewCVDetails(details, CVID))
                    {
                        Console.WriteLine("================================"); 
                        Console.WriteLine(" Unexpected problems might have occurred to the connection to the database. Parts of the CV will be missing");
                        break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't create new CV. Unexpected problems might have occurred to the connection to the database.");
        }
    }

    public void ApplyToNews(int? CandidateID, int NewsID)
    {
        if (!candidateDAL.InsertToApplyCandidates(CandidateID, NewsID))
        {
            Console.WriteLine("================================"); 
            Console.WriteLine(" Couldn't apply to the recruitment news. Unexpected problems might have occurred to the connection to the database.");
        }
    }

    public bool IsApplied(int? CandidateID, int NewsID)
    {
        return candidateDAL.GetApplyStatusByNewsID(CandidateID, NewsID);
    }
}