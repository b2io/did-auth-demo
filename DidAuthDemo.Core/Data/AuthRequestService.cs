namespace DidAuthDemo.Core;

public class AuthRequestService
{
    public List<AuthRequestWithSignature> Requests { get; set; }

    public AuthRequestService()
    {
        Requests = new List<AuthRequestWithSignature>();
    }

    public List<AuthRequestWithSignature> GetList() => Requests;

    public void AddDocument(AuthRequestWithSignature document) => Requests.Add(document);

    public List<AuthRequestWithSignature> GetByController(string did) => Requests.Where(x => x.Controller == did).ToList();
}
