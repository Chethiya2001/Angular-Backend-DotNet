
using API.ReqHelpers;
using Core.Entites;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;


public class ProductController(IGenericRepository<Product> repo) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetAllAsync([FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductFilterSpecification(specParams);
        return Ok(await CreatePageResultAsync(repo, spec, specParams.PageIndex, specParams.PageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetByIdAsync(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateAsync(Product product)
    {
        repo.Add(product);
        if (await repo.SaveAllChangesAsync())
        {
            return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
        }
        return BadRequest("Can't create product");
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateAsync(int id, Product product)
    {
        if (id != product.Id || !ProductExists(id))
        {
            return BadRequest(" Can't find product");
        }
        repo.Update(product);
        if (await repo.SaveAllChangesAsync())
        {
            return NoContent();
        }
        return BadRequest("Can't update product");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id, Product product)
    {
        if (id != product.Id || !ProductExists(id))
        {
            return BadRequest("Can't find product");
        }
        repo.Delete(product);
        if (await repo.SaveAllChangesAsync())
        {
            return Ok();
        }
        return BadRequest("Can't delete product");
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrandNamesAsync()
    {
        var spec = new BrandListSpec();
        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypeAsync()
    {
        var spec = new TypeListSpec();
        return Ok(await repo.ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return repo.IsExists(id);
    }





}
