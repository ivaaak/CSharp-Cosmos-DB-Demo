using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System;
using System.Threading.Tasks;

using static CosmosDemo.CRUD.Create;
using static CosmosDemo.CRUD.Read;
using static CosmosDemo.CRUD.Update;
using static CosmosDemo.CRUD.Delete;

using static CosmosDemo.Data.Constants;

namespace CosmosDemo;

public class CosmosService
{
    public async Task ExecuteCommand(string command)
    {
        using (var cosmosClient = new CosmosClientBuilder(EndpointUri, PrimaryKey).WithApplicationRegion(Regions.WestEurope).Build())
        {
            switch (command)
            {
                case "1":
                    await CreateDatabaseAsync(cosmosClient);
                    break;
                case "2":
                    await CreateContainerAsync(cosmosClient);
                    break;
                case "3":
                    await ScaleContainerAsync(cosmosClient);
                    break;
                case "4":
                    await AddItemsAsync(cosmosClient);
                    break;
                case "5":
                    await QueryItemsAsync(cosmosClient);
                    break;
                case "6":
                    await UpdateItemAsync(cosmosClient);
                    break;
                case "7":
                    await DeleteItemAsync(cosmosClient);
                    break;
                case "8":
                    await ConcurrencyUpdateAsync(cosmosClient);
                    break;
                case "9":
                    await ExecuteStoredProcedureAsync(cosmosClient);
                    break;
                case "10":
                    await DeleteDatabaseAsync(cosmosClient);
                    break;
                default:
                    Console.WriteLine($"Invalid command: {command}");
                    break;
            }
        }
    }
}