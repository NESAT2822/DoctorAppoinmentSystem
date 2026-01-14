
namespace DoctorAppoinmentSystem.Models
{
    public class DoctorProfile
    {
        public int Id { get; set; }
        public required string ApplicationUserId { get; set; }
        public required ApplicationUser ApplicationUser { get; set; }

        public int SpecialtyId { get; set; }
        public required Specialty Specialty { get; set; }

        public string Qualification { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;

        public required ICollection<Availability> Availabilities { get; set; }
        public required ICollection<Appointment> Appointments { get; set; }

        public static implicit operator DoctorProfile(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
