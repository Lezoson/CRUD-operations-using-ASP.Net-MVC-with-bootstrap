using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsMangement.Models
{
    public class StudentCourse
    {
        [Key]
        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseDuration { get; set; }

        public Guid CourseStudentID { get; set; }
        [ForeignKey("CourseStudentID")]
        public virtual StudentDetails StudentDetails { get; set; }

       
    }
}
