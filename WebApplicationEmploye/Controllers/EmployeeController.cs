using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using System.Collections.Generic;
using NuGet.Protocol.Core.Types;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        readonly IRepository<Employee> employeeRepository;
        //injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count;
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
        }
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(e);
                return RedirectToAction("Index");
            }
            return View(e);
        }

        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(int id, Employee newemployee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(id, newemployee);
                return RedirectToAction("Index");
            }
            return View(newemployee);
        }

        public ActionResult Delete(int id)
        {
            employeeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string term)
        {
            var result = employeeRepository.Search(term);
            return View("Index", result);
        }

    }
}

