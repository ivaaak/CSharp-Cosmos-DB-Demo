using CosmosDemo;
using Microsoft.Azure.Cosmos;
using System;

CosmosService cosmosService = new();

while (true)
{
    Console.WriteLine("1 - Create Database");
    Console.WriteLine("2 - Create Container");
    Console.WriteLine("3 - Scale Container");
    Console.WriteLine("4 - Add Items");
    Console.WriteLine("5 - Query Items");
    Console.WriteLine("6 - Update Item");
    Console.WriteLine("7 - Delete Item");
    Console.WriteLine("8 - Concurrency Update");
    Console.WriteLine("9 - Execute Stored Procedure");
    Console.WriteLine("10 - Delete Database");
    Console.WriteLine();
    Console.Write("Press command number or 'e' for exit: ");

    string input = Console.ReadLine();
    Console.Clear();

    if (input is "e") break;

    try
    {
        await cosmosService.ExecuteCommand(input);
    }
    catch (CosmosException de)
    {
        Exception baseException = de.GetBaseException();
        Console.WriteLine("{0} error occurred: {1}\n", de.StatusCode, de);
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
        Console.Clear();
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: {0}\n", e);
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
        Console.Clear();
    }
}

Console.WriteLine("End of demo.");