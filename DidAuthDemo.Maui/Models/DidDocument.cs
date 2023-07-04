using CardanoSharp.Wallet.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DidAuthDemo.Maui.Models;

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
