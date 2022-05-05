using CosmosDemo.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using static CosmosDemo.Data.Constants;

namespace CosmosDemo.CRUD;

public class Delete
{
    public static async Task DeleteItemAsync(CosmosClient cosmosClient)
    {
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
        string studentId = "1669e6aa-b695-4346-9985-8b6c81a33b26";

        ItemResponse<Student> response = await container.DeleteItemAsync<Student>(studentId, new PartitionKey("Denmark"));
        Console.WriteLine($"Deleted Student with id: {studentId}\n");
    }

    public static async Task DeleteDatabaseAsync(CosmosClient cosmosClient)
    {
        Database database = cosmosClient.GetDatabase(DatabaseId);

        DatabaseResponse response = await database.DeleteAsync();

        Console.WriteLine($"Deleted Database: {response.Database.Id}\n");
    }
}