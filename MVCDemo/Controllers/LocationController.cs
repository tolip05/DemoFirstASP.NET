using MVCDemo.Data;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class LocationController : Controller
    {

        private MVCDemoContext db = new MVCDemoContext();
        // GET: Location
        public ActionResult Index()
        {
        //    var result = db.Locations.ToList();
            return View(db.Locations.OrderBy(l => l.Name).ToList());
        }

        public ActionResult Location()
        {
            return View();
        }

        public ActionResult ErrorUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location([Bind(Include = "Name,Latitude,Longitude,Identifier,Emeil")] LocationModel model)
        {
            Forwarder forwarder = null;

            try
            {
                forwarder = db.Fowarders.Where(f => f.ForwarderIdentificator == model.Identifier
                && f.Emeil == model.Emeil).First();
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorUser");
                throw;
            }
            
            if (forwarder != null)
            {
                Location location = new Location();
                location.Name = model.Name;
                location.Latitude = model.Latitude;
                location.Longitude = model.Longitude;
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location loc = db.Locations.Find(id);
            if (loc == null)
            {
                return HttpNotFound();
            }
            return View(loc);
        }
    }
}