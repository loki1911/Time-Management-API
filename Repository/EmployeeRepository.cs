using Dapper;
using System.Data;
using System.Threading.Tasks;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Repository
{
    public class EmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Retrieves an employee by email (password hash is included)
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            const string query = "SELECT EmployeeID, EmployeeEmailID, EmployeeName, Password FROM Employee WHERE EmployeeEmailID = @Email";

            try
            {
                var employee = await _dbConnection.QuerySingleOrDefaultAsync<Employee>(query, new { Email = email });

                // If the employee doesn't exist, null will be returned
                return employee;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception("Error retrieving employee data.", ex);
            }
        }

        // Inserts a new employee into the database and returns the newly generated EmployeeID
        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            const string query = @"
            INSERT INTO Employee (EmployeeEmailID, EmployeeName, Password)
            VALUES (@EmployeeEmailID, @EmployeeName, @Password);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

            try
            {
                // The password must already be hashed when calling this method, so we don't hash it again here.
                return await _dbConnection.QuerySingleAsync<int>(query, employee);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception("Error inserting employee data.", ex);
            }
        }
    }
}
