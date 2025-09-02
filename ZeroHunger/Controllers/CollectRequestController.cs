using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZeroHunger;
using ZeroHunger.DTOs;

namespace ZeroHunger.Controllers
{
    public class CollectRequestController : Controller
    {
        private ZeroHungerEntities db = new ZeroHungerEntities();


        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CollectRequest, CollectRequestDto>().ReverseMap();

            });
            return new Mapper(config);
        }

        // GET: CollectRequest/Create
        public ActionResult Create(int? restaurantId)
        {
            if (restaurantId == null)
            {
                // You may want to redirect or show an error
                return RedirectToAction("Index", "Restaurant");
            }
            var dto = new CollectRequestDto { RestaurantId = restaurantId.Value };
            return View(dto);
        }

        // POST: CollectRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CollectRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                var entity = GetMapper().Map<CollectRequest>(dto);
                entity.Status = "Pending";
                db.CollectRequests.Add(entity);
                db.SaveChanges();
                return RedirectToAction("MyRequests", new { restaurantId = dto.RestaurantId });
            }
            return View(dto);
        }

        // GET: CollectRequest/MyRequests
        public ActionResult MyRequests(int restaurantId)
        {
            var requests = db.CollectRequests.Where(r => r.RestaurantId == restaurantId).ToList();
            //var dtos = _mapper.Map<List<CollectRequestDto>>(requests);
            var dtos = GetMapper().Map<List<CollectRequestDto>>(requests);
            return View(dtos);
        }


        // Everyone can view all requests and their status
        public ActionResult AllRequests()
        {
            var requests = db.CollectRequests.Include("Restaurant").Include("Employee").ToList();
            var dtos = GetMapper().Map<List<CollectRequestDto>>(requests);
            return View(dtos);
        }



    }
}