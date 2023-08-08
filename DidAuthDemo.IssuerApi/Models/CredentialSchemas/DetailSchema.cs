namespace DidAuthDemo.IssuerApi.Models.CredentialSchemas;

public class DetailSchema
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public SummarySchema? Summary { get; set; } = null!;
    public List<ClaimSchema> Claims { get; set; }
}
