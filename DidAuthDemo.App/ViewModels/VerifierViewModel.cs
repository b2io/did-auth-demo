using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.Utilities;
using DidAuthDemo.App.Common;
using DidAuthDemo.App.Data;
using DidAuthDemo.App.Models;
using Radzen;
using SimpleBase;
using System.Text;

namespace DidAuthDemo.App.ViewModels;

public class VerifierViewModel
{
    public DidDocument? VerifierDidDocument { get; set; }
    public List<DidDocument> AuthDidDocuments { get; set; }
    public List<DidDocument> ProfileDidDocuments { get; set; }
    public List<AuthRequestWithSignature> AuthRequests { get; set; }

    public IEnumerable<string> AuthDids => AuthDidDocuments.Select(x => x.Id);
    public string SelectedAuthDidForRequest { get; set; }
    public string ResponseToVerify { get; set; }

    private readonly DidService _service;
    private readonly DidAuthService _didAuthService;
    private readonly DidProfileService _didProfileService;
    private readonly AuthRequestService _authRequestService;
    private readonly ClipboardService _clipboardService;

    public VerifierViewModel(DidService service, DidAuthService didAuthService, DidProfileService didProfileService, ClipboardService clipboardService, AuthRequestService authRequestService)
    {
        _service = service;
        _didAuthService = didAuthService;
        _didProfileService = didProfileService;
        _clipboardService = clipboardService;
        _authRequestService = authRequestService;
    }

    public void GetVerifierDidDocument(string verifierDid)
    {
        VerifierDidDocument = _service.GetVerifierByDid(verifierDid);
    }

    public void GetDidAuthDocuments(string verifierDid)
    {
        AuthDidDocuments = _didAuthService.GetVerifierByDidController(verifierDid);
    }

    public void GetDidProfileDocuments(string verifierDid)
    {
        ProfileDidDocuments = _didProfileService.GetVerifierByDidController(verifierDid);
    }

    public void GetAuthRequests(string verifierDid)
    {
        AuthRequests = _authRequestService.GetByController(verifierDid);
    }

    public void GenerateNewAuthRequest()
    {
        var request = new AuthRequest()
        {
            Controller = VerifierDidDocument.Id,
            Queries = (new List<AuthRequestQuery>() { new AuthRequestQuery() }).ToArray()
        };

        var authDidDocument = _didAuthService.GetVerifierByDidId(SelectedAuthDidForRequest);

        var requestWithSignature = new AuthRequestWithSignature(request, authDidDocument);

        var valid = authDidDocument.KeyPair.PublicKey.Verify(Utility.ObjectToByteArray(request), Base58.Bitcoin.Decode(requestWithSignature.Proof.ProofValue));

        _authRequestService.AddDocument(requestWithSignature);
        GetAuthRequests(VerifierDidDocument.Id);
    }

    public void GenerateNewAuthDidDocument()
    {
        var didKeyPair = KeyPair.GenerateKeyPair();
        var did = $"did:b2i:{Base58.Bitcoin.Encode(HashUtility.Blake2b224(didKeyPair.PublicKey.Key))}";
        var keyId = $"{did}#key-1";
        var didDocumentPublicKeys = new List<DidPublicKey>()
        {
            new DidPublicKey()
            {
                Id = keyId,
                Type = "Ed25519VerificationKey2018",
                Owner = did,
                PublicKeyBase58 = Base58.Bitcoin.Encode(didKeyPair.PublicKey.Key)
            }
        };

        var didDocumentAuthentications = new List<DidAuthentication>()
        {
            new DidAuthentication()
            {
                Type = "Ed25519SignatureAuthentication2018",
                PublicKey = keyId
            }
        };

        var registrationDidDocument = new DidDocument()
        {
            Context = new[] { "https://www.w3.org/ns/did/v1" },
            Id = did,
            Controller = VerifierDidDocument.Id,
            Authentications = didDocumentAuthentications.ToArray(),
            PublicKeys = didDocumentPublicKeys.ToArray(),
            KeyPair = didKeyPair
        };

        _didAuthService.AddVerifierDocument(registrationDidDocument);
        GetDidAuthDocuments(VerifierDidDocument.Id);
    }

    public async Task CopyRequest(AuthRequestWithSignature request)
    {
        await _clipboardService.WriteTextAsync(Base58.Bitcoin.Encode(Utility.ObjectToByteArray(request)));
    }

    public (DidDocument?, bool) GetHoldersAuthDidDocument(AuthRequest request)
    {
        var responseDecoded = Base58.Bitcoin.Decode(ResponseToVerify);
        var response = Utility.ByteArrayToObject<AuthResponse>(responseDecoded);

        var key = response.Proof.VerificationMethod.Split("#");
        //not true resolver since we are using dummy did method
        var holderAuthDidDocument = _didAuthService.GetHolderByDidId(key[0]);

        if (holderAuthDidDocument == null)
            return (null, false);

        //using verification method to select correct public key
        var publicKeyObj = holderAuthDidDocument.PublicKeys.FirstOrDefault(x => x.Id == response.Proof.VerificationMethod);
        var publicKey = new PublicKey(Base58.Bitcoin.Decode(publicKeyObj.PublicKeyBase58), null);

        var message = Encoding.UTF8.GetBytes(request.Challenge);
        var isValid = publicKey.Verify(message, Base58.Bitcoin.Decode(response.Proof.ProofValue));

        ResponseToVerify = string.Empty;

        return (holderAuthDidDocument, isValid);
    }
}
