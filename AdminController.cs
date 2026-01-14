using DoctorAppoinmentSystem.Data;
using DoctorAppoinmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppoinmentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db) => _db = db;

        public IActionResult PendingAppointments()
        {
            var pending = _db.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.Specialty)
                .Include(a => a.Patient)
                .Where(a => a.Status == AppointmentStatus.Pending)
                .ToList();
            return View(pending);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var appt = await _db.Appointments.FindAsync(id);
            if (appt != null)
            {
                appt.Status = AppointmentStatus.Approved;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PendingAppointments");
        }

        public async Task<IActionResult> Reject(int id)
        {
            var appt = await _db.Appointments.FindAsync(id);
            if (appt != null)
            {
                appt.Status = AppointmentStatus.Rejected;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PendingAppointments");

        }

    }


}
