using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Tp1_CoreApplication;
using Tp1_CoreApplication.Domain;
using Tp1_WebApplication.ViewModels;

namespace Tp1_WebApplication.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly Tp1_Context _context;
        private readonly UserManager<User> userManager;

        public CarController(Tp1_Context context)
        {
            this._context = context;
        }

        //Get/Car/Manage
        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Manage(int BranchId)
        {
            var cars = _context.Cars
                .Where(car => car.BranchId == BranchId)
                .Select(car => new CarManageViewModel
                {
                    BranchId = car.BranchId,
                    ID = car.ID,
                    CarName = car.CarName,
                    Registration = car.Registration,
                    Brand = car.Brand,
                    Model = car.Model,
                    Color = car.Color,
                    Year = car.Year,
                    Activated = car.Activated,
                    Status = car.status,
                    Availability = car.Available
                })
                .OrderByDescending(car => car.Availability)
                .ThenBy(car => car.ID) // Add a secondary sorting criterion
                .ToList();

            TempData["ErrorMessage"] = "Request failed";

            return View(cars);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult Manage(string availability, int BranchId)
        {
            bool bAvailability;

            if (availability == "Available")
            {
                bAvailability = true;

                var Cars = _context.Cars.Select(Car => new CarManageViewModel
                {
                    BranchId = Car.BranchId,
                    ID = Car.ID,
                    CarName = Car.CarName,
                    Registration = Car.Registration,
                    Brand = Car.Brand,
                    Activated = Car.Activated,
                    Model = Car.Model,
                    Color = Car.Color,
                    Year = Car.Year,
                    Status = Car.status,
                    Availability = Car.Available
                }).ToList().Where(x => x.Availability == bAvailability && x.BranchId == BranchId);
                TempData["ErrorMessage"] = "Request failed";

                return View(Cars);
            }

            else if (availability == "Not available")
            {
                bAvailability = false;

                var Cars = _context.Cars.Select(Car => new CarManageViewModel
                {
                    BranchId = Car.BranchId,
                    ID = Car.ID,
                    CarName = Car.CarName,
                    Registration = Car.Registration,
                    Brand = Car.Brand,
                    Activated = Car.Activated,
                    Model = Car.Model,
                    Color = Car.Color,
                    Year = Car.Year,
                    Status = Car.status,
                    Availability = Car.Available
                }).ToList().Where(x => x.Availability == bAvailability && x.BranchId == BranchId);
                TempData["ErrorMessage"] = "Request failed";
                return View(Cars);
            }
            else
            {
                var Cars = _context.Cars.Select(Car => new CarManageViewModel
                {
                    BranchId = Car.BranchId,
                    ID = Car.ID,
                    CarName = Car.CarName,
                    Registration = Car.Registration,
                    Brand = Car.Brand,
                    Activated = Car.Activated,
                    Model = Car.Model,
                    Color = Car.Color,
                    Year = Car.Year,
                    Status = Car.status,
                    Availability = Car.Available
                }).ToList().Where(x => x.BranchId == BranchId);
                TempData["ErrorMessage"] = "Request failed";
                return View(Cars);
            }
        }

        [Authorize(Roles = "Administrator,Gérant")]
        public IActionResult Create(int BranchId)
        {

            ViewBag.BranchId = BranchId;

            var model = new CarCreateViewModel
            {
                ID = 0,
                BranchId = BranchId,
            };


            return View(model);
        }

        [Authorize(Roles = "Administrator,Gérant")]
        [HttpPost]
        public IActionResult Create(CarCreateViewModel vm, int BranchId)
        {
            ViewBag.BranchId = BranchId;
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = BranchId;
                return View(vm);
            }

            var car = new Car
            {
                Model = vm.Model,
                Brand = vm.Brand,
                CarName = vm.CarName,
                Color = vm.Color,
                Year = vm.Year,
                Registration = vm.Registration,
                SerialNumber = vm.SerialNumber,
                Kilometers = vm.Kilometers,
                EstimatedValue = vm.EstimatedValue,
                State = vm.State,
                status = vm.status,
                BranchId = BranchId
            };
            _context.Cars.Add(car);
            _context.SaveChanges();


            if (vm.NoteComment != null)
            {
                var note = new Note()
                {
                    NoteContent = vm.NoteComment,
                    carId = car.ID,
                    NotePostDate = DateTime.Now,

                };
                _context.Notes.Add(note);
                _context.SaveChanges();
            }


            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Car was succefully created!";
            return RedirectToAction(nameof(Manage), new { BranchId = BranchId });
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Details(int id, int BranchId)
        {
            var car = _context.Cars.Find(id);
            ViewBag.BranchId = BranchId;
            if (car == null)
            {
                ViewBag.BranchId = BranchId;
                return NotFound();
            }

            var note = _context.Notes.Where(n => n.carId == id).ToList();

            var vm = new CarEditViewModel
            {
                Notes = note.ToList(),
                CarName = car.CarName,
                Registration = car.Registration,
                Color = car.Color,
                Kilometers = car.Kilometers,
                State = car.State,
                Available = car.Available,
                status = car.status,
                EstimatedValue = car.EstimatedValue,
                BranchId = car.BranchId,
            };

            return View(vm);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Edit(int id, int BranchId)
        {
            var car = _context.Cars.Include(c => c.Rental)
                .Include(c => c.notes).FirstOrDefault(c => c.ID == id);


            ViewBag.BranchId = BranchId;
            if (car == null)
            {
                ViewBag.BranchId = BranchId;
                return NotFound();
            }

            var vm = new CarEditViewModel
            {
                ID = car.ID,
                CarName = car.CarName,
                Registration = car.Registration,
                Color = car.Color,
                Kilometers = car.Kilometers,
                State = car.State,
                Available = car.Available,
                status = car.status,
                Rentals = car.Rental?.Where(y => y.CarId == car.ID).ToList(),
                Notes = car.notes?.Where(x => x.carId == car.ID).ToList(),
                EstimatedValue = car.EstimatedValue,
                BranchId = BranchId
            };

            return View(vm);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult Edit(int id, CarEditViewModel vm, int BranchId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = BranchId;
                return View(vm);
            }

            var toEdit = _context.Cars.Find(id);

            if (toEdit is null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            toEdit.status = vm.status;
            toEdit.State = vm.State;
            toEdit.Available = vm.Available;
            toEdit.CarName = vm.CarName;
            toEdit.Color = vm.Color;
            toEdit.Registration = vm.Registration;
            toEdit.EstimatedValue = vm.EstimatedValue;
            toEdit.Kilometers = vm.Kilometers;
            toEdit.BranchId = BranchId;

            _context.SaveChanges();
            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Modifications succeeded";
            return RedirectToAction(nameof(Manage), new { BranchId = toEdit.BranchId });
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public async Task<ActionResult> DeactivateCar(int ID, int BranchId)
        {
            var car = _context.Cars.SingleOrDefault(s => s.ID == ID);

            if (car != null)
            {
                TempData["SuccessMessage"] = "Disabling worked";
                car.status = Tp1_CoreApplication.Enums.Status.Inactive;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
            }
            else
            {
                TempData["ErrorMessage"] = "Request failed";
                return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
            }
        }

        [Authorize(Roles = "Administrator, Gérant")]
        public async Task<ActionResult> ActivateCar(int ID, int BranchId)
        {
            var car = _context.Cars.SingleOrDefault(s => s.ID == ID);

            if (car != null)
            {
                TempData["SuccessMessage"] = "Activation succeeded";
                car.status = Tp1_CoreApplication.Enums.Status.Active;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
            }
            else
            {
                TempData["ErrorMessage"] = "Request failed";
                return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
            }
        }

        [Authorize(Roles = "Administrator,Gérant")]
        public async Task<ActionResult> ArchiveCar(int ID, int BranchId)
        {
            var car = _context.Cars.SingleOrDefault(s => s.ID == ID);

            if (car != null)
            {
                if (car.status == Tp1_CoreApplication.Enums.Status.Inactive)
                {
                    TempData["SuccessMessage"] = "Success, the car is archived.";
                    car.status = Tp1_CoreApplication.Enums.Status.Archived;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Request failed";
                return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
            }

            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Success, the car is archived.";
            return RedirectToAction(nameof(Manage), new { BranchId = car.BranchId });
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Remove(int id)
        {
            var toRemove = _context.Cars.Find(id);

            if (toRemove is null)
            {
                // throw new ArgumentOutOfRangeException(nameof(id));
            }

            _context.Cars.Remove(toRemove);
            _context.SaveChanges();
            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Deletion succeeded";

            return RedirectToAction(nameof(Manage), new { BranchId = toRemove.BranchId });
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult AddNote(int id, int BranchId)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                ViewBag.BranchId = BranchId;
                return NotFound();
            }

            ViewBag.id = id;
            ViewBag.BranchId = BranchId;


            var vm = new SimplifiedCommentViewModel();

            return View(vm);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult AddNote(int id, SimplifiedCommentViewModel vm, int BranchId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = BranchId;
                return View();
            }

            var toEdit = _context.Cars.Include(r => r.notes).FirstOrDefault(r => r.ID == vm.carId);

            //var rentalId = _context.Rentals.Where(x => x.CarId == vm.carId).FirstOrDefault();
            //int rentalIdInt = rentalId.Id;

            var note = new Note
            {
                
                carId = toEdit.ID,
                NoteContent = vm.Content,
                NotePostDate = DateTime.Now,
            };

            _context.Notes.Add(note);
            _context.SaveChanges();
            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Succefully add a note";
            ViewBag.BranchId = BranchId;

            return RedirectToAction(nameof(Manage), new { BranchId = BranchId });
        }
    }
}
