using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tp1_CoreApplication;
using Tp1_CoreApplication.Domain;
using Tp1_WebApplication.ViewModels;

namespace Tp1_WebApplication.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly Tp1_Context _context;

        public RentalController(Tp1_Context context)
        {
            this._context = context;
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Manage(int BranchId)
        {
            List<string> branchNames = _context.Branches.Where(x => x.ActiveBranch == true)
                                       .Select(r => r.Name).ToList();

            ViewBag.BranchId = BranchId;
            var rentals = _context.Rentals
                .Select(rentals => new RentalsManageViewModel()
                {
                    Id = rentals.Id,
                    note = rentals.notes.ToList(),
                    //Driver
                    FullName = rentals.drivers.Select(x => x.FirstName).First() + " " +
                                rentals.drivers.Select(x => x.LastName).First(),
                    Address = rentals.drivers.Select(y => y.Address).First(),
                    Email = rentals.drivers.Select(x => x.Email).First(),
                    PhoneNumber = rentals.drivers.Select(x => x.PhoneNumber).First(),
                    //Branch
                    branchName = rentals.car.branch.Name,
                    BranchId = rentals.car.BranchId,
                    //Car
                    CarId = rentals.CarId,
                    CarName = rentals.car.CarName,
                    closed = rentals.Closed,
                    SerialNumber = rentals.car.SerialNumber,
                    Registration = rentals.car.Registration,
                    State = rentals.car.State,
                    Brand = rentals.car.Brand,
                    Model = rentals.car.Model,
                    Year = rentals.car.Year,
                    Color = rentals.car.Color,
                    Kilometers = rentals.car.Kilometers,
                    EstimatedValue = rentals.car.EstimatedValue,
                    rentalId = rentals.Id,
                    //Rental
                    OpeningDateTime = rentals.OpeningDate.ToString("yyyy-MM-dd") + " " +
                                        rentals.OpeningDate.ToString("hh:mm:ss tt"),
                    ClosingDateTime = rentals.ClosingDate.ToString("yyyy-MM-dd") + " " +
                                        rentals.ClosingDate.ToString("hh:mm:ss tt"),
                    branchList = branchNames,
                }).Where(c => c.BranchId == BranchId);

            TempData["ErrorMessage"] = "The request has failed.";
            return View(rentals);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult Manage(string branchName, int BranchId)
        {
            List<string> branchNames = _context.Branches.Where(x => x.ActiveBranch == true)
                                       .Select(r => r.Name).ToList();
            BranchId = _context.Branches.Where(x => x.Name == branchName)
                                       .Select(r => r.Id).FirstOrDefault();

            ViewBag.BranchId = BranchId;

            var rentals = _context.Rentals
                .Include(x => x.car)
                .ThenInclude(x => x.branch)
                .Where(r => r.car.branch.Name == branchName)
                .Select(rentals => new RentalsManageViewModel()
                {
                    Id = rentals.Id,
                    note = rentals.notes.ToList(),
                    //Driver
                    FullName = rentals.drivers.Select(x => x.FirstName).First() + " " +
                                rentals.drivers.Select(x => x.LastName).First(),
                    Address = rentals.drivers.Select(y => y.Address).First(),
                    Email = rentals.drivers.Select(x => x.Email).First(),
                    PhoneNumber = rentals.drivers.Select(x => x.PhoneNumber).First(),
                    //Branch
                    branchName = rentals.car.branch.Name,
                    BranchId = rentals.car.BranchId,
                    //Car
                    CarId = rentals.CarId,
                    CarName = rentals.car.CarName,
                    closed = rentals.Closed,
                    SerialNumber = rentals.car.SerialNumber,
                    Registration = rentals.car.Registration,
                    State = rentals.car.State,
                    Brand = rentals.car.Brand,
                    Model = rentals.car.Model,
                    Year = rentals.car.Year,
                    Color = rentals.car.Color,
                    Kilometers = rentals.car.Kilometers,
                    EstimatedValue = rentals.car.EstimatedValue,
                    rentalId = rentals.Id,
                    //Rental
                    OpeningDateTime = rentals.OpeningDate.ToString("yyyy-MM-dd") + " " +
                                        rentals.OpeningDate.ToString("hh:mm:ss tt"),
                    ClosingDateTime = rentals.ClosingDate.ToString("yyyy-MM-dd") + " " +
                                        rentals.ClosingDate.ToString("hh:mm:ss tt"),
                    branchList = branchNames,
                }).Where(c => c.BranchId == BranchId);

            if (rentals.IsNullOrEmpty())
            {
                var rentals2 = _context.Rentals
                    .Select(rental =>
                    new RentalsManageViewModel()
                    {
                        BranchId = BranchId,
                        branchName = branchName,
                        branchList = branchNames,
                    });

                return View(rentals2);
            }
            return View(rentals);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Create(int id, int BranchId)
        {
            ViewBag.CarId = id;
            ViewBag.BranchId = BranchId;

            var model = new RentalCreateViewModel();

            return View(model);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult Create(RentalCreateViewModel vm, int BranchId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = BranchId;
                return View(vm);
            }

            int carId = vm.CarId;

            string formattedOpeningDate = vm.OpeningDate.ToString("yyyy-MM-dd");
            string formattedOpeningTime = vm.OpeningDate.ToString("hh:mm:ss tt");
            string formattedClosingDate = vm.ClosingDate.ToString("yyyy-MM-dd");
            string formattedClosingTime = vm.ClosingDate.ToString("hh:mm:ss tt");

            var changeAvailability = _context.Cars.FirstOrDefault(c => c.ID == carId);

            changeAvailability.Available = false;

            var newRental = new Rental()
            {
                CarId = carId,
                OpeningDate = DateTime.Parse(formattedOpeningDate),
                ClosingDate = DateTime.Parse(formattedClosingDate)
            };

            newRental.drivers.Add(new Driver()
            {
                FirstName = vm.FirstName.Trim(),
                LastName = vm.LastName.Trim(),
                Address = vm.StreetAddress.Trim(),
                PhoneNumber = vm.PhoneNumber.Trim(),
                Email = vm.Email.Trim(),
                DriverLicense = vm.DriverLicense.Trim(),
                IdentityProof = vm.IdentityProof,
                AssuranceProof = vm.AssuranceProof,
            });

            _context.Rentals.Add(newRental);
            _context.SaveChanges();

            if (vm.NoteContent != null)
            {
                newRental.notes.Add(new Note()
                {
                    rentalId = newRental.Id,
                    carId = carId,
                    NoteContent = vm.NoteContent.Trim(),
                    NotePostDate = DateTime.Now,
                });
                _context.Rentals.Update(newRental);
                _context.SaveChanges();
            }

            TempData["ErrorMessage"] = "The request has failed.";
            TempData["SuccessMessage"] = "Rental creation succeeded.";
            return RedirectToAction(nameof(Manage), new { BranchId = BranchId });
        }


        //Get/Edit/id
        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Edit(int id, int BranchId)
        {
            var toEdit = _context.Rentals.Find(id);

            if (toEdit is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var note = _context.Notes.Where(x => x.rentalId == id).ToList();

            var vm = new RentalEditViewModel()
            {
                //Car
                note = note.ToList(),
                CarId = toEdit.CarId,
                CarName = toEdit.car.CarName,
                SerialNumber = toEdit.car.SerialNumber,
                Registration = toEdit.car.Registration,
                State = toEdit.car.State,
                Brand = toEdit.car.Brand,
                Model = toEdit.car.Model,
                Year = toEdit.car.Year,
                Color = toEdit.car.Color,
                Kilometers = toEdit.car.Kilometers,
                EstimatedValue = toEdit.car.EstimatedValue,
                BranchId = BranchId,
                //Rental
                //OpeningDate = toEdit.OpeningDate,
                //ClosingDate = toEdit.ClosingDate,
                //OpeningTime = toEdit.OpeningTime,
                //ClosingTime = toEdit.ClosingTime,
                //Driver
                FirstName = toEdit.drivers.Select(x => x.FirstName).First(),
                LastName = toEdit.drivers.Select(x => x.LastName).First(),
                StreetAddress = toEdit.drivers.Select(y => y.Address).First(),
                Email = toEdit.drivers.Select(x => x.Email).First(),
                PhoneNumber = toEdit.drivers.Select(x => x.PhoneNumber).First(),
                AssuranceProof = toEdit.drivers.Select(x => x.AssuranceProof).First(),
                IdentityProof = toEdit.drivers.Select(x => x.IdentityProof).First(),
                DriverLicense = toEdit.drivers.Select(x => x.DriverLicense).First()
            };
            TempData["ErrorMessage"] = "La demande a échoué";
            TempData["SuccessMessage"] = "La modification à fonctionner.";
            ViewBag.Id = id;
            ViewBag.BranchId = BranchId;
            return View(vm);
        }


        //Post/Edit/id
        //Remove id
        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Remove(int id, int BranchId)
        {
            var toRemove = _context.Rentals.Find(id);

            if (toRemove is null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            _context.Rentals.Remove(toRemove);
            _context.SaveChanges();
            TempData["ErrorMessage"] = "La demande a échoué";
            TempData["SuccessMessage"] = "La suppression à fonctionner.";
            ViewBag.BranchId = BranchId;
            return RedirectToAction(nameof(Manage));
        }




        //Get/FormulaireRental/RentalId
        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Detail(int Id, int BranchId)
        {
            var toEdit = _context.Rentals.Include(r => r.drivers).FirstOrDefault(r => r.Id == Id);

            if (toEdit is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(Id));
            }

            Car car = _context.Cars.Find(toEdit.CarId);
            var note = _context.Notes.Where(x => x.rentalId == Id).ToList();

            var vm = new RentalEditViewModel()
            {
                BranchId = car.BranchId,
                note = note.ToList(),
                //Car
                CarId = car.ID,
                CarName = car.CarName,
                SerialNumber = car.SerialNumber,
                Registration = car.Registration,
                State = car.State,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Kilometers = car.Kilometers,
                EstimatedValue = car.EstimatedValue,
                //Rental
                OpeningDate = toEdit.OpeningDate.ToString(),
                ClosingDate = toEdit.ClosingDate.ToString(),
                //Driver
                FirstName = toEdit.drivers.Select(x => x.FirstName).First(),
                LastName = toEdit.drivers.Select(x => x.LastName).First(),
                StreetAddress = toEdit.drivers.Select(y => y.Address).First(),
                Email = toEdit.drivers.Select(x => x.Email).First(),
                PhoneNumber = toEdit.drivers.Select(x => x.PhoneNumber).First(),
                AssuranceProof = toEdit.drivers.Select(x => x.AssuranceProof).First(),
                IdentityProof = toEdit.drivers.Select(x => x.IdentityProof).First(),
                DriverLicense = toEdit.drivers.Select(x => x.DriverLicense).First(),

            };
            TempData["ErrorMessage"] = "La demande a échoué";
            ViewBag.BranchId = BranchId;
            return View(vm);
        }


        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Close(int Id, int BranchId)
        {
            var toEdit = _context.Rentals.Include(r => r.drivers).FirstOrDefault(r => r.Id == Id);

            if (toEdit is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(Id));
            }

            var vm = new RentalCloseViewModel()
            {
                Id = toEdit.Id,
                ClosingDate = toEdit.ClosingDate.ToString(),
                BranchId = BranchId
            };

            ViewBag.RentalId = Id;

            TempData["ErrorMessage"] = "La demande a échoué";
            TempData["SuccessMessage"] = "La fermeture à fonctionner.";
            ViewBag.BranchId = BranchId;
            return View(vm);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult CloseRental(int Id, RentalCloseViewModel vm, int BranchId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = BranchId;
                return View(vm);
            }

            var toEdit = _context.Rentals.Include(r => r.drivers).FirstOrDefault(r => r.Id == Id);

            if (toEdit is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(Id));
            }

            Car car = _context.Cars.Find(toEdit.CarId);
            car.Available = true;
            toEdit.Closed = true;
            toEdit.ClosingDate = DateTime.Parse(vm.ClosingDate);

            if (vm.NoteContent != null)
            {
                var note = new Note()
                {
                    rentalId = toEdit.Id,
                    carId = car.ID,
                    NoteContent = vm.NoteContent.Trim(),
                    NotePostDate = DateTime.Now,

                };

                _context.Notes.Add(note);
            }
            _context.SaveChanges();
            TempData["ErrorMessage"] = "The request has failed.";
            TempData["SuccessMessage"] = "Closing Rental succeeded.";
            ViewBag.BranchId = BranchId;
            return RedirectToAction(nameof(Manage), new { BranchId = BranchId });
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult AddComment(int Id, int BranchId)
        {
            var toEdit = _context.Rentals.Include(r => r.drivers).FirstOrDefault(r => r.Id == Id);

            if (toEdit is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(Id));
            }

            SimplifiedCommentViewModel vm = new SimplifiedCommentViewModel();

            ViewBag.RentalId = Id;

            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Succes";
            ViewBag.BranchId = BranchId;
            return View(vm);
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        [HttpPost]
        public IActionResult AddComment(int Id, SimplifiedCommentViewModel vm, int BranchId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = BranchId;
                return View(vm);
            }

            var toEdit = _context.Rentals.Include(r => r.notes).FirstOrDefault(r => r.Id == Id);

            if (toEdit is null)
            {
                ViewBag.BranchId = BranchId;
                throw new ArgumentOutOfRangeException(nameof(Id));
            }

            Note note = new Note
            {
                rentalId = Id,
                carId = toEdit.CarId,
                NoteContent = vm.Content,
                NotePostDate = DateTime.Now,
            };

            toEdit.notes.Add(note);
            _context.SaveChanges();
            TempData["ErrorMessage"] = "Request failed";
            TempData["SuccessMessage"] = "Succefully add a note";
            ViewBag.BranchId = BranchId;
            return RedirectToAction(nameof(Manage), new { BranchId = BranchId });
        }
    }
}
