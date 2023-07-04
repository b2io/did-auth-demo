using SQLite;

namespace DidAuthDemo.Maui.Models;

public class Key
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
}
