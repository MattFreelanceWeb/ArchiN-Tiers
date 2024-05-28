using BuisnessLayer;
using Castle.Windsor;
using IBuisnessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class StudentController : Controller
    {
        private static WindsorContainer InitDependency()
        {
            var container = new WindsorContainer();
            container.Register(Castle.MicroKernel.Registration.Component.For<IStudentBL>().ImplementedBy<StudentBL>());
            return container;
        }

        IStudentBL context = InitDependency().Resolve<IStudentBL>();

        // GET: StudentController
        public ActionResult Index(string searchString)
        {
            var students = context.GetStudents().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Nom.Contains(searchString) || s.Prenom.Contains(searchString)).ToList();
                ViewData["CurrentFilter"] = searchString;
            }

            return View(students);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await context.DetailsStudent(id));
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                await context.CreateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await context.DetailsStudent(id));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            try
            {
                await context.EditStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await context.DetailsStudent(id));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Student student)
        {
            try
            {
                await context.DeleteStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
