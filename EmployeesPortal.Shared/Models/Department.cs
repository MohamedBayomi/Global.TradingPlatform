namespace EmployeesPortal.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        // Navigation property
        public virtual ICollection<Employee> Employees { get; set; }
    }

}
