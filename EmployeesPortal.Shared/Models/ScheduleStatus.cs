using EmployeesPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesPortal.Shared.Models
{
    public class ScheduleStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }

        // Navigation property
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
