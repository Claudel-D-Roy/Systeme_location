using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Enums;

namespace Tp1_WebApplication.ViewModels
{
    public class CarProfilViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Status : ")]
        public Status status { get; set; } = Status.Active;

        [Display(Name = "State : ")]
        public States State { get; set; }

        [Display(Name = "Available : ")]
        public bool Available { get; set; } = true;

        [Display(Name = "Car Name : ")]
        public string CarName { get; set; }

        [Display(Name = "Serial Number : ")]
        public string SerialNumber { get; set; }

        [Display(Name = "Registration : ")]
        public string Registration { get; set; }

        [Display(Name = "Brand : ")]
        public string Brand { get; set; }

        [Display(Name = "Model : ")]
        public string Model { get; set; }

        [Display(Name = "Year : ")]
        public int Year { get; set; }

        [Display(Name = "Color : ")]
        public string Color { get; set; }

        [Display(Name = "Kilometers : ")]
        public uint Kilometers { get; set; }

        [Display(Name = "Estimated Value : ")]
        [DataType(DataType.Currency)]
        public decimal EstimatedValue { get; set; }

        [Display(Name = "Branch Id : ")]
        public int BranchId { get; set; }

        [Display(Name = "Branch Name : ")]
        public string BranchName { get; set; }

    }
}
