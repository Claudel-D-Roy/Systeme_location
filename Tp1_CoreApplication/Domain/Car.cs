using System.ComponentModel.DataAnnotations.Schema;
using Tp1_CoreApplication.Enums;

namespace Tp1_CoreApplication.Domain
{
    public class Car
    {

        public int ID { get; set; }

        public Status status { get; set; } = Status.Active;

        public States State { get; set; }

        public bool Available { get; set; } = true;

        public string CarName { get; set; }

        public string SerialNumber { get; set; }

        public string Registration { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        public uint Kilometers { get; set; }

        public decimal EstimatedValue { get; set; }

        //public string Image { get; set; }

        public bool Activated { get; set; }

        public int BranchId { get; set; }

        //Propriété de navigation
        [ForeignKey(nameof(BranchId))]
        public virtual Branch branch { get; set; }

        public List<Note> notes { get; set; } = new();
        public List<Rental> Rental { get; set; }


        //À voir plus tard
        //List<Note> Notes { get; set; }

    }
}
