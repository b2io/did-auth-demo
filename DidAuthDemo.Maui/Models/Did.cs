using SQLite;

namespace DidAuthDemo.Maui.Models;

public class Did
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int KeyId { get; set; }
    public int DidType { get; set; }
    public int IndexDerivation { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }
    public string Domain { get; set; }
    public string GithubUsername { get; set; }
}
