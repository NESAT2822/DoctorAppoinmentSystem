using DoctorAppoinmentSystem.Data;
using DoctorAppoinmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Patient")]
public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public AppointmentsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // GET: Create Appointment Page
    public IActionResult Create()
    {
        ViewBag.Doctors = _db.DoctorProfiles.Include(d => d.Specialty).ToList();
        return View();
    }

    // POST: Create Appointment
    [HttpPost]
    public async Task<IActionResult> Create(int doctorId, DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            // Check for conflict
            var conflict = await _db.Appointments.AnyAsync(a =>
                a.DoctorProfileId == doctorId && a.Date == date &&
                (a.StartTime < endTime && a.EndTime > startTime));

            if (conflict)
            {
                ModelState.AddModelError("", "Time slot not available");
                ViewBag.Doctors = _db.DoctorProfiles.Include(d => d.Specialty).ToList();
                return View();
            }

            // Create new appointment
            var appointment = new Appointment()
            {
                PatientId = user.Id,
                DoctorProfileId = doctorId,
                Date = date,
                StartTime = startTime,
                EndTime = endTime,
                Status = AppointmentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();

            return RedirectToAction("MyAppointments");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            ViewBag.Doctors = _db.DoctorProfiles.Include(d => d.Specialty).ToList();
            return View();
        }
    }

    // GET: My Appointments
    public async Task<IActionResult> MyAppointments()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var list = await _db.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.Specialty)
                .Where(a => a.PatientId == user.Id)
                .ToListAsync();

            return View(list);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Could not load appointments: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }
}
