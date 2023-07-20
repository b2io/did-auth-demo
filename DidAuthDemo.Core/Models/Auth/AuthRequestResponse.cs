using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Common;
using SimpleBase;
using System.Text;
using System.Text.Json.Serialization;

namespace DidAuthDemo.Core.Models
{
    public class AuthRequest
    {
        [JsonPropertyName("controller")]
        public string Controller { get; set; }

        [JsonPropertyName("query")]
        public AuthRequestQuery[] Queries { get; set; }

        [JsonPropertyName("challenge")]
        public string Challenge { get; set; } = Guid.NewGuid().ToString();
    }

    public class AuthRequestWithSignature : AuthRequest
    {
        [JsonConstructor] public AuthRequestWithSignature() { }

        public AuthRequestWithSignature(AuthRequest request, PrivateKey privateKey, string verificationMethodId)
        {
            Controller = request.Controller;
            Challenge = request.Challenge;
            Queries = request.Queries;

            var message = ByteArrayUtility.ObjectToByteArray(request);
            var sig = privateKey.Sign(message);

            Proof = new AuthRequestProof()
            {
                VerificationMethod = verificationMethodId,
                ProofValue = Base58.Bitcoin.Encode(sig)
            };
        }

        [JsonPropertyName("proof")]
        public AuthRequestProof Proof { get; set; }


    }

    public class AuthResponse
    {
        [JsonConstructor] public AuthResponse() { }

        public AuthResponse(PrivateKey privateKey, string verificationMethodId, string controller, string challenge)
        {
            Controller = controller;

            var message = Encoding.UTF8.GetBytes(challenge);
            var sig = privateKey.Sign(message);

            Proof = new AuthResponseProof()
            {
                VerificationMethod = verificationMethodId,
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

    public class AuthRequestProof
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "Ed25519Signature2020";

        [JsonPropertyName("verificationMethod")]
        public string VerificationMethod { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; } = DateTime.UtcNow.ToString("o");

        [JsonPropertyName("proofPurpose")]
        public string ProofPurpose { get; set; } = "assertionMethod";

        [JsonPropertyName("proofValue")]
        public string ProofValue { get; set; }
    }

    public class AuthRequestQuery
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "DIDAuthentication";
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
}
