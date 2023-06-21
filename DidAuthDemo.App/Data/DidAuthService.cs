using DidAuthDemo.App.Models;

namespace DidAuthDemo.App.Data;

public class DidAuthService
{
    public List<DidDocument> HolderDocuments { get; set; }
    public List<DidDocument> VerifierDocuments { get; set; }

    public DidAuthService()
    {
        HolderDocuments = new List<DidDocument>();
        VerifierDocuments = new List<DidDocument>();
    }

    public List<DidDocument> GetHolderList() => HolderDocuments;

    public void AddHolderDocument(DidDocument document) => HolderDocuments.Add(document);

    public DidDocument? GetHolderByDidId(string did) => HolderDocuments.FirstOrDefault(x => x.Id == did);

    public List<DidDocument> GetHolderByDidController(string did) => HolderDocuments.Where(x => x.Controller == did).ToList();

    public List<DidDocument> GetVerifierList() => VerifierDocuments;

    public void AddVerifierDocument(DidDocument document) => VerifierDocuments.Add(document);

    public DidDocument? GetVerifierByDidId(string did) => VerifierDocuments.FirstOrDefault(x => x.Id == did);

    public List<DidDocument> GetVerifierByDidController(string did) => VerifierDocuments.Where(x => x.Controller == did).ToList();
}
