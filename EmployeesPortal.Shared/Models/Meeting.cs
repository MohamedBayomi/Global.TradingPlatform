using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesPortal.Models
{
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Nullable, since not all meetings may be virtual
        public string? VirtualMeeting { get; set; }

        // Navigation property for participants of the meeting
        public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; } = new List<MeetingParticipant>();

        // Navigation property for schedules related to the meeting
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>(); // In case schedules are linked to meetings
    }
}
