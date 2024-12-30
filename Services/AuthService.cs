using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Repository;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TimeMangementSystemAPI.Services
{
    public class AuthService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(EmployeeRepository employeeRepository, ILogger<AuthService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Employee> RegisterAsync(Employee employee)
        {
            // Check if employee already exists
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(employee.EmployeeEmailID);
            if (existingEmployee != null)
            {
                _logger.LogWarning("Registration failed: Email {Email} already exists.", employee.EmployeeEmailID);
                throw new ArgumentException("Email already exists.");
            }

            // Log hashing password for debugging
            _logger.LogInformation("Hashing password for email: {Email}", employee.EmployeeEmailID);

            // Hash the password before storing it (with salt)
            employee.Password = HashPassword(employee.Password);

            var employeeId = await _employeeRepository.CreateEmployeeAsync(employee);
            employee.EmployeeID = employeeId;

            // Log successful registration
            _logger.LogInformation("Employee registered successfully with ID: {EmployeeId}", employeeId);

            return employee;
        }

        public async Task<Employee> LoginAsync(string email, string password)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
            if (employee == null)
            {
                _logger.LogWarning("Login failed: Invalid email {Email}.", email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            _logger.LogInformation("Verifying password for email: {Email}", email);

            if (!VerifyPassword(password, employee.Password))
            {
                _logger.LogWarning("Login failed: Invalid password for email {Email}.", email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            _logger.LogInformation("Login successful for email: {Email}", email);

            return employee;
        }

        // Hash password using PBKDF2 with salt
        private string HashPassword(string password)
        {
            var salt = new byte[128 / 8];  // Salt length = 16 bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hash}";
        }

        // Verify the password against the stored hash and salt
        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid password format.");
            }

            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = parts[1];

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return storedPasswordHash == hash;
        }
    }
}
