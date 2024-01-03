using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Tp1_WebApplication.ViewModels
{
    public class RentalCreateViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }

        [Display(Name = "Rental Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime OpeningDate { get; set; }

        [Display(Name = "Rental End Date")]
        [DataType(DataType.DateTime)]
        public DateTime ClosingDate { get; set; }

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

        public string? NoteContent { get; set; }

        public class Validator : AbstractValidator<RentalCreateViewModel>
        {
            private const string NAME_REGEX = @"^[a-zA-Z]{3,50}$";
            private const string FIRSTNAME_REGEX = @"^[a-zA-Z]{3,30}$";
            private const string PHONE_REGEX = @"^(\(\d{3}\)\s\d{3}-\d{4}|\d{3}-\d{3}-\d{4}|\d{10})$";
            private const string EMAIL_REGEX = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            private const string LICENSE_REGEX = @"^[A-Z]\d{4}(0[1-9]|[12][0-9]|3[01])(0[1-9]|1[012])(\d{2})\d{2}$";
            private const string ADDRESS_REGEX = @"^[a-zA-Z\s]{5,30}$";


            public Validator()
            {
                RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("Please provide a first name.").Length(3, 30)
                    .WithMessage("Please provide a first name between 3 and 30 characters.").Matches(FIRSTNAME_REGEX)
                    .WithMessage("First name can only contain alphabetic characters without spaces.");

                RuleFor(vm => vm.LastName).NotEmpty().WithMessage("Please provide a last name.").Length(3, 50)
                    .WithMessage("Please provide a last name between 3 and 50 characters.").Matches(NAME_REGEX)
                    .WithMessage("Last name can only contain alphabetic characters without spaces.");

                RuleFor(vm => vm.PhoneNumber).NotEmpty().WithMessage("Please provide a phone number.").Matches(PHONE_REGEX)
                    .WithMessage("Please provide a valid phone number. Valid formats: (555) 555-5555, 555-555-5555, 5555555555.");

                RuleFor(vm => vm.Email).NotEmpty().WithMessage("Please provide an email.").Matches(EMAIL_REGEX)
                    .WithMessage("Please provide a valid email address.");

                RuleFor(vm => vm.DriverLicense).NotEmpty().WithMessage("Please provide a driver's license.")
                    .Matches(LICENSE_REGEX).WithMessage("Driver's license should follow the pattern 'A####DDMMYY##'.")
                    .Must((vm, license) => license[0] == vm.LastName[0])
                    .WithMessage("Driver's license should start with the first letter of the last name.");

                RuleFor(vm => vm.StreetAddress).NotEmpty().WithMessage("Please provide a street address.").Length(5, 30)
                    .WithMessage("Street address should be between 5 and 30 characters.").Matches(ADDRESS_REGEX)
                    .WithMessage("Street address can only contain alphabetic characters and spaces.");

                RuleFor(vm => vm.OpeningDate).NotEmpty().WithMessage("Please provide an opening date.")
                    .LessThanOrEqualTo(vm => vm.ClosingDate).WithMessage("Opening date must be earlier than or equal to closing date.");

                RuleFor(vm => vm.ClosingDate).NotEmpty().WithMessage("Please provide a closing date.").GreaterThanOrEqualTo(vm => vm.OpeningDate)
                    .WithMessage("Closing date must be later than or equal to opening date.");

                RuleFor(vm => vm.IdentityProof).Equal(true).WithMessage("Please provide proof of identity.");

                RuleFor(vm => vm.AssuranceProof).Equal(true).WithMessage("Please provide proof of insurance.");

            }
        }
    }
}
