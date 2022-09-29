using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index(Person person)
        {
            if (string.IsNullOrEmpty(person.Name)
                 || person.Height == 0 || person.Weight == 0 || person.Age == 0)
            {
                ViewBag.Info = "Hay nhap thong tin day du!";
            }
            else
            {
                /*string message = "Name: " + person.Name + "\n" 
                    + "Age: " + person.Age + "\n" 
                    + "Height: " + person.Height + "\n"
                    + "Weight: " + person.Weight;*/
                string message = $"Name: {person.Name} \r\nAge: {person.Age} \r\nHeight: {person.Height} \r\nWeight: {person.Weight}";
                ViewBag.Info = message;

            }
            return View();
        }
    }
}
