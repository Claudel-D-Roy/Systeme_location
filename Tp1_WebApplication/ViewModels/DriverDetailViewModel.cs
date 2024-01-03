using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain;

namespace Tp1_WebApplication.ViewModels
{
    public class DriverDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name : ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name : ")]
        public string LastName { get; set; }

        [Display(Name = "Driver's Licence : ")]
        public string DriverLicense { get; set; }

        public List<Rental> Rentals { get; set; }

        public int BranchId { get; set; }
    }
}
