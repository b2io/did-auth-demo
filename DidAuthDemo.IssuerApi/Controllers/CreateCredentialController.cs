using DidAuthDemo.IssuerApi.Data;
using DidAuthDemo.IssuerApi.Data.Entities;
using DidAuthDemo.IssuerApi.Models.CredentialSchemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DidAuthDemo.IssuerApi.Controllers;

[Route("api/create-credentials")]
[ApiController]
public class CreateCredentialController : ControllerBase
{
    private readonly IssuerDbContext _issuerDbContext;

    public CreateCredentialController(IssuerDbContext issuerDbContext)
    {
        _issuerDbContext = issuerDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CredentialSchemaDto credentialSchemaDto)
    {
        var credentialSchema = new CredentialSchema
        {
            OwnerDid = credentialSchemaDto.OwnerDid,
            Name = credentialSchemaDto.Name,
            Description = credentialSchemaDto.Description,
            SchemaDefinition = JsonSerializer.Serialize(credentialSchemaDto.SchemaDefinition)
        };

        _issuerDbContext.CredentialSchemas.Add(credentialSchema);
        await _issuerDbContext.SaveChangesAsync();

        return Ok(credentialSchema);
    }
}

public record CredentialSchemaDto(string OwnerDid, string Name, string Description, DetailSchema SchemaDefinition);