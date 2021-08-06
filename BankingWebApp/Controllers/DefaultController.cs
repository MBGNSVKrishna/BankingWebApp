using BankingWebApp.Data;
using BankingWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingWebApp.Controllers
{
    public class DefaultController : Controller
    {
        private readonly BankingWebAppContext _context;

        public DefaultController(BankingWebAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IsCustomer()
        {
            if ( HttpContext.Session.GetString("UserName") != null &&  HttpContext.Session.GetString("Password") != null)
            {
                return RedirectToAction("Services");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IsCustomer(AccountUser u)
        {
            var obj = await _context.AccountUser.Where(a => a.UserName.Equals(u.UserName)).FirstOrDefaultAsync();
            if (obj != null  && obj.UserName == u.UserName  && obj.Password==u.Password)
            {
                HttpContext.Session.SetInt32("UserId", obj.UserId);
                HttpContext.Session.SetString("UserName", obj.UserName.ToString());
               HttpContext.Session.SetString("Balance", obj.Amount.ToString());
                return RedirectToAction("Services");
            }

            else
            {
                ModelState.AddModelError("", "Credentials are not matched");
            }
            return View();
        }

        public ActionResult  Services()
        {
            ViewBag.Id = HttpContext.Session.GetInt32("UserId");
            ViewBag.Uname = HttpContext.Session.GetString("UserName");
            return View();
        }



        public IActionResult Forgot()
        {
            if (HttpContext.Session.GetString("UserId") != null && HttpContext.Session.GetString("FirstName") != null && HttpContext.Session.GetString("Password") != null)
            {
                return RedirectToAction("Edit","AccountUsers");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Forgot(AccountUser u)
        {
            var obj = await _context.AccountUser.Where(a => a.UserId.Equals(u.UserId)).FirstOrDefaultAsync();
            if (obj != null && obj.UserId == u.UserId && obj.FirstName == u.FirstName && obj.Sques==u.Sques && obj.SAns==u.SAns)
            {
                HttpContext.Session.SetInt32("UserId", obj.UserId);
                ViewBag.Id = HttpContext.Session.GetInt32("UserId");
               
                return RedirectToAction("Edit", "AccountUsers", new { id = ViewBag.Id });
            }

            else
            {
                ModelState.AddModelError("", "Credentials are not matched");
            }
            return View();
        }

        
    }
}
