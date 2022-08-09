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
}