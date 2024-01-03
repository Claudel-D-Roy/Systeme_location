using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain.Validators;

namespace Tp1_WebApplication.ViewModels
{
    public class LogInViewModel
    {
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Pwd { get; set; }

        public class Validator : AbstractValidator<LogInViewModel>
        {
            public Validator()
            {
                RuleFor(x => x.UserName)
                    .SetValidator(new UsernameValidator());

                RuleFor(vm => vm.Pwd)
                    .SetValidator(new PasswordValidator());
            }
        }
    }
}
