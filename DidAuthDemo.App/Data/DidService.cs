using DidAuthDemo.App.Models;

namespace DidAuthDemo.App.Data;

public class DidService
{
    public List<DidDocument> HolderDocuments { get; set; }
    public List<DidDocument> VerifierDocuments { get; set; }

    public DidService()
    {
        HolderDocuments = new List<DidDocument>();
        VerifierDocuments = new List<DidDocument>();
    }

    public List<DidDocument> GetHolderList() => HolderDocuments;

    public void AddHolderDocument(DidDocument document) => HolderDocuments.Add(document);

    public DidDocument? GetHolderByDid(string did) => HolderDocuments.FirstOrDefault(x => x.Id == did);

    public List<DidDocument> GetVerifierList() => VerifierDocuments;

    public void AddVerifierDocument(DidDocument document) => VerifierDocuments.Add(document);

    public DidDocument? GetVerifierByDid(string did) => VerifierDocuments.FirstOrDefault(x => x.Id == did);
}
