using CosmosDemo.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static CosmosDemo.Data.Constants;
using static CosmosDemo.Data.Students;

namespace CosmosDemo.CRUD;

public class Update
{
    public static async Task AddItemsAsync(CosmosClient cosmosClient)
    {
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
  

        ItemResponse<Student> jonSkeetResponse =
            await container.CreateItemAsync<Student>(jonSkeet, new PartitionKey(jonSkeet.PartitionKey));
        Console.WriteLine($"Created item in database with id: {jonSkeetResponse.Resource.Id}");
        Console.WriteLine($"Operation consumed {jonSkeetResponse.RequestCharge} RUs.\n");


        ItemResponse<Student> bjarneStroustrupResponse =
            await container.CreateItemAsync<Student>(bjarneStroustrup, new PartitionKey(bjarneStroustrup.PartitionKey));
        Console.WriteLine($"Created item in database with id: {bjarneStroustrupResponse.Resource.Id}");
        Console.WriteLine($"Operation consumed {bjarneStroustrupResponse.RequestCharge} RUs.\n");


        ItemResponse<Student> robertMartinResponse =
            await container.CreateItemAsync<Student>(robertMartin, new PartitionKey(robertMartin.PartitionKey));
        Console.WriteLine($"Created item in database with id: {robertMartinResponse.Resource.Id}");
        Console.WriteLine($"Operation consumed {robertMartinResponse.RequestCharge} RUs.\n");
    }

    public static async Task UpdateItemAsync(CosmosClient cosmosClient)
    {
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);

        ItemResponse<Student> getResponse =
            await container.ReadItemAsync<Student>("1669e6aa-b695-4346-9985-8b6c81a33b26", new PartitionKey("Denmark"));

        Student student = getResponse.Resource;

        student.Address.Street = "Todor Kableshkov";

        ItemResponse<Student> updateResponse =
            await container.ReplaceItemAsync<Student>(
                student,
                student.Id,
                new PartitionKey(student.PartitionKey),
                new ItemRequestOptions { IfMatchEtag = student.Etag });
        Console.WriteLine($"Updated Student: {updateResponse.Resource}\n");
    }

    public static async Task ConcurrencyUpdateAsync(CosmosClient cosmosClient)
    {
        Console.WriteLine("Starting concurrency update.");
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);

        Task task1 = FirstUpdate();
        Task task2 = SecondUpdate();

        await Task.WhenAll(task1, task2);

        Console.WriteLine("Completed.\n");

        async Task FirstUpdate()
        {
            ItemResponse<Student> response =
                await container.ReadItemAsync<Student>("8a4e944e-061c-4daf-9daf-d062f1234dd5", new PartitionKey("USA"));

            Student student = response.Resource;

            student.FirstName = "Georgi";

            Thread.Sleep(3000);

            ItemResponse<Student> updateResponse =
                await container.ReplaceItemAsync<Student>(student, student.Id, new PartitionKey(student.PartitionKey));
        }
        async Task SecondUpdate()
        {
            ItemResponse<Student> response =
                await container.ReadItemAsync<Student>("8a4e944e-061c-4daf-9daf-d062f1234dd5", new PartitionKey("USA"));

            Student student = response.Resource;

            student.LastName = "Inkov";

            ItemResponse<Student> updateResponse =
                await container.ReplaceItemAsync<Student>(student, student.Id, new PartitionKey(student.PartitionKey));
        }
    }

    public static async Task ExecuteStoredProcedureAsync(CosmosClient cosmosClient, string continuationToken = null)
    {
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);

        StoredProcedureExecuteResponse<string> response =
            await container.Scripts.ExecuteStoredProcedureAsync<string>("DemoSP", new PartitionKey("USA"), null);

        IEnumerable<Student> students = JsonConvert.DeserializeObject<IEnumerable<Student>>(response.Resource);

        foreach (var student in students)
        {
            Console.WriteLine(student);
            Console.WriteLine();
        }

        Console.WriteLine($"Queried items: {students.Count()}");
        Console.WriteLine($"Request units: {response.RequestCharge}\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }
}