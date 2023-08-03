using Microsoft.EntityFrameworkCore;

namespace DidAuthDemo.IssuerApi.Data.Entities
{
    [Index(nameof(OwnerDid), Name = "Index_OwnerDid")]
    public class CredentialSchema : BaseEntity
    {
        public string OwnerDid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SchemaDefinition { get; set; }
    }
}
