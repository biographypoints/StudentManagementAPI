using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentController(StudentService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            _service.AddStudent(student);
            return Ok("Student added successfully");
        }
        [HttpGet("all")]
        public IActionResult GetAllStudents()
        {
            var students = _service.GetAllStudents();
            return Ok(students);
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = _service.UpdateStudent(id, updatedStudent);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        // Delete Student
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _service.DeleteStudent(id);

            if (!deleted)
                return NotFound("Student not found!");

            return Ok($"Student with ID {id} deleted successfully!");
        }
        [HttpGet("rollnumber/{rollNumber}")]
        public IActionResult GetStudentByRollNumber(int rollNumber)
        {
            var student = _service.GetStudentByRollNumber(rollNumber);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        [HttpGet("name/{name}")]
        public IActionResult GetStudentByName(string name)
        {
            var student = _service.GetStudentByName(name);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        [HttpGet("phonenumber/{phoneNumber}")]
        public IActionResult GetStudentByPhoneNumber(string phoneNumber)
        {
            var student = _service.GetStudentByPhoneNumber(phoneNumber);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        [HttpGet("filter")]
        public IActionResult FilterStudent([FromQuery] string className, [FromQuery] string section)
        {
            var student = _service.FillterStudent(className, section);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        [HttpGet("paged")]
        public IActionResult GetPagedSorted(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string sortBy = "Name",
    [FromQuery] bool ascending = true)
        {
            var students = _service.GetStudentsPagedSorted(pageNumber, pageSize, sortBy, ascending);

            if (students == null || students.Count == 0)
                return NotFound("No students found for the given page!");

            return Ok(students);
        }


    }




}
