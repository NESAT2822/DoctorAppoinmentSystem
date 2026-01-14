
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppoinmentSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;   // default "" দিলে null হবে না

        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string Address { get; set; } = string.Empty;   // null-safe

        public bool Enabled { get; set; } = true;  // default true/false

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }   // int by default 0, কিন্তু validation handle করবে
    }
}
