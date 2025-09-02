using System;

public class DistributionDto
{
    public int Id { get; set; }
    public int CollectRequestId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime DistributedAt { get; set; }
    public string DistributionDetails { get; set; }
}