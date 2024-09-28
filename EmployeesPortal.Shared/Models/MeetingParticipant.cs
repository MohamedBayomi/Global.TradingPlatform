namespace EmployeesPortal.Models
{
    public class MeetingParticipant
    {
        public int MeetingID { get; set; }
        public int EmployeeID { get; set; }
        public bool IsOff {  get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual Employee Employee { get; set; }
    }

}
