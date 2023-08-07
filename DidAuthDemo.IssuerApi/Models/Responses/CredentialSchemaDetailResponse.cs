using DidAuthDemo.IssuerApi.Models.CredentialSchemas;
using System.Text.Json;

namespace DidAuthDemo.IssuerApi.Models.Responses
{
    public class CredentialSchemaDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string OwnerDid { get; set; }
        public DetailSchema? SchemaDefinition { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public CredentialSchemaDetailResponse(
            int id, 
            string name, 
            string description, 
            string ownerId, 
            string schemaDefinition, 
            DateTimeOffset createdAt, 
            DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;    
            Description = description;
            OwnerDid = ownerId;
            SchemaDefinition = JsonSerializer.Deserialize<DetailSchema>(schemaDefinition);
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
