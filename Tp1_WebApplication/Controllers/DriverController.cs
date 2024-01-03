using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tp1_CoreApplication;
using Tp1_WebApplication.ViewModels;

namespace Tp1_WebApplication.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly Tp1_Context _context;

        public DriverController(Tp1_Context context)
        {
            this._context = context;
        }

        //Get/ListDrivers
        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Manage(int BranchId)
        {
            ViewBag.BranchId = BranchId;
            var drivers = _context.Drivers
                .GroupJoin(_context.Rentals,
                driver => driver.Id,
                reservation => reservation.Id,
                (driver, rentals) => new DriverManagerViewModel
                {
                    FullName = driver.FirstName + " " + driver.LastName,
                    DateLastReservation = rentals.OrderByDescending(r => r.ClosingDate)
                                        .Select(r => r.ClosingDate)
                                        .FirstOrDefault(),
                    ReservationCount = rentals.Count(),
                    id = driver.Id,
                    BranchId = BranchId
                });
            TempData["ErrorMessage"] = "The request has failed.";
            return View(drivers);
        }

        //Get/Driver/DriverId
        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Detail(int id, int BranchId)
        {
            var driver = _context.Drivers.Include(d => d.rental).FirstOrDefault(d => d.Id == id);
            ViewBag.BranchId = BranchId;
            if (driver is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var vm = new DriverDetailViewModel()
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                DriverLicense = driver.DriverLicense,
                BranchId = BranchId
            };

            var rentals = _context.Rentals.Where(r => r.Id == id).ToList();
            vm.Rentals = rentals;
            TempData["ErrorMessage"] = "The request has failed.";
            return View(vm);
        }
    }
}
