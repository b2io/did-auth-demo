using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace DidAuthDemo.IssuerApi.Data.Entities;

public class CredentialRequest : BaseEntity
{
    public int CredentialSchemaId { get; set; }
    [ForeignKey(nameof(CredentialSchemaId))]
    public CredentialSchema CredentialSchema { get; set; }
    public string RequesterDid { get; set;}
    public string RequestBody { get; set; }
    public int StatusId { get; set; }
}
