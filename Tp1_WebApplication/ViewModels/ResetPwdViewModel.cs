using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Tp1_CoreApplication.Domain.Validators;

namespace Tp1_WebApplication.ViewModels
{
    public class ResetPwdViewModel
    {
        [Display(Name = "User Id :")]
        public Guid UserId { get; set; }

        [Display(Name = "User Name :")]
        public string UserName { get; set; }

        [Display(Name = "Password :")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password :")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false)]
        public string ConfirmNewPassword { get; set; }

        public class Validator : AbstractValidator<ResetPwdViewModel>
        {
            public Validator()
            {
                RuleFor(vm => vm.NewPassword)
                    .SetValidator(new PasswordValidator());

                RuleFor(vm => vm.ConfirmNewPassword)
                    .Equal(vm => vm.NewPassword)
                        .WithMessage("The password and confirmation password do not match.");
            }
        }
    }
}
