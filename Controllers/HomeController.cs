using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvccore.DB_Context;
using mvccore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mvccore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            mbvdatabaseContext obj = new mbvdatabaseContext();
            var res = obj.Mytables.ToList();

            return View(res);
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Table obj)
        {
            mbvdatabaseContext obj1 = new mbvdatabaseContext();
            var log = obj1.Tables.Where(m => m.Email == obj.Email).FirstOrDefault();
            if (log == null)
            {

            }
            else
            {
                if (log.Email == obj.Email && log.Password == obj.Password)
                {
                    HttpContext.Session.SetString("Name", log.Email);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pass"] = "password feild";
                }
            }
            
            return View();
        }
        public ActionResult form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult form(Class obj1)
        {
            mbvdatabaseContext obj = new mbvdatabaseContext();
            Mytable obj2 = new Mytable();
            obj2.Id = obj1.Id;
            obj2.Name = obj1.Name;
            obj2.Email = obj1.Email;
            if (obj1.Id == 0)
            {
                obj.Mytables.Add(obj2);
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                obj.Entry(obj2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                obj.SaveChanges();
                return RedirectToAction("Index");
            }

          
        }
        public ActionResult Regestation()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Regestation(Class1 obj1)
        {
            mbvdatabaseContext obj = new mbvdatabaseContext();
            Table obj2 = new Table();
            obj2.Id = obj1.Id;
            obj2.Name = obj1.Name;
            obj2.Email = obj1.Email;
            obj2.Password = obj1.Password;
            obj.Tables.Add(obj2);
            obj.SaveChanges();
            return RedirectToAction("login");
        }

        public ActionResult delete(int Id)
        {
            mbvdatabaseContext obj = new mbvdatabaseContext();
            var delete = obj.Mytables.Where(x => x.Id == Id).FirstOrDefault();
            obj.Mytables.Remove(delete);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult edit(int Id)
        {
            Mytable edi = new Mytable();
            mbvdatabaseContext obj = new mbvdatabaseContext();
            var dit = obj.Mytables.Where(x => x.Id == Id).First();
            edi.Id = dit.Id;

            edi.Name = dit.Name;
            edi.Email = dit.Email;
            return View("form", edi);
        }
        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
