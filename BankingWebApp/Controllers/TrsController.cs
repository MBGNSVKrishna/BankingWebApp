using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingWebApp.Data;
using BankingWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace BankingWebApp.Controllers
{
    public class TrsController : Controller
    {
        private readonly BankingWebAppContext _context;

        public TrsController(BankingWebAppContext context)
        {
            _context = context;
        }
        // GET: Trs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tr.ToListAsync());
        }


        public IActionResult Check()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Check(Datee D)
        {
            if(D!=null)
            {
                HttpContext.Session.SetString("FDate", D.FDate.ToString());
                HttpContext.Session.SetString("TDate", D.ToDate.ToString());
                return RedirectToAction("c1");
            }
            return RedirectToAction("c1");
        }
            
        




        public IActionResult c1()
        {
            var transactions = _context.Tr.Where(c => c.UserID == HttpContext.Session.GetInt32("UserId") && c.TrDate.Date >= Convert.ToDateTime(HttpContext.Session.GetString("FDate")) && c.TrDate.Date <= Convert.ToDateTime(HttpContext.Session.GetString("TDate"))) ;
           
                if (transactions.Count() != 0)
                {
                    return View(transactions);
                }
                else
                {
                    ModelState.AddModelError("", "No product was purchased on this date");
                    return RedirectToAction("noitem");
                }

            }


        // GET: Trs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tr = await _context.Tr
                .FirstOrDefaultAsync(m => m.TrId == id);
            if (tr == null)
            {
                return NotFound();
            }

            return View(tr);
        }

        // GET: Trs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrId,TrDate,amount,UserID")] Tr tr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tr);
        }

        // GET: Trs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tr = await _context.Tr.FindAsync(id);
            if (tr == null)
            {
                return NotFound();
            }
            return View(tr);
        }

        // POST: Trs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrId,TrDate,amount,UserID")] Tr tr)
        {
            if (id != tr.TrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrExists(tr.TrId))
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
            return View(tr);
        }

        // GET: Trs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tr = await _context.Tr
                .FirstOrDefaultAsync(m => m.TrId == id);
            if (tr == null)
            {
                return NotFound();
            }

            return View(tr);
        }

        // POST: Trs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tr = await _context.Tr.FindAsync(id);
            _context.Tr.Remove(tr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrExists(int id)
        {
            return _context.Tr.Any(e => e.TrId == id);
        }
    }
}
