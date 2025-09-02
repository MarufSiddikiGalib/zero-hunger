using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZeroHunger;
using ZeroHunger.DTOs;

namespace ZeroHunger.Controllers
{
    public class AdminController : Controller
    {
        private ZeroHungerEntities db = new ZeroHungerEntities();

        private static IMapper _mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<CollectRequest, CollectRequestDto>().ReverseMap();
            cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
        }).CreateMapper();

        // 1. View all pending collect requests
        public ActionResult PendingRequests()
        {
            var requests = db.CollectRequests
                .Where(r => r.Status == "Pending")
                .ToList();
            var dtos = _mapper.Map<List<CollectRequestDto>>(requests);
            return View(dtos);
        }

        // 2. Assign employee to a request (GET: show assign form)
        public ActionResult AssignEmployee(int id)
        {
            var request = db.CollectRequests.Find(id);
            if (request == null) return HttpNotFound();

            var employees = db.Employees.ToList();
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            ViewBag.Employees = new SelectList(employeeDtos, "Id", "Name");
            var requestDto = _mapper.Map<CollectRequestDto>(request);
            return View(requestDto);
        }

        // 3. Assign employee to a request (POST: save assignment)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignEmployee(int id, int AssignedEmployeeId)
        {
            var request = db.CollectRequests.Find(id);
            if (request == null) return HttpNotFound();

            request.AssignedEmployeeId = AssignedEmployeeId;
            request.Status = "Assigned";
            db.SaveChanges();

            return RedirectToAction("PendingRequests");
        }
    }
}