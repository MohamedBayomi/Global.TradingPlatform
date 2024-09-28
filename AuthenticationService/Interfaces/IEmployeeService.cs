using EmployeesPortal.Models;

namespace Authentication_Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeIdByAppId(string appId);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee entity);
        Task<Employee> GetByIdAsync(int id);
        Employee Update(Employee entity);
        Task DeleteAsync(Employee entity);
    }
}
