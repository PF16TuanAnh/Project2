using DAL;
using Persistence;

namespace BL;

public class UserBL
{
    private UserDAL userDAL;

    public UserBL()
    {
        userDAL = new UserDAL();
    }

    // public User GetEmployeeByID(int emp_no)
    // {
    //     Employee employee = employeeDAL.GetEmployeeByID(emp_no);
    //     employee.EmployeeFirstName = employee.EmployeeFirstName!.ToUpper();
    //     employee.EmployeeLastName = employee.EmployeeLastName!.ToUpper();
    //     return employee;
    // }
}