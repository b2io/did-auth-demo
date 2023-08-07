namespace DidAuthDemo.IssuerApi.Models.CredentialSchemas;

public class DetailSchema
{
    public SummarySchema? Summary { get; set; } = null!;
    public List<ClaimSchema> Claims { get; set; }
}
