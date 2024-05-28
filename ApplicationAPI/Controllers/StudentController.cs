using Microsoft.AspNetCore.Mvc;
using Models;
using ORM;

namespace ApplicationAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase {
    private StudentContext _context;

    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
        _context = StudentContext.Instance; // Appelle singleton
    }

    [HttpGet(Name = "GetStudents")]
    public IQueryable<Student> Get()
    {
        return _context.Student;
    }
    [HttpGet("{id}",Name = "GetStudentById")]
    public ActionResult<Student> GetById([FromRoute] int id)
    {
        var student = _context.Student.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return student;
    }
    
    
    [HttpPost(Name = "PostStudent")]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] StudentCreateDto studentDto)
    {
        // Créez un nouvel objet Student avec les données fournies
        var student = new Student
        {
            Prenom = studentDto.Prenom,
            Nom = studentDto.Nom
        };

        // Ajoutez l'étudiant au contexte
        _context.Student.Add(student);

        // Sauvegardez les modifications de manière asynchrone
        await _context.SaveChangesAsync();

        // Retournez l'étudiant créé avec un code de statut 201 Created
        return CreatedAtRoute("GetStudentById", new { id = student.Id }, student);
    }

    
    [HttpPut(Name = "PutStudent")]
    public async Task<Student> EditStudent([FromQuery] Student student)
    {
        var existingStudent = await _context.Student.FindAsync(student.Id);
        if (existingStudent == null)
        {
            throw new Exception("Student not found");
        }

        existingStudent.Nom = student.Nom;
        existingStudent.Prenom = student.Prenom;

        _context.Student.Update(existingStudent);
        await _context.SaveChangesAsync();
        return existingStudent;
    }
    
    [HttpDelete("{id}",Name = "DeleteStudent")]
    public async Task DeleteStudent(int id)
    {
        var existingStudent = await _context.Student.FindAsync(id);
        if (existingStudent == null)
        {
            throw new Exception("Student not found");
        }

        _context.Student.Remove(existingStudent);
        await _context.SaveChangesAsync();
    }
}