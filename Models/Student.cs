using Newtonsoft.Json;

namespace CosmosDemo.Models;

public class Student
{
    public string PartitionKey { get; set; }

    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "_etag")]
    public string Etag { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Address Address { get; set; }

    public Course[] Courses { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}