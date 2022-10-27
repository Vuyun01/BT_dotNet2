using TranBaVuBTH2.Data;
using TranBaVuBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}