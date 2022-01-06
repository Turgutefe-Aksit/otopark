using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTrainProject.Models;
using System.Web.Security;

namespace MVCTrainProject.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security

        car_parkEntities db = new car_parkEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(employee e)
        {
            var info = db.employee.FirstOrDefault(x => x.email == e.email && x.pword == e.pword || e.email == "admin" && e.pword == "admin");
            if (info != null)
            {
                FormsAuthentication.SetAuthCookie(info.email, false);
                return RedirectToAction("Index", "Park");
            }
            else
            {
                ViewBag.Message = "Şifre veya E-posta Hatalı...";
                return View();
            }
        }
    }
}