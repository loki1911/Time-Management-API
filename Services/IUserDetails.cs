using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public interface IUserDetails
    {
        List<UserDetailsDTO> GetEmployeeTasksByEmail(string employeeEmailId);

    }
}
