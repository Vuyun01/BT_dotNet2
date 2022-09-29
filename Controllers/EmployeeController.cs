using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name)
                 || string.IsNullOrEmpty(employee.ID) || employee.Age == 0)
            {
                ViewBag.Info = "Hay nhap thong tin day du!";
            }
            else
            {
                /*string message = "ID: " + employee.ID + "\n"
                    + "Name: " + employee.Name + "\n"
                    + "Age: " + employee.Age;*/
                string message = $"ID: {employee.ID} \nName: {employee.Name} \nAge: {employee.Age}";
                ViewBag.Info = message;

            }
            return View();
        }
    }
}
