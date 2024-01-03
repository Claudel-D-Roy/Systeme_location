using System.ComponentModel.DataAnnotations;

namespace Tp1_WebApplication.ViewModels
{
    public class BranchDetailViewModel
    {
        [Display(Name = "Branch Id")]
        public int Id { get; set; }

        [Display(Name = "Branch Name")]
        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Active")]
        public bool ActiveBranch { get; set; }

        [Display(Name = "Number Of Cars")]
        public int Count { get; set; }

        [Display(Name = "Number of Cars Available")]
        public int CountAvailable { get; set; }

        [Display(Name = "Archived Car")]
        public int CountArchive { get; set; }

        [Display(Name = "Not Archived Car")]
        public int CountNotArchive { get; set; }

    }
}
