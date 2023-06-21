using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.Utilities;
using DidAuthDemo.App.Common;
using DidAuthDemo.App.Data;
using DidAuthDemo.App.Models;
using SimpleBase;

namespace DidAuthDemo.App.ViewModels;

public class HolderViewModel
{
    public DidDocument? HolderDidDocument { get; set; }
    public List<DidDocument> AuthDidDocuments { get; set; }
    public List<DidDocument> ProfileDidDocuments { get; set; }
    public List<AuthResponse> AuthResponses { get; set; }

    public IEnumerable<string> AuthDids => AuthDidDocuments.Select(x => x.Id);
    public string SelectedAuthDidForResponse { get; set; }
    public string RequestToSign { get; set; }

    private readonly DidService _service;
    private readonly DidAuthService _didAuthService;
    private readonly DidProfileService _didProfileService;
    private readonly AuthResponseService _authResponseService;
    private readonly ClipboardService _clipboardService;

    public HolderViewModel(DidAuthService didAuthService, DidProfileService didProfileService, AuthResponseService authResponseService, DidService service, ClipboardService clipboardService)
    {
        _didAuthService = didAuthService;
        _didProfileService = didProfileService;
        _authResponseService = authResponseService;
        _service = service;
        _clipboardService = clipboardService;
    }

    public void GetHolderDidDocument(string holderDid)
    {
        HolderDidDocument = _service.GetHolderByDid(holderDid);
    }

    public void GetDidAuthDocuments(string holderDid)
    {
        AuthDidDocuments = _didAuthService.GetHolderByDidController(holderDid);
    }

    public void GetDidProfileDocuments(string holderDid)
    {
        ProfileDidDocuments = _didProfileService.GetHolderByDidController(holderDid);
    }

    public void GetAuthResponses(string holderDid)
    {
        AuthResponses = _authResponseService.GetByController(holderDid);
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
            Controller = HolderDidDocument.Id,
            Authentications = didDocumentAuthentications.ToArray(),
            PublicKeys = didDocumentPublicKeys.ToArray(),
            KeyPair = didKeyPair
        };

        _didAuthService.AddHolderDocument(registrationDidDocument);
        GetDidAuthDocuments(HolderDidDocument.Id);
    }

    public void SignAuthRequest()
    {
        var requestDecoded = Base58.Bitcoin.Decode(RequestToSign);
        var request = Utility.ByteArrayToObject<AuthRequestWithSignature>(requestDecoded);

        var authDidDocument = _didAuthService.GetHolderByDidId(SelectedAuthDidForResponse);

        var response = new AuthResponse(HolderDidDocument.Id, request.Challenge, authDidDocument);

        _authResponseService.AddDocument(response);
        RequestToSign = string.Empty;
        GetAuthResponses(HolderDidDocument.Id);
    }

    public async Task CopyResponse(AuthResponse response)
    {
        await _clipboardService.WriteTextAsync(Base58.Bitcoin.Encode(Utility.ObjectToByteArray(response)));
    }
}
