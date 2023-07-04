using System.Text.Json.Serialization;

namespace DidAuthDemo.Maui.Models;

public class DidPublicKey
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("publicKeyBase58")]
    public string? PublicKeyBase58 { get; set; }
}
