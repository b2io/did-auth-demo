using DidAuthDemo.IssuerApi.Entities.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DidAuthDemo.IssuerApi.Controllers;

[Route("api/check-auth-status")]
public class CheckAuthStatusController : Controller
{
    [HttpGet("{challenge}")]
    public IActionResult Index(string challenge)
    {
        return Ok();
    }
}
