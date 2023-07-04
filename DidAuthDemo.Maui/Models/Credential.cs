using SQLite;

namespace DidAuthDemo.Maui.Models;

public class Credential
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Issuer { get; set; }
    public string Payload { get; set; }
}
