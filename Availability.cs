namespace DoctorAppoinmentSystem.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public int DoctorProfileId { get; set; }
        public required DoctorProfile Doctor { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
