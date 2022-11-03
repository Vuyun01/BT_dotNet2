using TranBaVuBTH2.Data;
using TranBaVuBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranBaVuBTH2.Models.Process;

namespace TranBaVuBTH2.Controllers{
    public class CustomerController : Controller{
        private readonly ApplicationDbContext _context;

        private ExcelProcess _excelProcess = new ExcelProcess();

        public CustomerController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var model = await _context.Customers.ToListAsync();
            return View(model);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer cus){
            if(ModelState.IsValid){
                _context.Add(cus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cus);
        }
        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var Customer = await _context.Customers.FindAsync(id);
            if (Customer == null)
            {
                return View("NotFound");
            }
            return View(Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerID, CustomerName, CustomerAddress, CustomerPhone")] Customer std)
        {
            if (id != std.CustomerID)
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
                    if (!CustomerExists(std.CustomerID))
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
            var std = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerID == id);
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

            var std = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension != ".xls" && fileExtension != ".xlsx")
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
                        for(int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Customer object
                            var emp = new Customer();
                            //set values for attributes
                            emp.CustomerID = dt.Rows[i][0].ToString();
                            emp.CustomerName = dt.Rows[i][1].ToString();
                            emp.CustomerAddress = dt.Rows[i][2].ToString();
                            emp.CustomerPhone = dt.Rows[i][3].ToString();
                            //add object to Context
                            if (!CustomerExists(emp.CustomerID))
                            {
                                _context.Customers.Add(emp);    
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