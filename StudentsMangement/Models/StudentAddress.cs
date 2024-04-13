using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsMangement.Models
{
    public class StudentAddress
    {
        [Key]
        public Guid AddressId { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Guid AddressStudentID { get; set; }
        [ForeignKey("AddressStudentID")]
        public virtual StudentDetails StudentDetails { get; set; }

    }

}
