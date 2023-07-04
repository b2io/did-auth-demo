namespace DidAuthDemo.Core;

public class DidService
{
    public List<DidDocument> Documents { get; set; }

    public DidService()
    {
        Documents = new List<DidDocument>();
    }

    public List<DidDocument> List() => Documents;

    public void AddDocument(DidDocument document) => Documents.Add(document);

    public DidDocument? GetByDid(string did) => Documents.FirstOrDefault(x => x.Id == did);

    public List<DidDocument> GetByController(string did) => Documents.Where(x => x.Controller == did).ToList();
}
