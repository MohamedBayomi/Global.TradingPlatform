using System.ComponentModel.DataAnnotations;

namespace EmployeesPortal.Models
{
    public class AttendanceRecord
    {
        [Key]
        public int AttendanceRecordID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; } // Nullable for current day's check-in

        public virtual Employee Employee { get; set; }
    }

}
