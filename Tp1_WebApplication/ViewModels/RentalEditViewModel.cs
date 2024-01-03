using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain;
using Tp1_CoreApplication.Enums;

namespace Tp1_WebApplication.ViewModels
{
    public class RentalEditViewModel
    {
        [Display(Name = "Car :")]
        public int CarId { get; set; }

        [Display(Name = "CarName")]
        public string CarName { get; set; }

        [Display(Name = "Serial number")]
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

        public bool available { get; set; }

        [Display(Name = "Rental Start Date")]
        [DataType(DataType.DateTime)]
        public string OpeningDate { get; set; }

        [Display(Name = "Rental End Date")]
        [DataType(DataType.DateTime)]
        public string ClosingDate { get; set; }

        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Driver's License")]
        public string DriverLicense { get; set; }

        [Display(Name = "Proof of Identity")]
        public bool IdentityProof { get; set; }

        [Display(Name = "Proof of Insurance")]
        public bool AssuranceProof { get; set; }

        public int BranchId { get; set; }

        public List<Note>? note { get; set; }
    }
}
