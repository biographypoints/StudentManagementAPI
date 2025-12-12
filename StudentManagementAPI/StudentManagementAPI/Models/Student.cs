using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Roll Number Required")]
        public int RollNumber { get; set; }

        [Required(ErrorMessage = "Class Name is required")]
        public string ClassName { get; set; }
 
        [Required(ErrorMessage = "Section is required")]
        public string Section { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]   
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }



    }
}
