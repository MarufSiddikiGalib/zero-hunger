public class UserCredentialsDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // plain on registration, hash in DB
    public string UserType { get; set; }
    public int? RestaurantId { get; set; }
    public int? EmployeeId { get; set; }
}