using CrudConsumeApi.Repository;
using Employee_Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrudConsumeApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICommonRepository repository;

        public EmployeeController(ICommonRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<EmployeeDetailsModel> employees = repository.GetAll<EmployeeDetailsModel>("Employee/GetAllEmployee");
            return View(employees);

        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDetailsModel employee)
        {
            if (ModelState.IsValid)
            {
                int affectedRows = repository.Post<int>("Employee/AddEmployee", employee);
                if (affectedRows > 0)
                {
                    TempData["successMessage"] = "Employee created successfully!";
                    return RedirectToAction("Index");
                }
                TempData["errorMessage"] = "Employee not created successfully!";
                return RedirectToAction("Error");
            }
            else
            {
                TempData["errorMessage"] = "Model data is not valid.";
            }
            return View(employee);
        }

       
        public IActionResult Edit(int employeeId)
        {
            EmployeeDetailsModel employee = repository.Get<EmployeeDetailsModel>("Employee/GetEmployeeById", new { employeeId });
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeDetailsModel employee)
        {
            if (ModelState.IsValid)
            {
                int affectedRows = repository.Post<int>("Employee/UpdateEmployee", employee);
                if (affectedRows > 0)
                {
                    TempData["successMessage"] = "Employee updated successfully!";
                    return RedirectToAction("Index");
                }
                TempData["errorMessage"] = "Employee not updated successfully!";
                return RedirectToAction("Error");
            }
            else
            {
                TempData["errorMessage"] = "Model data is not valid.";
            }
            return View(employee);
        }

        public IActionResult Details(int employeeId)
        {
            EmployeeDetailsModel employees = repository.Get<EmployeeDetailsModel>("Employee/GetEmployeeById", new { employeeId });
            return View(employees);
        }

        
        public IActionResult Delete(int employeeId)
        {
            EmployeeDetailsModel employee = repository.Get<EmployeeDetailsModel>("Employee/GetEmployeeById", new { employeeId });
            int affectedRows = repository.Post<int>("Employee/DeleteEmployee", employee);
            if (affectedRows > 0)
            {
                TempData["successMessage"] = "Employee Deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["errorMessage"] = "Error emplyee Deleted";
            return RedirectToAction("Error");
        }
    }
}
