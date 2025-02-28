using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> employeeRepository; // Correction du type et ajout de l'espace

        // Injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        // Méthodes d'action
        public ActionResult Index() { 
            var employees = employeeRepository.GetAll(); 
            ViewData["EmployeesCount"] = employees.Count(); 
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage(); 
            ViewData["MaxSalary"] = employeeRepository.MaxSalary(); 
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount(); 
            return View(employees);
        }

        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id); // Correction de la syntaxe
            return View(employee);
        } 

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            employeeRepository.Add(employee); // Correction de la syntaxe
            return RedirectToAction(nameof(Index));
        } 
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            employeeRepository.Update(id, employee); // Correction de la syntaxe
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            employeeRepository.Delete(id); // Correction de la syntaxe
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Search(string term) { 
            var result = employeeRepository.Search(term); 
            return View("Index", result); 
        }
    }
}
