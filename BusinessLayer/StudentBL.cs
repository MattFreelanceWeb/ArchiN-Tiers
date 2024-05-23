using DataRepository;
using IBuisnessLayer;
using IDataRepository;
using Models;

namespace BuisnessLayer
{
	public class StudentBL : IStudentBL
	{
		IStudentRepository _dataFactory;

		public StudentBL(): this(new StudentRepository())
		{
		}

		private StudentBL(IStudentRepository dataFactory)
		{
			_dataFactory = dataFactory;
		}

		public Task<Student> CreateStudent(Student student)
		{
			return _dataFactory.CreateStudent(student);
		}
		
		
		/// <summary>
		/// Édition d'un étudiant
		/// </summary>
		/// <param name="student">objet Student</param>
		/// <returns>Task de type Student</returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<Student> EditStudent(Student student)
		{
			return _dataFactory.EditStudent(student);
		}

		/// <summary>
		/// Détails d'un étudiant
		/// </summary>
		/// <param name="student">objet Student</param>
		/// <returns>Task de type Student</returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<Student> DetailsStudent(int id)
		{
			return _dataFactory.DetailsStudent(id);
		}

		/// <summary>
		/// Suppression d'un étudiant
		/// </summary>
		/// <param name="student">objet Student</param>
		/// <returns>Task de type Student</returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task DeleteStudent(Student student)
		{
			return _dataFactory.DeleteStudent(student);
		}

		public void Dispose()
		{
			if (_dataFactory != null)
			{
				_dataFactory.Dispose();
			}
		}

		public IQueryable<Student> GetStudents()
		{
			return _dataFactory.GetStudents();
		}
	}
}
