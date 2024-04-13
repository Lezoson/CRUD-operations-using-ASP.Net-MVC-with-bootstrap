using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsMangement.Models
{
    public class StudentDetails
    {
        [Key]
        public Guid StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime DateCreated { get; set; }  

        public StudentAddress StudentAddress { get; set; }  

        public virtual StudentCourse StudentCourse { get; set; }
        public virtual CourseDetails CourseDetails { get; set; }

        [NotMapped]
        public string[] SelectedItems { get; set; }              
        }
}
