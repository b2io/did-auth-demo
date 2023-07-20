using System.ComponentModel.DataAnnotations;

namespace DidAuthDemo.IssuerApi.Data.Entities
{
    public class Auth : BaseEntity
    {
        [Required]
        public string? Challenge { get; set; }

        [Required]
        public string? DidOwner { get; set; }
    }
}
