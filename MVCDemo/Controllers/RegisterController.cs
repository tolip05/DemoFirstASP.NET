using MVCDemo.Data;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class RegisterController : Controller
    {

        private MVCDemoContext db = new MVCDemoContext();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }


        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name,TypeUser,Emeil,IdentidierNumber")] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TypeUser.ToLower() == "forwarder")
                {
                    var forwarder = new Forwarder();
                    forwarder.Name = model.Name;
                    forwarder.Emeil = model.Name;
                    forwarder.ForwarderIdentificator = model.IdentidierNumber;
                    db.Fowarders.Add(forwarder);
                    db.SaveChanges();
                    return RedirectToAction("Registered");
                }
                else if (model.TypeUser.ToLower() == "carrier")
                {
                    var carrier = new Carrier();
                    carrier.Name = model.Name;
                    carrier.Emeil = model.Emeil;
                    carrier.CarrierIdentificator = model.IdentidierNumber;
                    db.Carriers.Add(carrier);
                    db.SaveChanges();
                    return RedirectToAction("Registered");
                }
            }

            return RedirectToAction("Register");
        }
        // GET: Register
        public ActionResult Registered()
        {
            return View();
        }
    }
}