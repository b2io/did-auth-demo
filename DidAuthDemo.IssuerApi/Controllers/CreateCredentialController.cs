using DidAuthDemo.IssuerApi.Data;
using DidAuthDemo.IssuerApi.Data.Entities;
using DidAuthDemo.IssuerApi.Models.CredentialSchemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        // Creates a TextInfo based on the "en-US" culture.
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        var credentialSchema = new CredentialSchema
        {
            OwnerDid = credentialSchemaDto.OwnerDid,
            Name = credentialSchemaDto.Name,
            Description = credentialSchemaDto.Description,
            Type = textInfo.ToTitleCase(credentialSchemaDto.Name).Replace(" ", ""),
            SchemaDefinition = JsonSerializer.Serialize(credentialSchemaDto.SchemaDefinition)
        };

        _issuerDbContext.CredentialSchemas.Add(credentialSchema);
        await _issuerDbContext.SaveChangesAsync();

        return Ok(credentialSchema);
    }
}

public record CredentialSchemaDto(string OwnerDid, string Name, string Description, ClaimSchema[] SchemaDefinition);