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
}