namespace Pl_Console;
using BL;
using Persistence;
using System.Text.RegularExpressions;
using ConsoleTables;

public class RecruiterController
{
    private readonly Regex NumericRegex = new Regex(@"^[0-9]*$");
    private RecruiterBL recruiterBL;

    public RecruiterController()
    {
        recruiterBL = new RecruiterBL();
    }

    
}
