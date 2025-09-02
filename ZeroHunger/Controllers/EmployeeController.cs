using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZeroHunger;
using ZeroHunger.DTOs;
using AutoMapper;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        private ZeroHungerEntities db = new ZeroHungerEntities();

        private static IMapper _mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<CollectRequest, CollectRequestDto>().ReverseMap();
            cfg.CreateMap<Distribution, DistributionDto>().ReverseMap();
        }).CreateMapper();

        // 1. See assigned requests
        public ActionResult AssignedRequests(int employeeId)
        {
            var requests = db.CollectRequests
                .Where(r => r.AssignedEmployeeId == employeeId && r.Status == "Assigned")
                .ToList();
            var dtos = _mapper.Map<List<CollectRequestDto>>(requests);
            return View(dtos);
        }

        // 2. Mark as Collected/Distributed (GET: show form)
        public ActionResult MarkDistributed(int id)
        {
            var request = db.CollectRequests.Find(id);
            if (request == null) return HttpNotFound();

            var dto = _mapper.Map<CollectRequestDto>(request);
            return View(dto);
        }

        // 2b. Mark as Collected/Distributed (POST: update status, optionally create Distribution)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkDistributed(int id, string distributionDetails)
        {
            var request = db.CollectRequests.Find(id);
            if (request == null) return HttpNotFound();

            request.Status = "Completed";
            db.SaveChanges();

            // Optionally, add a distribution record
            var distribution = new Distribution
            {
                CollectRequestId = request.Id,
                EmployeeId = request.AssignedEmployeeId ?? 0,
                DistributedAt = DateTime.Now,
                DistributionDetails = distributionDetails
            };
            db.Distributions.Add(distribution);
            db.SaveChanges();

            return RedirectToAction("AssignedRequests", new { employeeId = request.AssignedEmployeeId });
        }
    }
}