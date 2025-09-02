using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ZeroHunger;
using ZeroHunger.DTOs;
using AutoMapper;

namespace ZeroHunger.Controllers
{
    public class EmployeeManageController : Controller
    {
        private ZeroHungerEntities db = new ZeroHungerEntities();

        private static IMapper _mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
        }).CreateMapper();

        // READ: List all employees
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            var dtos = _mapper.Map<List<EmployeeDto>>(employees);
            return View(dtos);
        }

        // CREATE: Show create form
        public ActionResult Create()
        {
            return View();
        }

        // CREATE: Handle create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto dto)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Employee>(dto);
                db.Employees.Add(entity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // UPDATE: Show edit form
        public ActionResult Edit(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null) return HttpNotFound();
            var dto = _mapper.Map<EmployeeDto>(employee);
            return View(dto);
        }

        // UPDATE: Handle edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDto dto)
        {
            if (ModelState.IsValid)
            {
                var employee = db.Employees.Find(dto.Id);
                if (employee == null) return HttpNotFound();

                employee.Name = dto.Name;
                employee.ContactInfo = dto.ContactInfo;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // DELETE: Show delete confirmation
        public ActionResult Delete(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null) return HttpNotFound();
            var dto = _mapper.Map<EmployeeDto>(employee);
            return View(dto);
        }

        // DELETE: Handle delete post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null) return HttpNotFound();
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}