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
    public class FDsController : Controller
    {
        private readonly BankingWebAppContext _context;

        public FDsController(BankingWebAppContext context)
        {
            _context = context;
        }

        // GET: FDs
        public async Task<IActionResult> Index()
        {
            return View(await _context.FD.ToListAsync());
        }

        // GET: FDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fD = await _context.FD
                .FirstOrDefaultAsync(m => m.FdId == id);
            if (fD == null)
            {
                return NotFound();
            }

            return View(fD);
        }

        // GET: FDs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FdId,FdInvMon,Month,UserID,FdMAmount,FdInMoney")] FD fD)
        {
            if (ModelState.IsValid)
            {
                fD.UserID=(int) HttpContext.Session.GetInt32("UserId");
               
                double m = fD.Month;
                int v = (int)m;
                HttpContext.Session.SetInt32("Tenure",v);
                double amount = fD.FdInvMon;
                if(m<6)
                {
                    fD.FdMAmount = amount + (amount * 0.04);
                }
                else if(m>=6 &&m<12)
                {
                    fD.FdMAmount = amount + (amount * 0.054);
                }

                else if (m >= 12 && m < 24)
                {
                    fD.FdMAmount = amount + (amount * 0.057);
                }

                else if (m >= 24 && m < 36)
                {
                    fD.FdMAmount = amount + (amount * 0.06);
                }

                else if (m >= 36 && m < 70)
                {
                    fD.FdMAmount = amount + (amount * 0.063);
                }
                else if (m >= 70 && m < 1200)
                {
                    fD.FdMAmount = amount + (amount * 0.07);
                }

                fD.FdInMoney = fD.FdMAmount - amount;
                int conamount = (int)fD.FdMAmount;
                int inamount = (int)fD.FdInMoney;

                HttpContext.Session.SetInt32("MAmount", conamount);
                HttpContext.Session.SetInt32("IntAmount", inamount);

                _context.Add(fD);
                await _context.SaveChangesAsync();
                //                return RedirectToAction(nameof(Index));

                return RedirectToAction("FDSuccess");
            }
            return View(fD);
        }

        // GET: FDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fD = await _context.FD.FindAsync(id);
            if (fD == null)
            {
                return NotFound();
            }
            return View(fD);
        }

        // POST: FDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FdId,FdInvMon,Month,UserID,FdMAmount,FdInMoney")] FD fD)
        {
            if (id != fD.FdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FDExists(fD.FdId))
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
            return View(fD);
        }

        // GET: FDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fD = await _context.FD
                .FirstOrDefaultAsync(m => m.FdId == id);
            if (fD == null)
            {
                return NotFound();
            }

            return View(fD);
        }

        // POST: FDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fD = await _context.FD.FindAsync(id);
            _context.FD.Remove(fD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FDExists(int id)
        {
            return _context.FD.Any(e => e.FdId == id);
        }

        public IActionResult FDSuccess ()
        {
            ViewBag.Amount = HttpContext.Session.GetInt32("MAmount");
            ViewBag.IntAmount=HttpContext.Session.GetInt32("IntAmount");
            ViewBag.Month = HttpContext.Session.GetInt32("Tenure");
            return View();
        }

    }
}
