using Models;

namespace IBuisnessLayer
{
	public interface IStudentBL : IDisposable
	{
		// Méthode qui renvoit  la liste des étudiants
		IQueryable<Student> GetStudents();
		// Méthode pour créer un étudiant
		Task<Student> CreateStudent(Student student);
		// Méthode pour éditer un étudiant
		Task<Student> EditStudent(Student student);
		// Méthode pour récupérer les détails d'un étudiant
		Task<Student> DetailsStudent(int id);
		// Méthode pour supprimer un étudiant
		Task DeleteStudent(Student student);
	}
}
