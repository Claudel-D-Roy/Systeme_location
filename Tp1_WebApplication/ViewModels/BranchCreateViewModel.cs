using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Tp1_WebApplication.ViewModels
{
    public class BranchCreateViewModel
    {
        [Display(Name = "Branch Name")]
        public string Name { get; set; }

        [Display(Name = "Street Number")]
        public string? StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string? StreetName { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Province")]
        public string? Province { get; set; }

        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "Active")]
        public bool ActiveBranch { get; set; } = false;

        public class Validator : AbstractValidator<BranchCreateViewModel>
        {
            private const int NAME_MIN = 2;
            private const int NAME_MAX = 20;
            private const string CHIFFREONLY = @"^[0-9]+$";
            private const string LETTERS_ONLY_REGEX = "^[a-zA-Z]+$";
            private const string CHIFFRELETTRE = @"^[a-zA-Z0-9]+$";
            private const string POSTALCODE = "^[A-Za-z]\\d[A-Za-z] ?\\d[A-Za-z]\\d$";

            public Validator()
            {
                RuleFor(vm => vm.Name).NotEmpty().WithMessage("Please provide a name for the branch.").Length(NAME_MIN, NAME_MAX)
                    .WithMessage("Please provide a name between 2 and 20 characters");

                RuleFor(vm => vm.StreetNumber).NotEmpty().When(vm => !string.IsNullOrEmpty(vm.StreetNumber))
                    .WithMessage("Please provide a street number for the branch.").Matches(CHIFFREONLY).When(vm => !string.IsNullOrEmpty(vm.StreetNumber))
                    .WithMessage("The street number must only be made of numbers");

                RuleFor(vm => vm.StreetName).NotEmpty().When(vm => !string.IsNullOrEmpty(vm.StreetName))
                    .WithMessage("Please provide a street name for the branch.").Matches(LETTERS_ONLY_REGEX).When(vm => !string.IsNullOrEmpty(vm.StreetName))
                    .WithMessage("The street name must only be made of letters");

                RuleFor(vm => vm.City).NotEmpty().When(vm => !string.IsNullOrEmpty(vm.City)).WithMessage("Please provide a city for the branch.")
                    .Matches(LETTERS_ONLY_REGEX).When(vm => !string.IsNullOrEmpty(vm.City)).WithMessage("The city must only be made of letters");

                RuleFor(vm => vm.Province).NotEmpty().When(vm => !string.IsNullOrEmpty(vm.Province)).WithMessage("Please provide a province for the branch.")
                    .Matches(LETTERS_ONLY_REGEX).When(vm => !string.IsNullOrEmpty(vm.Province)).WithMessage("The province must only be made of letters");

                RuleFor(vm => vm.PostalCode).NotEmpty().When(vm => !string.IsNullOrEmpty(vm.PostalCode)).WithMessage("Please provide a postal code for the branch.")
                    .Matches(POSTALCODE).When(vm => !string.IsNullOrEmpty(vm.PostalCode)).WithMessage("The postal code must follow the format 'A0A 0A0'");

                RuleFor(vm => vm.Country).NotEmpty().When(vm => !string.IsNullOrEmpty(vm.Country)).WithMessage("Please provide a country code for the branch.")
                    .Matches(LETTERS_ONLY_REGEX).When(vm => !string.IsNullOrEmpty(vm.Country)).WithMessage("The country code must only be made of letters");
            }
        }

    }
}
