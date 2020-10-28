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
    public class CargoController : Controller
    {
        private MVCDemoContext db = new MVCDemoContext();
        private List<CargoView> cargoesViews;
        // GET: Cargo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorUser()
        {
            return View();
        }
        public ActionResult AddedCargo()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Where(c => c.CargoId == id).First();
            if (cargo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoModel model = new CargoModel();
            model.Id = cargo.CargoId;
            model.Name = cargo.Name;
            model.PricePerKilometer = cargo.PricePerKilometer;
            Location fromLocation = db.Locations.Where(l => l.LocationId == cargo.FromLocationId).First();
            Location forLocation = db.Locations.Where(l => l.LocationId == cargo.ForLocationId).First();
            model.FromLocation = fromLocation.Name;
            model.ForLocation = forLocation.Name;
            model.Weight = cargo.Weight;
            model.CargoType = cargo.CargoType.ToString();
            model.TransportType = cargo.TransportType.ToString();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Id,Identifier,Emeil")] CargoModel model)
        {
            Carrier carrier = null;
            try
            {
            carrier = db.Carriers.Where(c => c.CarrierIdentificator == model.Identifier
            && c.Emeil == model.Emeil).First();
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("ErrorUser");
                throw;
            }
            
            Cargo cargo = db.Cargoes.Where(c => c.CargoId == model.Id).First();
            if (carrier == null)
            {
                return RedirectToAction("AllCargoes");
            }
            carrier.Cargoes.Add(cargo);
            cargo.CarrierId = carrier.CarrierId;
            db.SaveChanges();
            return RedirectToAction("AddedCargo");
        }

        public ActionResult Check()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Check([Bind(Include = "Key,Emeil")] IdentifierKey model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var emeil = model.Emeil;
            try
            {
                var result = db.Fowarders.Where(x =>
          x.ForwarderIdentificator == model.Key && x.Emeil == emeil)
              .First();
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorUser");
                throw;
            }
          
            
             return RedirectToAction("Cargo");
            
            
        }
        public ActionResult Cargo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cargo([Bind(Include = "Name,PricePerKilometer,Weight," +
            "FromLocation,ForLocation,CargoType,TransportType,Identifier,Emeil")] CargoModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cargo = new Cargo();
            Location fromLocation = db.Locations.Where(l => l.Name == model.FromLocation).First();
            Location forLocation = db.Locations.Where(l => l.Name == model.ForLocation).First();
            if (fromLocation == null || forLocation == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cargo.Name = model.Name;
            cargo.PricePerKilometer = model.PricePerKilometer;
            cargo.Weight = model.Weight;
            cargo.FromLocationId = fromLocation.LocationId;
            cargo.ForLocationId = forLocation.LocationId;

            if (model.CargoType.ToLower() == CargoType.Animal.ToString().ToLower())
            {
                cargo.CargoType = CargoType.Animal;
            }
            else if (model.CargoType.ToLower() == CargoType.Food.ToString().ToLower())
            {
                cargo.CargoType = CargoType.Food;
            }
            else if (model.CargoType.ToLower() == CargoType.Liquid.ToString().ToLower())
            {
                cargo.CargoType = CargoType.Liquid;
            }
            else if (model.CargoType.ToLower() == CargoType.Dangerous.ToString().ToLower())
            {
                cargo.CargoType = CargoType.Dangerous;
            }
            Forwarder forwarder = db.Fowarders.Where(f => f.ForwarderIdentificator == model.Identifier && f.Emeil == model.Emeil).First();

            if (forwarder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cargo.ForwarderId = forwarder.ForwarderId;
            if (model.TransportType.ToLower() == Models.TransportType.Air.ToString().ToLower())
            {
                cargo.TransportType = Models.TransportType.Air;
            }
            else if (model.TransportType.ToLower() == Models.TransportType.Railway.ToString().ToLower())
            {
                cargo.TransportType = Models.TransportType.Railway;
            }
            else if (model.TransportType.ToLower() == Models.TransportType.Water.ToString().ToLower())
            {
                cargo.TransportType = Models.TransportType.Water;
            }
            else if (model.TransportType.ToLower() == Models.TransportType.Truck.ToString().ToLower())
            {
                cargo.TransportType = Models.TransportType.Truck;
            }
            if (cargo != null)
            {
                db.Cargoes.Add(cargo);
                db.SaveChanges();
                Cargo cargoFromForwarder = db.Cargoes.Where(c => c.ForwarderId == forwarder.ForwarderId).First();
                forwarder.Cargoes.Add(cargoFromForwarder);
                db.SaveChanges();
                return RedirectToAction("AllCargoes");
            }
            return View();
        }

        public ActionResult AllCargoes()
        {
            List<Cargo> cargoes = db.Cargoes.Where(c => c.CarrierId < 1 || c.CarrierId == null).ToList();
            List<CargoView> cargoViews = new List<CargoView>();
            foreach (var cargo in cargoes)
            {
                CargoView cargoView = new CargoView();
                cargoView.Id = cargo.CargoId;
                cargoView.Name = cargo.Name;
                cargoView.PricePerKilometer = cargo.PricePerKilometer;
                cargoView.Weight = cargo.Weight;
                Location fromLocation = db.Locations.Where(l => l.LocationId == cargo.FromLocationId).First();
                Location forLocation = db.Locations.Where(l => l.LocationId == cargo.ForLocationId).First();
                cargoView.FromLocation = fromLocation.Name;
                cargoView.ForLocation = forLocation.Name;
                Forwarder forwarder = db.Fowarders.Where(f => f.ForwarderId == cargo.ForwarderId).First();
                cargoView.CargoType = cargo.CargoType.ToString();
                cargoView.TransportType = cargo.TransportType.ToString();
                cargoView.Forwarder = forwarder.Name;
                cargoViews.Add(cargoView);
            }
            return View(cargoViews);
        }
        public ActionResult MyCargoes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyCargoes([Bind(Include = "Id,Emeil")] ModelWithIdAndEmeil model)
        {
            Forwarder forwarder = null;
            Carrier carrier = null;
            try
            {
                forwarder = db.Fowarders.Where(f => f.ForwarderIdentificator == model.Id
                && f.Emeil == model.Emeil).First();
            }
            catch (Exception)
            {
                forwarder = null;
            }
            try
            {
                carrier = db.Carriers.Where(c => c.CarrierIdentificator == model.Id &&
                c.Emeil == model.Emeil).First();
            }
            catch (Exception)
            {
                carrier = null;
            }
            if (forwarder == null && carrier == null)
            {
                return RedirectToAction("ErrorUser");
            }

            List<Cargo> cargoes = new List<Cargo>();
            if (forwarder != null)
            {
                cargoes = db.Cargoes.Where(c => c.ForwarderId == forwarder.ForwarderId).ToList();
            }
            else
            {
                cargoes = db.Cargoes.Where(c => c.CarrierId == carrier.CarrierId).ToList();
            }
            ModelStaticList.cargoModels = new List<CargoView>();
            foreach (var cargo in cargoes)
            {
                CargoView cargoView = new CargoView();
                cargoView.Id = cargo.CargoId;
                cargoView.Name = cargo.Name;
                cargoView.PricePerKilometer = cargo.PricePerKilometer;
                cargoView.Weight = cargo.Weight;
                Location fromLocation = db.Locations.Where(l => l.LocationId == cargo.FromLocationId).First();
                Location forLocation = db.Locations.Where(l => l.LocationId == cargo.ForLocationId).First();
                cargoView.FromLocation = fromLocation.Name;
                cargoView.ForLocation = forLocation.Name;
                cargoView.CargoType = cargo.CargoType.ToString();
                cargoView.TransportType = cargo.TransportType.ToString();
                Forwarder forwarderIn = db.Fowarders.Where(f => f.ForwarderId == cargo.ForwarderId).First();
                cargoView.Forwarder = forwarderIn.Name;
                ModelStaticList.cargoModels.Add(cargoView);
            }
            return RedirectToAction("AllMyCargoes");
        }
        public ActionResult AllMyCargoes()
        {
            return View(ModelStaticList.cargoModels);
        }

        public ActionResult SearchCargo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchCargo([Bind
            (Include = "Name,PricePerKilometer,Weight,FromLocation,ForLocation,CargoType,TransportType")] SearchModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location fromLocation = null;
            Location forLocation = null;
            try
            {
                fromLocation = db.Locations.Where(l => l.Name == model.FromLocation).First();
            }
            catch (Exception)
            {
                fromLocation = null;
            }
            try
            {
                forLocation = db.Locations.Where(l => l.Name == model.ForLocation).First();
            }
            catch (Exception)
            {
                forLocation = null;
            }
            if (model.Name == null || model.Name == "")
            {
                model.Name = "empty";
            }
            if (model.PricePerKilometer < 1 || model.PricePerKilometer == null)
            {
                model.PricePerKilometer = 0;
            }
            if (model.Weight == null || model.Weight < 1)
            {
                model.Weight = 0;
            }
            if (fromLocation == null)
            {
                fromLocation = new Location();
                fromLocation.LocationId = 0;
            }
            if (forLocation == null)
            {
                forLocation = new Location();
                forLocation.LocationId = 0;
            }
            if (model.TransportType == null || model.TransportType == "")
            {
                model.TransportType = "empty";
            }
            if (model.CargoType == null || model.CargoType == "")
            {
                model.CargoType = "empty";
            }
            List<Cargo> cargoes = new List<Cargo>();
            cargoes = db.Cargoes.Where(c => (c.Name == model.Name || model.Name == "empty")
            && (model.PricePerKilometer == 0 || c.PricePerKilometer == model.PricePerKilometer)
            && (model.Weight == 0 || c.Weight == model.Weight)
            && (fromLocation.LocationId == 0 || c.FromLocationId == fromLocation.LocationId)
            && (forLocation.LocationId == 0 || c.ForLocationId == forLocation.LocationId)
            && (model.TransportType == "empty" || c.TransportType.ToString() == model.TransportType)
            && (model.CargoType == "empty" || c.CargoType.ToString() == model.CargoType)).ToList();
            List<Cargo> results = cargoes.Where(c => c.CarrierId == null).ToList();
            ModelStaticList.cargoModels = new List<CargoView>();
            foreach (var cargo in results)
            {
                CargoView cargoView = new CargoView();
                cargoView.Id = cargo.CargoId;
                cargoView.Name = cargo.Name;
                cargoView.PricePerKilometer = cargo.PricePerKilometer;
                cargoView.Weight = cargo.Weight;
                Location fromLoc = db.Locations.Where(l => l.LocationId == cargo.FromLocationId).First();
                Location forLoc = db.Locations.Where(l => l.LocationId == cargo.ForLocationId).First();
                cargoView.FromLocation = fromLoc.Name;
                cargoView.ForLocation = forLoc.Name;
                cargoView.CargoType = cargo.CargoType.ToString();
                cargoView.TransportType = cargo.TransportType.ToString();
                Forwarder forwarderIn = db.Fowarders.Where(f => f.ForwarderId == cargo.ForwarderId).First();
                cargoView.Forwarder = forwarderIn.Name;
                ModelStaticList.cargoModels.Add(cargoView);
            }
            return RedirectToAction("SearchedCargoes");
        }
        public ActionResult SearchedCargoes()
        {
            return View(ModelStaticList.cargoModels);
        }
    }

}