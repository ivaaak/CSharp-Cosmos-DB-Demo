using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using static CosmosDemo.Data.Constants;

namespace CosmosDemo.CRUD;

public class Create
{
    public static async Task CreateDatabaseAsync(CosmosClient cosmosClient)
    {
        DatabaseResponse response = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);

        Console.WriteLine($"Created Database: {response.Database.Id}\n");
    }

    
    public static async Task CreateContainerAsync(CosmosClient cosmosClient)
    {
        Database database = cosmosClient.GetDatabase(DatabaseId);

        ContainerResponse response = await database.CreateContainerIfNotExistsAsync(ContainerId, "/PartitionKey");

        Console.WriteLine($"Created Container: {response.Container.Id}\n");
    }

    
    public static async Task ScaleContainerAsync(CosmosClient cosmosClient)
    {
        Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);

        int? throughput = await container.ReadThroughputAsync();
        if (throughput.HasValue)
        {
            Console.WriteLine($"Current provisioned throughput : {throughput.Value}\n");

            int newThroughput = 10000;

            await container.ReplaceThroughputAsync(newThroughput);

            Console.WriteLine($"New provisioned throughput : {newThroughput}\n");
        }
    }
}