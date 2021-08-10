using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingWebApp.Data;
using BankingWebApp.Models;

namespace BankingWebApp.Controllers
{
    public class DateesController : Controller
    {
        private readonly BankingWebAppContext _context;

        public DateesController(BankingWebAppContext context)
        {
            _context = context;
        }

        // GET: Datees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Datee.ToListAsync());
        }

        // GET: Datees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datee = await _context.Datee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (datee == null)
            {
                return NotFound();
            }

            return View(datee);
        }

        // GET: Datees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Datees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FDate,ToDate")] Datee datee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(datee);
        }

        // GET: Datees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datee = await _context.Datee.FindAsync(id);
            if (datee == null)
            {
                return NotFound();
            }
            return View(datee);
        }

        // POST: Datees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FDate,ToDate")] Datee datee)
        {
            if (id != datee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DateeExists(datee.ID))
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
            return View(datee);
        }

        // GET: Datees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datee = await _context.Datee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (datee == null)
            {
                return NotFound();
            }

            return View(datee);
        }

        // POST: Datees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datee = await _context.Datee.FindAsync(id);
            _context.Datee.Remove(datee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DateeExists(int id)
        {
            return _context.Datee.Any(e => e.ID == id);
        }
    }
}
