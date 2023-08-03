using DidAuthDemo.IssuerApi.Data;
using DidAuthDemo.IssuerApi.Data.Entities;
using DidAuthDemo.IssuerApi.Models.CredentialSchemas;
using DidAuthDemo.IssuerApi.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DidAuthDemo.IssuerApi.Controllers;

[Route("api/get-credential-schemas")]
[ApiController]
public class GetCredentialSchemasController : ControllerBase
{
    private readonly IssuerDbContext _issuerDbContext;

    public GetCredentialSchemasController(IssuerDbContext issuerDbContext)
    {
        _issuerDbContext = issuerDbContext;
    }

    [HttpGet("{ownerDid}/list")]
    public async Task<IActionResult> GetAsync(string ownerDid) =>
        Ok(await _issuerDbContext.CredentialSchemas
            .Select(x => new CredentialSchema()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                OwnerDid = x.OwnerDid
            })
            .Where(x => x.OwnerDid == ownerDid)
            .ToListAsync());


    [HttpGet("{ownerDid}/details/{id}")]
    public async Task<IActionResult> DetailsAsync(string ownerDid, int id)
    {
        var result = await _issuerDbContext.CredentialSchemas
            .SingleOrDefaultAsync(x => x.OwnerDid == ownerDid
                && x.Id == id);

        var detailSchema = new CredentialSchemaDetailResponse(
            result.Id,
            result.Name,
            result.Description,
            result.OwnerDid,
            result.SchemaDefinition,
            result.CreatedAt,
            result.UpdatedAt);

        return Ok(detailSchema);
    }

}
