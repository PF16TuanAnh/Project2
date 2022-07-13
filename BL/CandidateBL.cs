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

    // public User GetEmployeeByID(int emp_no)
    // {
    //     Employee employee = employeeDAL.GetEmployeeByID(emp_no);
    //     employee.EmployeeFirstName = employee.EmployeeFirstName!.ToUpper();
    //     employee.EmployeeLastName = employee.EmployeeLastName!.ToUpper();
    //     return employee;
    // }
}