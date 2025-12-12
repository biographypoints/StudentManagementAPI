using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public class StudentService
    {

        private List<Student> _students = new List<Student>();
        private int nextId = 1;



        public Student AddStudent(Student student)
        {
            student.Id = nextId++;

            _students.Add(student);
            return student;

        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }
        public Student UpdateStudent(int id, Student updatedStudent)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Description = updatedStudent.Description;
                student.RollNumber = updatedStudent.RollNumber;
                student.ClassName = updatedStudent.ClassName;
                student.Section = updatedStudent.Section;
                student.Email = updatedStudent.Email;
                student.PhoneNumber = updatedStudent.PhoneNumber;
            }
            return student;
        }
        // --------------------------
        // Delete Student by ID
        public bool DeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return false;

            _students.Remove(student);
            return true;
        }
        public Student GetStudentByRollNumber(int rollNumber)
        {
            return _students.FirstOrDefault(s => s.RollNumber == rollNumber);

        }
        public Student GetStudentByName(string name)
        {
            return _students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public List<Student> GetStudentByPhoneNumber(string phoneNumber)
        {
            return _students.Where(s => s.PhoneNumber == phoneNumber).ToList();
        }
        public Student FillterStudent(string className, string section)
        {
            return _students.FirstOrDefault(s => s.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase)
            && s.Section.Equals(section, StringComparison.OrdinalIgnoreCase));


        }
       

       // --------------------------
        // Pagination and Sorting
        public List<Student> GetStudentsPagedSorted(int pageNumber, int pageSize, string sortBy = "Name", bool ascending = true)
        {
            IEnumerable<Student> query = _students;

            // Sorting
            query = sortBy.ToLower() switch
            {
                "name" => ascending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
                "rollnumber" => ascending ? query.OrderBy(s => s.RollNumber) : query.OrderByDescending(s => s.RollNumber),
                "class" => ascending ? query.OrderBy(s => s.ClassName) : query.OrderByDescending(s => s.ClassName),
                _ => query.OrderBy(s => s.Name)
            };

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

    }
}
