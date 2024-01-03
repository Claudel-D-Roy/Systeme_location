using FluentValidation;
using System.Text.RegularExpressions;

namespace Tp1_CoreApplication.Domain.Validators
{
    public class UsernameValidator : AbstractValidator<string?>
    {
        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

        private const int LENGTH_MIN = 6;
        private const string REGEX_USERNAME = @"^([a-z0-9_.-])+$";

        public UsernameValidator()
        {
            RuleFor(userName => userName)
                .NotEmpty()
                    .WithMessage("The UserName cannot be empty.")
                .MinimumLength(LENGTH_MIN)
                    .WithMessage($"The UserName must have at least {LENGTH_MIN} characters.")
                .Matches(REGEX_USERNAME, REGEX_OPTIONS)
                    .WithMessage("The UserName contains invalid characters.");
        }
    }

    public class FirstNameValidator : AbstractValidator<string?>
    {
        private const int FIRST_NAME_LENGTH_MIN = 2;
        private const int FIRST_NAME_LENGTH_MAX = 30;

        public FirstNameValidator()
        {
            RuleFor(firstName => firstName)
                .NotEmpty()
                .WithMessage("Please provide a First Name.")
                .Length(FIRST_NAME_LENGTH_MIN, FIRST_NAME_LENGTH_MAX)
                .WithMessage("Please provide a First Name between 3 and 30 caracters.")
                .IsValidName()
                .WithMessage("Please provide a First Name who contains only letters.");
        }
    }

    public class LastNameValidator : AbstractValidator<string?>
    {
        private const int LAST_NAME_LENGTH_MIN = 2;
        private const int LAST_NAME_LENGTH_MAX = 30;

        public LastNameValidator()
        {
            RuleFor(lastName => lastName)
                .NotEmpty()
                .WithMessage("Please provide a Last Name.")
                .Length(LAST_NAME_LENGTH_MIN, LAST_NAME_LENGTH_MAX)
                .WithMessage("Please provide a Last Name between 3 and 30 caracters.")
                .IsValidName()
                .WithMessage("Please provide a Last Name who contains only letters.");
        }
    }

    public class PasswordValidator : AbstractValidator<string?>
    {
        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

        private const int LENGTH_MIN = 8;
        private const string REGEX_UPPERCASE = @"[A-Z]*";
        private const string REGEX_LOWERCASE = @"[a-z]*";
        private const string REGEX_DIGIT = @"[0-9]*";
        private const string REGEX_NOT_SPACE = @"[^\s]*";

        public PasswordValidator()
        {
            RuleFor(password => password)
                .NotEmpty()
                    .WithMessage("The Password cannot be empty.")
                .MinimumLength(LENGTH_MIN)
                    .WithMessage($"The Password must have at least {LENGTH_MIN} characters.")
                .Matches(REGEX_UPPERCASE)
                    .WithMessage("The Password must have at least one uppercase letter.")
                .Matches(REGEX_LOWERCASE)
                    .WithMessage("The Password must have at least one lowercase letter.")
                .Matches(REGEX_DIGIT)
                    .WithMessage("The Password must have at least one digit.")
                .Matches(REGEX_NOT_SPACE)
                    .WithMessage("The Password cannot contain spaces.");
        }
    }
}
