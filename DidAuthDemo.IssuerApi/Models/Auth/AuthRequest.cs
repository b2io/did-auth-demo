using System.Text.Json.Serialization;

namespace DidAuthDemo.IssuerApi.Entities.Requests;

public class AuthRequest
{
    [JsonPropertyName("query")]
    public AuthQueryRequest[] Queries { get; set; }

    [JsonPropertyName("challenge")]
    public string Challenge { get; set; } = Guid.NewGuid().ToString();
}
