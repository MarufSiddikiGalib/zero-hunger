using System;

namespace ZeroHunger.DTOs
{
    public class CollectRequestDto
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string FoodDescription { get; set; }
        public DateTime MaxPreserveTime { get; set; }
        public string Status { get; set; }
        public int? AssignedEmployeeId { get; set; }


        // Optionally, include these for display
        public string RestaurantName { get; set; }
        public string EmployeeName { get; set; }
    }
}