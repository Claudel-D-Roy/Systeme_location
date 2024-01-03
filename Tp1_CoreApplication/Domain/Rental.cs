using System.ComponentModel.DataAnnotations.Schema;

namespace Tp1_CoreApplication.Domain
{
    public class Rental
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        //public int NoteId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }

        public bool Closed { get; set; }

        //Propriété de navigation
        public List<Driver> drivers { get; set; } = new();

        public List<Note> notes { get; set; } = new();


        [ForeignKey(nameof(CarId))]
        public virtual Car car { get; set; }

        public override string ToString()
        {
            return $"{OpeningDate}, {ClosingDate}";
        }
    }
}
