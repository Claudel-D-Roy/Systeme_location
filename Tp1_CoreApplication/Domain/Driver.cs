using System.ComponentModel.DataAnnotations.Schema;

namespace Tp1_CoreApplication.Domain
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DriverLicense { get; set; }
        public bool IdentityProof { get; set; }
        public bool AssuranceProof { get; set; }

        public int RentalId { get; set; }

        //Propriété de navigation
        [ForeignKey(nameof(RentalId))]
        public virtual Rental rental { get; set; }

        //Last rental datetime??? À voir

        public override string ToString()
        {
            return $"{LastName.ToUpper()}, {FirstName}, {Address}, " +
                $"{PhoneNumber}, {Email}, {DriverLicense}";
        }
    }
}
