namespace DoctorAppoinmentSystem.Models
{
    public enum AppointmentStatus { Pending, Approved, Rejected, Cancelled }


    public class Appointment
    {
        public int Id { get; set; }

        public string PatientId { get; set; } = string.Empty;
        public ApplicationUser Patient { get; set; } = null!;

        public int DoctorProfileId { get; set; }
        public DoctorProfile Doctor { get; set; } = null!;

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

