using TranBaVuBTH2.Data;
using TranBaVuBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranBaVuBTH2.Models.Process;

namespace TranBaVuBTH2.Controllers{
    public class PersonController : Controller{
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var model = await _context.People.ToListAsync();
            return View(model);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person per){
            if(ModelState.IsValid){
                _context.Add(per);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(per);
        }
        private bool PersonExists(string id)
        {
            return _context.People.Any(e => e.PersonID == id);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return View("NotFound");
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonID, PersonName, Height, Weight")] Person std)
        {
            if (id != std.PersonID)
            {
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(std.PersonID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var std = await _context.People.FirstOrDefaultAsync(m => m.PersonID == id);
            if (std == null)
            {
                return View("NotFound");
            }
            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var std = await _context.People.FindAsync(id);
            _context.People.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private ExcelProcess _excelProcess = new ExcelProcess();
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload");
                }
                else
                {
                    //rename file when upload to server
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Person object
                            var emp = new Person();
                            //set values for attributes
                            emp.PersonID = dt.Rows[i][0].ToString();
                            emp.PersonName = dt.Rows[i][1].ToString();
                            emp.Height = dt.Rows[i][2].ToString();
                            emp.Weight = dt.Rows[i][3].ToString();
                       
                            //add object to Context
                            if (!PersonExists(emp.PersonID))
                            {
                                _context.People.Add(emp);
                            }

                        }
                        //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }


                }
            }
            return View();
        }
    }
}