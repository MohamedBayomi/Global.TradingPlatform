using EmployeesPortal.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesPortal.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Description { get; set; }
        public int? MeetingID { get; set; }
        public int? VactionRequestID { get; set; }
        public int ScheduleStatusId { get; set; }
        public bool IsOff {  get; set; }


        public virtual Employee Employee { get; set; }
        public virtual ScheduleStatus Status { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual VacationRequest VacationRequest { get; set; }

    }

}
