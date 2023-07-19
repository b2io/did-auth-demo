using System.Text.Json.Serialization;

namespace DidAuthDemo.Core.Models;

public class DidDocument
{
    [JsonPropertyName("@context")]
    public string[]? Context { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("controller")]
    public string? Controller { get; set; }

    [JsonPropertyName("authentication")]
    public DidAuthentication[]? Authentications { get; set; }

    [JsonPropertyName("publicKey")]
    public DidPublicKey[]? PublicKeys { get; set; }
}
