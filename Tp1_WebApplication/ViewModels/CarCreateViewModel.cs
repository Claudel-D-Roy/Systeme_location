using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Enums;

namespace Tp1_WebApplication.ViewModels
{
    public class CarCreateViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Status : ")]
        public Status status { get; set; } = Status.Active;

        [Display(Name = "State : ")]
        public States State { get; set; }

        [Display(Name = "Availability : ")]
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
        public string? NoteComment { get; set; }

        public class Validator : AbstractValidator<CarCreateViewModel>
        {
            private const int NAME_MIN = 5;
            private const int NAME_MAX = 20;
            private const string NAME_REGEX = @"^[a-zA-Z0-9\-#]{5,20}$";
            private const string PLATE_REGEX = "^[A-Z0-9]{1,7}(-[A-Z0-9]{1,7})?$";
            private const string CHIFFRELETTRE = @"^[a-zA-Z0-9]+$";
            private const string LETTERS_ONLY_REGEX = "^[a-zA-Z]+$";

            public Validator()
            {
                RuleFor(vm => vm.CarName).NotEmpty().WithMessage("Please provide a name for the car.").Length(NAME_MIN, NAME_MAX)
                    .WithMessage("Please provide a name between 5 and 20 characters").Matches(NAME_REGEX).WithMessage("Please provide a valid name.");

                RuleFor(vm => vm.SerialNumber).NotEmpty().WithMessage("Please provide a serial number for the car.").Matches(PLATE_REGEX)
                    .WithMessage("Please provide a valid serial number for the car.");

                RuleFor(vm => vm.Registration).NotEmpty().WithMessage("Please provide a registration number for the car.").Matches(CHIFFRELETTRE)
                    .WithMessage("The registration must only be made of numbers and letters");

                RuleFor(vm => vm.Brand).NotEmpty().WithMessage("Please provide a brand for the car.");

                RuleFor(vm => vm.Model).NotEmpty().WithMessage("Please provide a model for the car.");

                RuleFor(vm => vm.Year).NotEmpty().WithMessage("Please provide a year for the car.").InclusiveBetween(2000, 2024)
                    .WithMessage("Please provide a year greater than 1999 for the car.");

                RuleFor(vm => vm.Color).NotEmpty().WithMessage("Please provide a color for the car.").Matches(LETTERS_ONLY_REGEX)
                   .WithMessage("Please provide a valid color for the car.");

            }
        }
    }
}
