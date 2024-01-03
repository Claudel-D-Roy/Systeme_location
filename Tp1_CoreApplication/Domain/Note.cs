using System.ComponentModel.DataAnnotations.Schema;

namespace Tp1_CoreApplication.Domain
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime NotePostDate { get; set; }
        public string NoteContent { get; set; }

        //View components prop
        [NotMapped]
        public int EntityId { get; set; }

        [NotMapped]
        public string EntityType { get; set; }

        //Navigations
        public int carId { get; set; }
        public int? rentalId { get; set; }

        //À voir plus tard
        [ForeignKey(nameof(carId))]
        public virtual Car car { get; set; }

        [ForeignKey(nameof(rentalId))]
        public virtual Rental? rental { get; set; }
    }
}
