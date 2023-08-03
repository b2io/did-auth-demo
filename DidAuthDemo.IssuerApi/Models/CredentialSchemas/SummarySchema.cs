namespace DidAuthDemo.IssuerApi.Models.CredentialSchemas;

public class SummarySchema
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string OwnerDid { get; set; } = null!;
}
