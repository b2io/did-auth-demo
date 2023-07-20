using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Resolvers;
using DidAuthDemo.IssuerApi.Data;
using DidAuthDemo.IssuerApi.Entities.Auth;
using Microsoft.AspNetCore.Mvc;
using SimpleBase;
using System.Text;

namespace DidAuthDemo.IssuerApi.Controllers;

[Route("api/capture-auth-response")]
public class CaptureAuthResponseController : Controller
{
    private readonly ICaptureAuthResponseService _captureAuthResponseService;

    public CaptureAuthResponseController(ICaptureAuthResponseService captureAuthResponseService)
    {
        _captureAuthResponseService = captureAuthResponseService;
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromBody] AuthResponse authResponse)
    {
        var isResolvableAndValid = await _captureAuthResponseService.IsDidResolvableAndValid(authResponse);
        if (!isResolvableAndValid) return Ok(new { successful = false });

        await _captureAuthResponseService.SaveAuth(authResponse);

        return Ok(new { successful = true });
    }
}

public interface ICaptureAuthResponseService
{
    Task<bool> IsDidResolvableAndValid(AuthResponse authResponse);
    Task SaveAuth(AuthResponse authResponse);
}

public class CaptureAuthResponseService : ICaptureAuthResponseService
{
    private readonly IssuerDbContext _issuerDbContext;

    public CaptureAuthResponseService(IssuerDbContext issuerDbContext)
    {
        _issuerDbContext = issuerDbContext;
    }

    public async Task<bool> IsDidResolvableAndValid(AuthResponse authResponse)
    {
        var resolver = ResolverFactory.GetResolver(authResponse.Controller);
        var didDocument = await resolver.GetDidDocument(authResponse.Controller);

        var publicKeyObj = didDocument.PublicKeys.FirstOrDefault(x => x.Id == authResponse.Proof.VerificationMethod);
        var publicKey = new PublicKey(Base58.Bitcoin.Decode(publicKeyObj.PublicKeyBase58), null);

        var message = Encoding.UTF8.GetBytes(authResponse.Proof.Challenge);
        return publicKey.Verify(message, Base58.Bitcoin.Decode(authResponse.Proof.ProofValue));
    }

    public async Task SaveAuth(AuthResponse authResponse)
    {
        _issuerDbContext.Auths.Add(new Data.Entities.Auth()
        {
            DidOwner = authResponse.Controller,
            Challenge = authResponse.Proof.Challenge,
        });

        await _issuerDbContext.SaveChangesAsync();
    }
}
