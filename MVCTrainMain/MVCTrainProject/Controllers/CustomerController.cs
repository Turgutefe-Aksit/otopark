using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTrainProject.Models;
using MVCTrainProject.Models.Classes;
namespace MVCTrainProject.Controllers
{

    public class CustomerController : Controller
    {
        // GET: Customer
        car_parkEntities db = new car_parkEntities();
        public ActionResult Index()
        {
            IEnumClass enumClass = new IEnumClass();
            var values = db.customer.ToList().Where(c => c.situation == null);
            enumClass.CustomerList = values.ToList();
            return View(enumClass.CustomerList);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(customer c)
        {
            db.customer.Add(c);
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var item = db.customer.Find(id);
            item.situation = 1;

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult BringCustomer(int id)
        {
            var cus = db.customer.Find(id);
            return View("BringCustomer", cus);
        }

        public ActionResult UpdateCustomer(customer c)
        {
            var customer = db.customer.Find(c.customer_id);
            customer.fname = c.fname;
            customer.lname = c.lname;
            customer.phone_number = c.phone_number;
            customer.email = c.email;
            customer.pword = c.pword;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}