using HospitalManagement.Auth;
using HospitalManagement.Data;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    // STRICT SECURITY: Only logged-in Patients can access this entire section
    [Authorize(Roles = HospitalRoles.Patient)]
    public class PatientPortalController : Controller
    {
        private readonly HospitalDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PatientPortalController(HospitalDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // --- Helper to safely get the logged-in patient's LinkedId ---
        private async Task<int?> GetPatientLinkedIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.LinkedId;
        }

        // GET: /PatientPortal/Dashboard
        // Update PatientPortalController.cs Dashboard method
        public async Task<IActionResult> Dashboard()
        {
            var linkedId = await GetPatientLinkedIdAsync();
            if (linkedId == null)
            {
                return Content("Error: Your account is not linked to a patient profile.");
            }

            var patient = await _context.Patients.FindAsync(linkedId);

            // 🚨 FIX: Prevent a fatal crash if the record was deleted by an admin
            if (patient == null)
            {
                return Content("Error: Your medical profile has been archived or removed from the system. Please contact reception.");
            }

            ViewBag.UnpaidBillsCount = await _context.Bills.CountAsync(b => b.PatientId == linkedId && b.PaymentStatus != PaymentStatus.Paid);
            ViewBag.TotalVisits = await _context.MedicalRecords.CountAsync(m => m.PatientId == linkedId);

            return View(patient);
        }

        // GET: /PatientPortal/MyRecords
        public async Task<IActionResult> MyRecords()
        {
            var linkedId = await GetPatientLinkedIdAsync();
            if (linkedId == null) return NotFound();

            var records = await _context.MedicalRecords
                .Include(m => m.Doctor)
                .Include(m => m.Prescriptions)
                .Where(m => m.PatientId == linkedId)
                .OrderByDescending(m => m.VisitDate)
                .AsNoTracking()
                .ToListAsync();

            return View(records);
        }

        // GET: /PatientPortal/MyBills
        public async Task<IActionResult> MyBills()
        {
            var linkedId = await GetPatientLinkedIdAsync();
            if (linkedId == null) return NotFound();

            var bills = await _context.Bills
                .Where(b => b.PatientId == linkedId)
                .OrderByDescending(b => b.BillDate)
                .AsNoTracking()
                .ToListAsync();

            return View(bills);
        }

        // GET: /PatientPortal/MyReports
        public async Task<IActionResult> MyReports()
        {
            var linkedId = await GetPatientLinkedIdAsync();
            if (linkedId == null) return NotFound();

            var reports = await _context.LabTests
                .Include(l => l.MedicalRecord)
                    .ThenInclude(m => m.Doctor) // Shows which doctor ordered it
                .Where(l => l.PatientId == linkedId)
                .OrderByDescending(l => l.TestDate)
                .AsNoTracking()
                .ToListAsync();

            return View(reports);
        }

        // GET: /PatientPortal/DownloadRx/5
        public async Task<IActionResult> DownloadRx(int id)
        {
            var linkedId = await GetPatientLinkedIdAsync();
            if (linkedId == null) return Unauthorized();

            // 🚨 SECURITY: Fetch the prescription, but strictly ensure it belongs to THIS logged-in patient!
            var prescription = await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.MedicalRecord)
                    .ThenInclude(m => m.Patient)
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medicine)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id && m.MedicalRecord.PatientId == linkedId);

            if (prescription == null)
            {
                return Content("Error: Prescription not found, or you do not have permission to view this document.");
            }

            return View("~/Views/Prescriptions/Print.cshtml", prescription);
        }
    }
}