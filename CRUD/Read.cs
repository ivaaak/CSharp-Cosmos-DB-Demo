using CosmosDemo.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CosmosDemo.Data.Constants;


namespace CosmosDemo.CRUD;

public class Read
{
    public static async Task QueryItemsAsync(CosmosClient cosmosClient)
    {
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);

        string sqlQueryText = "SELECT * FROM c WHERE c.PartitionKey = 'USA'";

        Console.WriteLine($"Running query: {sqlQueryText}\n");

        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);

        List<Student> students = new();

        using FeedIterator<Student> feedIterator = container.GetItemQueryIterator<Student>(queryDefinition);

        double requestUnits = 0;
        while (feedIterator.HasMoreResults)
        {
            FeedResponse<Student> response = await feedIterator.ReadNextAsync();
            students.AddRange(response);
            requestUnits += response.RequestCharge;
        }

        foreach (Student student in students)
        {
            Console.WriteLine($"{student}\n");
        }

        Console.WriteLine($"Queried items: {students.Count}");
        Console.WriteLine($"Request units: {requestUnits}\n");
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
        Console.Clear();
    }
}