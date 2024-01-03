using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Enums;

namespace Tp1_WebApplication.ViewModels
{
    public class CarManageViewModel
    {
        public int BranchId { get; set; }

        public int ID { get; set; }

        [Display(Name = "Car Name : ")]
        public string CarName { get; set; }

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

        [Display(Name = "Availability : ")]
        public bool Availability { get; set; }

        public bool Activated { get; set; }

        public Status Status { get; set; }
    }
}
