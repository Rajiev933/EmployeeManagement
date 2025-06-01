using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByUsernameAsync(string username)
        {
            var employees = await _context.Employees
        .FromSqlRaw("EXEC GetEmployeeByUsername @Username = {0}", username)
        .ToListAsync();

            return employees.FirstOrDefault();
        }
    }
}
