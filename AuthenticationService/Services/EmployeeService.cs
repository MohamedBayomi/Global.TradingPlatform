using Authentication_Service.Interfaces;
using EmployeesPortal.Data;
using EmployeesPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new employee asynchronously
        public async Task<Employee> AddAsync(Employee entity)
        {
            _context.Employees.Add(entity);     
            await _context.SaveChangesAsync();  
            return entity;   
        }

        // Delete an employee asynchronously
        public async Task DeleteAsync(Employee entity)
        {
            _context.Employees.Remove(entity);  
            await _context.SaveChangesAsync();  
        }

        // Get all employees asynchronously
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync(); 
        }

        // Get an employee by their ID asynchronously
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id); 
        }

        public async Task<Employee> GetEmployeeIdByAppId(string appId)
         => await _context.Employees.SingleOrDefaultAsync(t => t.ApplicationUserId == appId);

        // Update an employee synchronously
        public Employee Update(Employee entity)
        {
            _context.Employees.Update(entity);  
            _context.SaveChanges();  
            return entity;  
        }
    }
}
