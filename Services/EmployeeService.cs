using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TimeMangementSystemAPI.DTOs;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            string query = @"
            SELECT 
                e.EmployeeID,
                e.EmployeeEmailId,
                e.EmployeeName,
                e.EmployeeDesignation AS Designation,
                d.DepartmentName,
                r.RoleName
            FROM Employee e
            INNER JOIN Department d ON e.DepartmentID = d.DepartmentID
            INNER JOIN Role r ON e.RoleId = r.RoleId";

            return _dbConnection.Query<EmployeeDto>(query);
        }

        public EmployeeDto GetEmployeeByEmail(string emailId)
        {
            string query = @"
            SELECT 
                e.EmployeeEmailId,
                e.EmployeeName,
                e.EmployeeDesignation AS Designation,
                d.DepartmentName,
                r.RoleName
            FROM Employee e
            INNER JOIN Department d ON e.DepartmentID = d.DepartmentID
            INNER JOIN Role r ON e.RoleId = r.RoleId
            WHERE e.EmployeeEmailId = @EmailId";

            return _dbConnection.QueryFirstOrDefault<EmployeeDto>(query, new { EmailId = emailId });
        }



        public int SaveEmployee(EmployeeData employeeData)
        {
           
                string query = @"
    INSERT INTO Employee ( EmployeeEmailId, EmployeeName, EmployeeDesignation, DepartmentID, RoleId)
    VALUES ( @EmployeeEmailId, @EmployeeName, @EmployeeDesignation, @DepartmentID,  @RoleId);
    ";

                return _dbConnection.ExecuteScalar<int>(query, employeeData);
            

        }

        public int UpdateEmployee(EmployeeData employeeData)
        {
            
                string query = @"
    UPDATE Employee
    SET 
        EmployeeEmailId = @EmployeeEmailId,
        EmployeeName = @EmployeeName,
        EmployeeDesignation = @EmployeeDesignation,
        DepartmentID = @DepartmentID,
       
        RoleId = @RoleId
    WHERE EmployeeID = @EmployeeID;";

                return _dbConnection.Execute(query, employeeData);
            }
        }
 }

