using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain.Validators;

namespace Tp1_WebApplication.ViewModels;

public class UserCreateViewModel
{
    [Display(Name = "User Name :")]
    [Required(AllowEmptyStrings = false)]
    public string UserName { get; set; }

    [Display(Name = "Role :")]
    public Guid RoleId { get; set; }

    [Display(Name = "Password :")]
    [DataType(DataType.Password)]
    [Required(AllowEmptyStrings = false)]
    public string Pwd { get; set; }

    [Display(Name = "Confirm Password :")]
    [DataType(DataType.Password)]
    [Required(AllowEmptyStrings = false)]
    public string ConfirmPwd { get; set; }

    public class Validator : AbstractValidator<UserCreateViewModel>
    {
        public Validator()
        {
            RuleFor(vm => vm.UserName)
                .SetValidator(new UsernameValidator());

            RuleFor(vm => vm.Pwd)
                .SetValidator(new PasswordValidator());

            RuleFor(vm => vm.ConfirmPwd)
                .Equal(vm => vm.Pwd)
                    .WithMessage("The password and confirmation password do not match.");
        }
    }
}
