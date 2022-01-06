using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using MVCTrainProject.Models;
using MVCTrainProject.Models.Classes;

namespace MVCTrainProject.Controllers
{
    public class ParkController : Controller
    {
        car_parkEntities db = new car_parkEntities();
        // GET: Park
        public ActionResult Index()
        {
            IEnumClass enumClass = new IEnumClass();
            var values = db.park_place.ToList();
            enumClass.Park_Places = values.ToList();
            return View(enumClass.Park_Places);

        }

        [HttpGet]
        public ActionResult Park_Registration()
        {
            IEnumClass cs = new IEnumClass();
            cs.CarEnum = db.car.ToList();
            cs.ParkEnum = db.park_place.ToList();

            List<SelectListItem> carValues = (from i in db.car
                                              where i.situation == null
                                              select new SelectListItem
                                              {
                                                  Text = i.car_plate,
                                                  Value = i.car_plate.ToString()
                                              }).ToList();
            ViewBag.vl = carValues;

            List<SelectListItem> parkValues = (from i in db.park_place
                                               where i.occupancy_info == "BOŞ"
                                               select new SelectListItem
                                               {

                                                   Text = i.loc_number.ToString(),
                                                   Value = i.loc_number.ToString()
                                               }
                                               ).ToList();
            ViewBag.vl2 = parkValues;

            return View();
        }

        [HttpPost]
        public ActionResult Park_Registration(IEnumClass cs)
        {
            registration_date r = new registration_date();

            var car_id = from c in db.car
                         where c.car_plate == cs.carClass.car_plate
                         select c;


            var park = from p in db.park_place
                       where p.loc_number == cs.parkClass.loc_number
                       select p;
            car car2 = new car();
            car2 = car_id.FirstOrDefault();

            park_place park2 = new park_place();
            park2 = park.FirstOrDefault();

            var park3 = db.park_place.Find(park2.loc_number);
            park3.occupancy_info = "DOLU";

            r.car_id = car2.car_id;
            r.loc_number = cs.parkClass.loc_number;
            r.entery_time = DateTime.Now;

            r.park_place = park3;

            db.registration_date.Add(r);
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");


        }


        public ActionResult ParkList()
        {
            var values = db.registration_date.ToList();
            return View(values);
        }

        public ActionResult Registration_Delete()
        {
            var values = db.registration_date.ToList();
            return View(values);
        }

        public ActionResult End_Process(int id)
        {
            var item = db.registration_date.Find(id);

            var park = db.park_place.Find(item.loc_number);
            park.occupancy_info = "BOŞ";
            item.park_place = park;
            item.leave_time = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult CustomerIndex()
        {
            IEnumClass enumClass = new IEnumClass();
            var values = db.park_place.ToList();
            enumClass.Park_Places = values.ToList();
            return View(enumClass.Park_Places);
        }

    }
}