using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTrainProject.Models;
using MVCTrainProject.Models.Classes;
namespace MVCTrainProject.Controllers
{
    public class CarController : Controller
    {
        // GET: AddCar
        car_parkEntities db = new car_parkEntities();
        public ActionResult CarList()
        {
            IEnumClass enumClass = new IEnumClass();
            var values = db.car.ToList().Where(c => c.situation == null);
            enumClass.CarList = values.ToList();
            return View(enumClass.CarList);
        }
        [HttpGet]
        public ActionResult AddCar(int id)
        {
            var cus = db.customer.Find(id);
            return View("AddCar", cus);
        }

        [HttpPost]
        public ActionResult AddCar(car c)
        {
            db.car.Add(c);
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var item = db.car.Find(id);
            item.situation = 1;

            db.SaveChanges();
            return RedirectToAction("CarList");
        }
    }
}