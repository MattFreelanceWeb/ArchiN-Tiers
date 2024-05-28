using IDataRepository;
using Models;
using ORM;

namespace DataRepository
{
	public class StudentRepository : IStudentRepository
	{
		// context DB
		private StudentContext _context;

		// injection de dépendence par le constructeur
		public StudentRepository() 
		{ 
			_context = StudentContext.Instance; // Appelle singleton
		}

		public IQueryable<Student> GetStudents()
		{
			return _context.Student;
		}

		public async Task<Student> CreateStudent(Student student)
		{
			_context.Student.Add(student);

			await _context.SaveChangesAsync();
			return student;
		}
		
		/// <summary>
		/// Édition d'un étudiant
		/// </summary>
		/// <param name="student">objet Student</param>
		/// <returns>Task de type Student</returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Student> EditStudent(Student student)
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

		/// <summary>
		/// Détails d'un étudiant
		/// </summary>
		/// <param name="student">objet Student</param>
		/// <returns>Task de type Student</returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Student> DetailsStudent(int id)
		{
			var student = await _context.Student.FindAsync(id);
			if (student == null)
			{
				throw new Exception("Student not found");
			}
			return student;
		}

		/// <summary>
		/// Suppression d'un étudiant
		/// </summary>
		/// <param name="student">objet Student</param>
		/// <returns>Task de type Student</returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task DeleteStudent(Student student)
		{
			var existingStudent = await _context.Student.FindAsync(student.Id);
			if (existingStudent == null)
			{
				throw new Exception("Student not found");
			}

			_context.Student.Remove(existingStudent);
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			if(_context != null)
			{
				_context.Dispose();
			}
		}

	}
}
