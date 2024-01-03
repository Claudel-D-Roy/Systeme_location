using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tp1_CoreApplication;
using Tp1_CoreApplication.Domain;
using Tp1_WebApplication.ViewModels;

namespace Tp1_WebApplication.Controllers
{
    [Authorize]
    public class BranchController : Controller
    {
        private readonly Tp1_Context _context;

        public BranchController(Tp1_Context context)
        {
            this._context = context;
        }

        [Authorize(Roles = "Administrator, Gérant, Commis")]
        public IActionResult Manage()
        {
            var Branchs = _context.Branches.Select(Branchs => new BranchDetailViewModel()
            {
                Id = Branchs.Id,
                Name = Branchs.Name,
                StreetName = Branchs.StreetName!,
                StreetNumber = Branchs.StreetNumber!,
                City = Branchs.City!,
                Province = Branchs.Province!,
                PostalCode = Branchs.PostalCode!,
                Country = Branchs.Country!,
                ActiveBranch = Branchs.ActiveBranch,
                Count = Branchs.Cars.Count(),
                CountAvailable = Branchs.Cars.Count(c => c.Available),
                CountArchive = Branchs.Cars.Count(c => c.status == Tp1_CoreApplication.Enums.Status.Archived),
                CountNotArchive = Branchs.Cars.Count(c => c.status != Tp1_CoreApplication.Enums.Status.Archived)

            });
            TempData["ErrorMessage"] = "The request has failed. Please try again.";
            return View(Branchs);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(BranchCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Name == "")
            {
                ModelState.AddModelError("", "Please enter a name for your branch!");
                return View(vm);
            }

            var newBranch = new Branch()
            {
                Name = vm.Name,
                StreetName = vm.StreetName,
                StreetNumber = vm.StreetNumber!,
                City = vm.City!,
                Province = vm.Province!,
                PostalCode = vm.PostalCode!,
                Country = vm.Country!,
                ActiveBranch = vm.ActiveBranch

            };

            _context.Branches.Add(newBranch);
            _context.SaveChanges();
            TempData["ErrorMessage"] = "The branch creation has failed. Please try again.";
            TempData["SuccessMessage"] = "The branch was created succesfully.";
            return RedirectToAction(nameof(Manage));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult SetActive(int id, bool active)
        {
            var branch = _context.Branches.Find(id);

            if (branch is null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            branch.ActiveBranch = active;
            _context.SaveChanges();
            TempData["ErrorMessage"] = "The activation/deactivation has failed. Please try again.";
            TempData["SuccessMessage"] = "The activation/deactivation succeeded.";
            return RedirectToAction(nameof(Manage));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Remove(int id)
        {
            var toRemove = _context.Branches.Find(id);

            if (toRemove is null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            _context.Branches.Remove(toRemove);
            _context.SaveChanges();
            TempData["ErrorMessage"] = "The branch deletion has failed. Please try again.";
            TempData["SuccessMessage"] = "The branch deletion succeeded.";
            return RedirectToAction(nameof(Manage));
        }
    }
}


