using System.Text.Json.Serialization;

namespace DidAuthDemo.IssuerApi.Entities.Requests;

public class AuthQueryRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "DIDAuthentication";
}
