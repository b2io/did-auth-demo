namespace DidAuthDemo.Core;

public class AuthResponseService
{
    public List<AuthResponse> Responses { get; set; }

    public AuthResponseService()
    {
        Responses = new List<AuthResponse>();
    }

    public List<AuthResponse> GetList() => Responses;

    public void AddDocument(AuthResponse document) => Responses.Add(document);

    public List<AuthResponse> GetByController(string did) => Responses.Where(x => x.Controller == did).ToList();
}
