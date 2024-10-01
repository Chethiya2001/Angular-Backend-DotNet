using API.ReqHelpers;
using Core.Entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/product")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePageResultAsync<T>(IGenericRepository<T> repo,
    ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);
        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return Ok(pagination);
    }

}
