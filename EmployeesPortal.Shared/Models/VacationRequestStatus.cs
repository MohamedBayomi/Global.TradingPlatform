using System.ComponentModel.DataAnnotations;

namespace EmployeesPortal.Models
{
    public class VacationRequestStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }

        // Navigation property
        public virtual ICollection<VacationRequest> VacationRequests { get; set; }
    }

}
