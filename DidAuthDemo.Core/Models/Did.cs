namespace DidAuthDemo.Core.Models;

public class Did
{
    public int KeyId { get; set; }
    public int DidType { get; set; }
    public int IndexDerivation { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }
    public string ResolutionType { get; set; }
    public string Domain { get; set; }
    public string GithubUsername { get; set; }
    public string DidDocument { get; set; }
}
