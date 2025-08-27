using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ZeroHunger; // Your namespace
using ZeroHunger.DTOs; // DTO namespace

namespace ZeroHunger.Controllers
{
    public class RestaurantController : Controller
    {
        private ZeroHungerEntities db = new ZeroHungerEntities();

        // Mapper config as static field (one-time setup, used everywhere)
       
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantDto>().ReverseMap();
                
            });
            return new Mapper(config);
        }




        // GET: Restaurant
        public ActionResult Index()
        {
            var restaurants = db.Restaurants.ToList();
            var dtos = GetMapper().Map<List<RestaurantDto>>(restaurants);
            return View(dtos); // View should use IEnumerable<RestaurantDTO>
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null) return HttpNotFound();
            var dto = GetMapper().Map<RestaurantDto>(restaurant);
            return View(dto);
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantDto dto)
        {
            if (ModelState.IsValid)
            {
                var restaurant = GetMapper().Map<Restaurant>(dto);
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null) return HttpNotFound();
            var dto = GetMapper().Map<RestaurantDto>(restaurant);
            return View(dto);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RestaurantDto dto)
        {
            if (ModelState.IsValid)
            {
                var restaurant = GetMapper().Map<Restaurant>(dto);
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null) return HttpNotFound();
            var dto = GetMapper().Map<RestaurantDto>(restaurant);
            return View(dto);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}