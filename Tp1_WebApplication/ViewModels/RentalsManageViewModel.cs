using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain;
using Tp1_CoreApplication.Enums;

namespace Tp1_WebApplication.ViewModels
{
    public class RentalsManageViewModel
    {
        [Display(Name = "Name : ")]
        public string FullName { get; set; }

        public string branchName { get; set; }

        [Display(Name = "Driver's Address")]
        public string Address { get; set; }

        [Display(Name = "Driver's PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Car Id")]
        public int CarId { get; set; }

        [Display(Name = "Car Name")]
        public string CarName { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Registration")]
        public string Registration { get; set; }

        [Display(Name = "State")]
        public States State { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Kilometers")]
        public uint Kilometers { get; set; }

        [Display(Name = "Estimated Value")]
        [DataType(DataType.Currency)]
        public decimal EstimatedValue { get; set; }

        [Display(Name = "Rental Id")]
        public int Id { get; set; }

        [Display(Name = "Rental Start Date")]
        [DataType(DataType.DateTime)]
        public string OpeningDateTime { get; set; }

        [Display(Name = "Rental End Date")]
        [DataType(DataType.Time)]
        public string ClosingDateTime { get; set; }

        public bool closed { get; set; }

        public List<string> branchList { get; set; }

        public int BranchId { get; set; }

        public int rentalId { get; set; }

        public List<Note>? note { get; set; }
    }
}
