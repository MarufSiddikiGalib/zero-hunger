using System.Web.Mvc;
using ZeroHunger;
using ZeroHunger.DTOs;

public class RegisterController : Controller
{
    private ZeroHungerEntities db = new ZeroHungerEntities();

    // Restaurant Registration
    public ActionResult Restaurant() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Restaurant(RestaurantDto dto, string username, string password)
    {
        // Save Restaurant
        var restaurant = new Restaurant { Name = dto.Name, ContactInfo = dto.ContactInfo, Address = dto.Address };
        db.Restaurants.Add(restaurant);
        db.SaveChanges();

        // Save UserCredentials
        var user = new UserCredentials
        {
            Username = username,
            PasswordHash = HashPassword(password),
            UserType = "Restaurant",
            RestaurantId = restaurant.Id
        };
        db.UserCredentials.Add(user);
        db.SaveChanges();

        return RedirectToAction("Login", "Account");
    }

    // Employee Registration
    public ActionResult Employee() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Employee(EmployeeDto dto, string username, string password)
    {
        var employee = new Employee { Name = dto.Name, ContactInfo = dto.ContactInfo };
        db.Employees.Add(employee);
        db.SaveChanges();

        var user = new UserCredentials
        {
            Username = username,
            PasswordHash = HashPassword(password),
            UserType = "Employee",
            EmployeeId = employee.Id
        };
        db.UserCredentials.Add(user);
        db.SaveChanges();

        return RedirectToAction("Login", "Account");
    }

    private string HashPassword(string password)
    {
        // Use a real hashing function here, e.g., SHA256
        return password; // Replace with hash
    }
}