using CosmosDemo.Models;
using System;

namespace CosmosDemo.Data;

public class Students
{
    public static Student jonSkeet = new Student
    {
        Id = "8a4e944e-061c-4daf-9daf-d062f1234dd5",
        PartitionKey = "USA",
        Username = "jon.skeet",
        FirstName = "Jon",
        LastName = "Skeet",
        Address = new Address { Street = "5th Avenue", City = "New York", Country = "USA" },
        Courses = new Course[]
            {
                new Course
                {
                    Name = "C# Basics",
                    Trainer = "Svetlin Nakov",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1)
                },
                new Course
                {
                    Name = "C# Fundamentials",
                    Trainer = "Svetlin Nakov",
                    StartDate = DateTime.UtcNow.AddMonths(2),
                    EndDate = DateTime.UtcNow.AddMonths(3)
                }
            }
    };

    public static Student bjarneStroustrup = new Student
    {
        Id = "1669e6aa-b695-4346-9985-8b6c81a33b26",
        PartitionKey = "Denmark",
        Username = "bjarne.stroustrup",
        FirstName = "Bjarne",
        LastName = "Stroustrup",
        Address = new Address { Street = "Solstien", City = "Aarhus", Country = "Denmark" },
        Courses = new Course[]
            {
                new Course
                {
                    Name = "C++ Basics",
                    Trainer = "George Georgiev",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1)
                }
            }
    };

    public static Student robertMartin = new Student
    {
        Id = "7a1ec9a9-b6f7-437f-a541-f36c29282816",
        PartitionKey = "USA",
        Username = "robert.martin",
        FirstName = "Robert",
        LastName = "Martin",
        Address = new Address { Street = "Sunset Blvd.", City = "Los Angeles", Country = "USA" },
        Courses = new Course[]
           {
                new Course
                {
                    Name = "Clojure Basics",
                    Trainer = "Angel Georgiev",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1)
                }
           }
    };
}