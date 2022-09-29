using DemoMVC.Models.Xulychuoi;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers{
    public class StringProcessController : Controller{
         public IActionResult stringProcess()
        {
            return View();
        }

        [HttpPost]

        public IActionResult stringProcess(string stringInput)
        {
            // ViewData["RemovedSpaceString"] = Xulychuoi.removeSpaceInString(stringInput);
            // ViewData["toUpperString"] = Xulychuoi.toUpperString(stringInput);
            // ViewData["toLowerString"] = Xulychuoi.toLowerString(stringInput);
            // ViewData["CapitalizeOneFirstCharacter"] = Xulychuoi.CapitalizeOneFirstCharacter(stringInput);
            ViewData["CapitalizeFirstCharacter"] = Xulychuoi.CapitalizeOneFirstCharacter(stringInput);

            return View();
        }
    }
}