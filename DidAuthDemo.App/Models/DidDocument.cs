using CardanoSharp.Wallet.Models.Keys;
using System.Text.Json.Serialization;

namespace DidAuthDemo.App.Models;

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

    [JsonPropertyName("profile")]
    public DidProfile? Profile { get; set; }

    [JsonIgnore]
    public KeyPair KeyPair { get; set; }
}

public class DidAuthentication
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("publicKey")]
    public string? PublicKey { get; set; }
}

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

public class DidProfile
{
    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}