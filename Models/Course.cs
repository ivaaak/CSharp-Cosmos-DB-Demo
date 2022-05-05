using System;

namespace CosmosDemo.Models;

public class Course
{
    public string Name { get; set; }
    public string Trainer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
