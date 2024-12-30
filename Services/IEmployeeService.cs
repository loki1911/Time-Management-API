using TimeMangementSystemAPI.DTOs;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDto GetEmployeeByEmail(string emailId);
        int SaveEmployee(EmployeeData employeeData);

        int UpdateEmployee(EmployeeData employeeData);
    }
}
