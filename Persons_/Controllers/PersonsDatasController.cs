using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persons_.Data;
using Persons_.Models;

namespace Persons_.Controllers
{
    public class PersonsDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonsDatas
        public async Task<IActionResult> Index() // Método que muestra todos los datos en forma de una lista
        {
            return View(await _context.PersonsData.ToListAsync());
        }

        // GET: PersonsDatas/Details/5
        public async Task<IActionResult> Details(int? id) // Método que muestra detalles de una lista
        {
            if (id == null)
            {
                return NotFound();
            }

            var personsData = await _context.PersonsData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (personsData == null)
            {
                return NotFound();
            }

            return View(personsData);
        }

        // GET: PersonsDatas/Create
        public IActionResult Create() // Método para registrar un nuevo dato o persona.
        {
            return View();
        }

        // POST: PersonsDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Age,Sex")] PersonsData personsData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personsData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personsData);
        }

        // GET: PersonsDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personsData = await _context.PersonsData.FindAsync(id);
            if (personsData == null)
            {
                return NotFound();
            }
            return View(personsData);
        }

        // POST: PersonsDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Age,Sex")] PersonsData personsData)
        {
            if (id != personsData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personsData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonsDataExists(personsData.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personsData);
        }

        // GET: PersonsDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personsData = await _context.PersonsData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (personsData == null)
            {
                return NotFound();
            }

            return View(personsData);
        }

        // POST: PersonsDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personsData = await _context.PersonsData.FindAsync(id);
            _context.PersonsData.Remove(personsData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonsDataExists(int id)
        {
            return _context.PersonsData.Any(e => e.ID == id);
        }
    }
}
