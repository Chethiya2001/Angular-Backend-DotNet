using Core.Entites;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ErrorController : BaseApiController
{
    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request");
    }

    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }

    [HttpGet("bad-gateway")]
    public ActionResult GetBadGateway()
    {
        throw new Exception("This is a bad gateway");
    }

    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("forbidden")]
    public ActionResult GetForbidden()
    {
        return Forbid();
    }

    [HttpGet("not-found/{id}")]
    public ActionResult GetNotFound(int id)
    {
        return NotFound();
    }
    [HttpGet("validation-error")]
    public ActionResult GetValidationError(Product product)
    {
        return Ok();
    }
    [HttpGet("internal-error")]
    public ActionResult GetInternalError()
    {
        return StatusCode(500);
    }
}
