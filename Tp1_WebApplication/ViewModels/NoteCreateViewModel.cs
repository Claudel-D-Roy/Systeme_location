namespace Tp1_WebApplication.ViewModels
{
    public class NoteCreateViewModel
    {
        public int EntityId { get; set; }

        public string EntityType { get; set; }

        public int Id { get; set; }

        public DateTime noteDateTime { get; set; }

        public List<string> Comments { get; set; }

        public string userName { get; set; }

        public int carId { get; set; }
        public int rentalId { get; set; }
        public int BranchId { get; set; }
    }
}
