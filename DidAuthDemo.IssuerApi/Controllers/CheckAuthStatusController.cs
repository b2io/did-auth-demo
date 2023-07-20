using DidAuthDemo.IssuerApi.Data;
using DidAuthDemo.IssuerApi.Entities.Requests;
using DidAuthDemo.IssuerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DidAuthDemo.IssuerApi.Controllers;

[Route("api/check-auth-status")]
public class CheckAuthStatusController : Controller
{
    private readonly ICheckAuthStatusService _checkAuthStatusService;

    public CheckAuthStatusController(ICheckAuthStatusService checkAuthStatusService)
    {
        _checkAuthStatusService = checkAuthStatusService;
    }

    [HttpGet("{challenge}")]
    public async Task<IActionResult> Index(string challenge)
    {
        var authRecord = await _checkAuthStatusService.HasChallengeBeenAuthenticated(challenge);
        if (!authRecord.IsAuthenticated) return Ok(new CheckAuthResponse(authRecord.IsAuthenticated, null));

        //create JWT
        string jwt = _checkAuthStatusService.GenerateToken(authRecord.Did);

        return Ok(new CheckAuthResponse(authRecord.IsAuthenticated, jwt));
    }
}

public record AuthenticatedRecord(bool IsAuthenticated, string? Did);

public record CheckAuthResponse(bool IsAuthenticated, string? AccessToken);

public interface ICheckAuthStatusService
{
    Task<AuthenticatedRecord> HasChallengeBeenAuthenticated(string challenge);
    string GenerateToken(string did);
}

public class CheckAuthStatusService : ICheckAuthStatusService
{
    private readonly IssuerDbContext _issuerDbContext;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public CheckAuthStatusService(IssuerDbContext issuerDbContext, ITokenGeneratorService tokenGeneratorService)
    {
        _issuerDbContext = issuerDbContext;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public string GenerateToken(string did) =>
        _tokenGeneratorService.GenerateToken(did);
    

    public async Task<AuthenticatedRecord> HasChallengeBeenAuthenticated(string challenge)
    {
        var authRecord = await _issuerDbContext.Auths
            .Where(x => x.Challenge == challenge)
            .FirstOrDefaultAsync();

        if (authRecord is null) return new AuthenticatedRecord(false, null);

        _issuerDbContext.Auths.Remove(authRecord);
        await _issuerDbContext.SaveChangesAsync();

        return new AuthenticatedRecord((DateTime.UtcNow - authRecord.CreatedAt).TotalMinutes > 15, authRecord.DidOwner);
    }
}