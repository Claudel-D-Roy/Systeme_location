using System.ComponentModel.DataAnnotations;

namespace Tp1_WebApplication.ViewModels
{
    public class UserDetailViewModel
    {
        [Display(Name = "User Id :")]
        public Guid UserId { get; set; }

        [Display(Name = "User Name :")]
        public string UserName { get; set; }

        [Display(Name = "Role :")]
        public string RoleName { get; set; }
    }
}
