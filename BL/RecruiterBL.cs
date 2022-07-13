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

    // public User GetEmployeeByID(int emp_no)
    // {
    //     Employee employee = employeeDAL.GetEmployeeByID(emp_no);
    //     employee.EmployeeFirstName = employee.EmployeeFirstName!.ToUpper();
    //     employee.EmployeeLastName = employee.EmployeeLastName!.ToUpper();
    //     return employee;
    // }
}