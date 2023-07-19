using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using SimpleBase;
using System.Text;
using System.Text.Json.Serialization;

namespace DidAuthDemo.IssuerApi.Entities.Auth;

public class AuthResponse
{
    [JsonConstructor] public AuthResponse() { }

    public AuthResponse(PrivateKey privateKey, string verificationMethodId, string controller, string challenge)
    {
        Controller = controller;

        var message = Encoding.ASCII.GetBytes(challenge);
        var sig = privateKey.Sign(message); // authDidDoc.KeyPair.PrivateKey.Sign(message);

        Proof = new AuthResponseProof()
        {
            VerificationMethod = verificationMethodId, // authDidDoc.PublicKeys[0].Id,
            Challenge = challenge,
            ProofValue = Base58.Bitcoin.Encode(sig)
        };
    }

    [JsonPropertyName("@context")]
    public string[]? Context { get; set; } = new[] { "https://www.w3.org/2022/credentials/v2" };

    [JsonPropertyName("type")]
    public string Type { get; set; } = "VerifiablePresentation";

    [JsonPropertyName("controller")]
    public string Controller { get; set; }

    [JsonPropertyName("proof")]
    public AuthResponseProof Proof { get; set; }
}

public class AuthResponseProof
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "DataIntegrityProof";

    [JsonPropertyName("cryptosuite")]
    public string CryptoSuite { get; set; } = "eddsa-2022";

    [JsonPropertyName("verificationMethod")]
    public string VerificationMethod { get; set; }

    [JsonPropertyName("challenge")]
    public string Challenge { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; } = DateTime.UtcNow.ToString("o");

    [JsonPropertyName("proofPurpose")]
    public string ProofPurpose { get; set; } = "authentication";

    [JsonPropertyName("proofValue")]
    public string ProofValue { get; set; }
}
