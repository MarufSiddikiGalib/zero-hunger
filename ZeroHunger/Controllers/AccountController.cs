using System.Web.Mvc;
using ZeroHunger;
using System.Linq;

public class AccountController : Controller
{
    private ZeroHungerEntities db = new ZeroHungerEntities();

    public ActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(string username, string password)
    {
        var user = db.UserCredentials.SingleOrDefault(u => u.Username == username);
        if (user != null && user.PasswordHash == password) // Use hash check!
        {
            // Set session variables
            Session["UserId"] = user.Id;
            Session["UserType"] = user.UserType;
            Session["RestaurantId"] = user.RestaurantId;
            Session["EmployeeId"] = user.EmployeeId;

            // Redirect based on role
            switch (user.UserType)
            {
                case "Admin": return RedirectToAction("PendingRequests", "Admin");
                case "Restaurant": return RedirectToAction("MyRequests", "CollectRequest", new { restaurantId = user.RestaurantId });
                case "Employee": return RedirectToAction("AssignedRequests", "Employee", new { employeeId = user.EmployeeId });
            }
        }
        ViewBag.Error = "Invalid username or password";
        return View();
    }

    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Login");
    }
}