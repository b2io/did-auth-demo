using System.Text.Json.Serialization;

namespace DidAuthDemo.Maui.Models;

public class DidAuthentication
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("publicKey")]
    public string? PublicKey { get; set; }
}
