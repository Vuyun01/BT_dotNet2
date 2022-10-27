using TranBaVuBTH2.Data;
using TranBaVuBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TranBaVuBTH2.Controllers{
    public class EmployeeController : Controller{
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var model = await _context.Employees.ToListAsync();
            return View(model);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp){
            if(ModelState.IsValid){
                _context.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return View("NotFound");
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeID, EmployeeName, EmployeeAddress, EmployeeEmail, EmployeePhone")] Employee std)
        {
            if (id != std.EmployeeID)
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
                    if (!EmployeeExists(std.EmployeeID))
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
            var std = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeID == id);
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

            var std = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}