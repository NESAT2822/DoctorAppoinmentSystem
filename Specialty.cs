namespace DoctorAppoinmentSystem.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<DoctorProfile> Doctors { get; set; } = new List<DoctorProfile>();
    }
}
