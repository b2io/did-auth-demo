using System.ComponentModel.DataAnnotations;

namespace DidAuthDemo.IssuerApi.Data.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTimeOffset UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPersisted => !IsNewRecord;

    public bool IsNewRecord => Id.Equals(0);
}
