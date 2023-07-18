using DidAuthDemo.IssuerApi.Entities.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DidAuthDemo.IssuerApi.Controllers;

[Route("api/get-auth-request")]
public class GetAuthRequestController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new AuthRequest()
        {
            Queries = (new List<AuthQueryRequest>() { new AuthQueryRequest() }).ToArray()
        });
    }
}
