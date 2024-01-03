using System.ComponentModel.DataAnnotations;

namespace Tp1_WebApplication.ViewModels
{
    public class RentalCloseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Rental End Date")]
        [DataType(DataType.DateTime)]
        public string ClosingDate { get; set; }

        public int BranchId { get; set; }

        public string? NoteContent { get; set; }
    }
}
