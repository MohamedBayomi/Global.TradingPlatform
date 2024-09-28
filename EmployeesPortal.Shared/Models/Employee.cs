using EmployeesPortal.Models;
using EmployeesPortal.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    public int EmployeeID { get; set; }
    [Required]
    public string ApplicationUserId { get; set; }
    [ForeignKey(nameof(ApplicationUserId))]
    public virtual ApplicationUser ApplicationUser { get; set; }

    public int? ManagerID { get; set; } // Nullable for top-level managers
    public int DepartmentID { get; set; }

    public virtual Employee Manager { get; set; } // Navigation property
    public virtual Department Department { get; set; } // Navigation property
    public virtual ICollection<VacationRequest> VacationRequests { get; set; }
    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }
    public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
}
