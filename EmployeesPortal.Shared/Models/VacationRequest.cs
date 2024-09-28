using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesPortal.Models
{
    public class VacationRequest
    {
        [Key]
        public int RequestID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartDateOfVacation { get; set; }
        public DateTime EndDateOfVacation { get; set; }
        public int StatusID { get; set; }
        public string Reason { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EscalationAt { get; set; }
        public int ManagerIdOfRequest { get; set; }
        public int Escalation { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual Employee Employee { get; set; }
        public virtual VacationRequestStatus Status { get; set; }

        // One VacationRequest can have multiple Schedules
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
