using Persistence;
using MySql.Data.MySqlClient;

namespace DAL;

public class UserDAL
{
    private string? query;
    private MySqlDataReader? reader;

    // public Employee GetEmployeeByID(int empId)
    // {
    //     query = @"select emp_no, first_name, last_name from employees where emp_no = " + empId;

        
    //     DBHelper.OpenConnection();
        
    //     reader = DBHelper.ExecQuery(query);

    //     Employee employee = null!;
    //     if (reader.Read())
    //     {
    //         employee = GetEmployeeInfo(reader);
    //     }

    //     DBHelper.CloseConnection();

    //     return employee;
    // }

    private User GetUserInfo(MySqlDataReader reader)
    {
        User user = new User();
        user.UserID = reader.GetInt32("UserID");
        user.Username = reader.GetString("Username");
        user.Password = reader.GetString("UserPassword");
        user.Email = reader.GetString("Email");
        user.Gender = reader.GetString("Gender");

        return user;
    }
}