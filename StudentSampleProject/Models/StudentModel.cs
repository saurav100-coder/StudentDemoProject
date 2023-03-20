using System.ComponentModel.DataAnnotations;

namespace StudentSampleProject.Models
{
    public class StudentModel
    {
        [Display (Name = "Id")]
        public int StudId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
    }
}
