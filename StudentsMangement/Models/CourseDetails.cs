using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsMangement.Models
{
    public class CourseDetails
    {
        [Key]
        public Guid Id { get; set; }

        public string CourseName { get; set; }

        public string CourseDuration { get; set; }

       
    }
}
