using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: EmployeesController
        public ActionResult Index()
        {
            List<Employee?>? list = Employee.getAllEmployees();

            return View(list);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            Employee? employee = Employee.getEmployee(id);

            return View(employee);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee obj)
        {
            try
            {
                
                Employee.Insert(obj);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id = 1)
        {

            Employee employee = new Employee();

            employee.EmpNo = 1;
            employee.Name = "Jitendra";
            employee.Basic = 12345;
            employee.DeptNo = 4;

            return View(employee);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {

            Employee emp = Employee.getEmployee(id);
            Console.WriteLine("in delete Get method");


            return View(emp);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee emp, int id)
        {
            try
            {

                Employee.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
