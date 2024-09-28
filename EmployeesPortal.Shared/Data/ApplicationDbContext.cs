using EmployeesPortal.Models;
using EmployeesPortal.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeesPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<VacationRequestStatus> VacationRequestStatuses { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleStatus> scheduleStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.VacationRequests)
                .WithOne(v => v.Employee)
                .HasForeignKey(v => v.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Schedules)
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.EmployeeID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Meeting)
                .WithMany(m => m.Schedules)
                .HasForeignKey(s => s.MeetingID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.VacationRequest)
                .WithMany(v => v.Schedules)
                .HasForeignKey(s => s.VactionRequestID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Status)
                .WithMany(st => st.Schedules)
                .HasForeignKey(s => s.ScheduleID);

            modelBuilder.Entity<Schedule>()
                .Property(s => s.ScheduleID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MeetingParticipant>()
                .HasKey(mp => new { mp.MeetingID, mp.EmployeeID });

            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.Meeting)
                .WithMany(m => m.MeetingParticipants)
                .HasForeignKey(mp => mp.MeetingID);


            modelBuilder.Entity<ScheduleStatus>().HasData(
                new ScheduleStatus { StatusID = 1, StatusName = "VactionRequest" },
                new ScheduleStatus { StatusID = 2, StatusName = "Meeting" },
                new ScheduleStatus { StatusID = 3, StatusName = "Task" }
            );

            modelBuilder.Entity<VacationRequestStatus>().HasData(
                new VacationRequestStatus { StatusID = 1, StatusName = "Pending" },
                new VacationRequestStatus { StatusID = 2, StatusName = "Approved" },
                new VacationRequestStatus { StatusID = 3, StatusName = "Rejected" },
                new VacationRequestStatus { StatusID = 4, StatusName = "Escalated" },
                new VacationRequestStatus { StatusID = 5, StatusName = "Cancelled" },
                new VacationRequestStatus { StatusID = 6, StatusName = "Completed" }
            );
        }
    }
}
