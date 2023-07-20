using SQLite;

namespace DidAuthDemo.Maui.Models;

public class Did: Core.Models.Did
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int KeyId { get; set; }
}
