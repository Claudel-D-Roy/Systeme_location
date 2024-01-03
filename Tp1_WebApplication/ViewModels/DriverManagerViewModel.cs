using System.ComponentModel.DataAnnotations;

namespace Tp1_WebApplication.ViewModels
{
    public class DriverManagerViewModel
    {
        public int id { get; set; }

        [Display(Name = "Full name : ")]
        public string FullName { get; set; }

        [Display(Name = "Reservation Count")]
        public int ReservationCount { get; set; }

        [Display(Name = "Last Reservation Date")]
        public DateTime DateLastReservation { get; set; }

        public int BranchId { get; set; }
    }
}
