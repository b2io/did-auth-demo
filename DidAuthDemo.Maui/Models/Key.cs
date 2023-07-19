using SQLite;

namespace DidAuthDemo.Maui.Models;

public class Key: Core.Models.Key
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
