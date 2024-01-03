using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain;
using Tp1_CoreApplication.Enums;

namespace Tp1_WebApplication.ViewModels
{
    public class CarEditViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Status:")]
        public Status status { get; set; } = Status.Active;

        [Display(Name = "State:")]
        public States State { get; set; }

        [Display(Name = "Availability:")]
        public bool Available { get; set; } = true;

        [Display(Name = "Car Name:")]
        public string CarName { get; set; }

        [Display(Name = "Registration:")]
        public string Registration { get; set; }

        [Display(Name = "Color:")]
        public string Color { get; set; }

        [Display(Name = "Kilometers:")]
        public uint Kilometers { get; set; }

        [Display(Name = "Estimated Value:")]
        [DataType(DataType.Currency)]
        public decimal EstimatedValue { get; set; }

        [Display(Name = "Notes : ")]
        public List<Note>? Notes { get; set; }

        [Display(Name = "Rentals : ")]
        public List<Rental>? Rentals { get; set; }

        public int RentalId { get; set; }

        public int BranchId { get; set; }
    }
}
