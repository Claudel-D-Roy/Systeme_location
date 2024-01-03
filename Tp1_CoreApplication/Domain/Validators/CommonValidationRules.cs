using FluentValidation;
using System.Text.RegularExpressions;

namespace Tp1_CoreApplication.Domain.Validators
{
    public static class CommonValidationRules
    {
        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

        private const string REGEX_VALID_NAME = @"^\p{L}+([ -]\p{L}+)*$";

        public static IRuleBuilderOptions<T, string> IsValidName<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(REGEX_VALID_NAME, REGEX_OPTIONS)
                .WithMessage("Please provide a valid name.");
        }
    }
}
