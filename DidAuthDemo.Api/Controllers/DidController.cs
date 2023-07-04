using DidAuthDemo.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DidAuthDemo.Api.Controllers
{
    [Route("api/did")]
    [ApiController]
    public class DidController : ControllerBase
    {
        private readonly DidService _service;

        public DidController(DidService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IActionResult List()
        {
            return Ok(_service.List());
        }

        [HttpGet("{did}")]
        public IActionResult Get(string did)
        {
            return Ok(_service.GetByDid(did));
        }

        [HttpGet("controller/{did}")]
        public IActionResult GetByController(string did)
        {
            return Ok(_service.GetByController(did));
        }

        [HttpPost()]
        public IActionResult Add([FromBody] DidDocument didDocument)
        {
            _service.AddDocument(didDocument);
            return Ok(didDocument);
        }
    }
}
