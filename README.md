# StudentManagementAPI
StudentManagementAPI Start First Version v1.0.0
📘 StudentManagementAPI — Simple C# Demo Project

This is my second project built with ASP.NET Core Web API (without React).
It is created for learning and demo purposes, covering all basic API concepts:

Program.cs setup

Models

Services (Business Logic)

Controllers (API Routes)

Global Exception Handling

This project helps beginners understand how an API works internally with clean examples.

📂 Project Structure
StudentManagementAPI
│
├── Program.cs
├── Models
│     └── Student.cs
│
├── Services
│     └── StudentService.cs
│
├── Controllers
│     └── StudentController.cs
│
└── Middleware
      └── GlobalExceptionMiddleware.cs

🔹 Program.cs (Dependency Injection + Setup)

The Program.cs file registers services, controllers, and middleware.

Purpose:

Register your services

Enable Controllers

Add custom middleware

Example:

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();

🔹 Model — Student.cs

The Model defines the structure of data stored or returned by the API.

Example Fields:

public class Student
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string RollNumber { get; set; }

    [Required]
    public string ClassName { get; set; }

    [Required]
    public string Section { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Phone { get; set; }
}

🔹 Service — StudentService.cs

This file contains the business logic (what the API actually does).

Features Included

Add Student

Get All Students

Update Student

Delete Student

Search by Roll Number / Name / Phone

Filter by Class & Section

Pagination + Sorting

Example Code:

public class StudentService
{
    private readonly List<Student> _students = new();

    public void AddStudent(Student student)
    {
        student.Id = _students.Count + 1;
        _students.Add(student);
    }

    public List<Student> GetAllStudents() => _students;

    public Student UpdateStudent(int id, Student updated)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return null;

        student.Name = updated.Name;
        student.RollNumber = updated.RollNumber;
        student.ClassName = updated.ClassName;
        student.Section = updated.Section;
        student.Email = updated.Email;
        student.Phone = updated.Phone;

        return student;
    }

    public bool DeleteStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return false;

        _students.Remove(student);
        return true;
    }
}

🔹 Controller — StudentController.cs

The Controller defines API URLs and connects them to the service.

API Endpoints
Method	URL	Description
POST	/api/student	Add student
GET	/api/student	Get all students
PUT	/api/student/{id}	Update student
DELETE	/api/student/{id}	Delete student
GET	/api/student/rollnumber/{roll}	Get by roll number
GET	/api/student/name/{name}	Search name
GET	/api/student/phone/{phone}	Search phone
GET	/api/student/filter	Filter class + section
GET	/api/student/paged	Pagination + sorting

Example Code:

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _service;

    public StudentController(StudentService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult AddStudent(Student student)
    {
        _service.AddStudent(student);
        return Ok(student);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAllStudents());
}

🔹 Global Exception Handling

This middleware catches all errors and returns clean JSON responses.

Example:

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message,
                Status = 500
            });
        }
    }
}

🎯 Learning Focus (What This Project Teaches)

✔ Understanding Program.cs
✔ Dependency Injection
✔ How Models work
✔ Building Services (Logic Layer)
✔ Writing Controllers (API Endpoints)
✔ Implementing CRUD operations
✔ Searching, filtering, pagination
✔ Global exception handling
✔ Testing APIs in Postman / Swagger

🚀 How to Run

Clone the repository

Open in Visual Studio / VS Code

Run the project

Open Swagger:

https://localhost:5001/swagger

📌 Final Note

This project is a simple demo designed for learning how a real API is structured.
It prepares the base for future projects like React + C# full integration, authentication, JWT, and database setup.
