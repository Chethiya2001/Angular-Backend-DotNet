using Core.Entites;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class ProductRepository(AppDbContext context) : IProductRepository
{


    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandNamesAsync()
    {
        return await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
    }


    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        return product!;

    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types, string? sorts)
    {
        var quary = context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(brands))
        {
            quary = quary.Where(p => p.Brand == brands);
        }
        if (!string.IsNullOrEmpty(types))
        {
            quary = quary.Where(p => p.Type == types);
        }

        quary = sorts switch
        {
            "pAsc" => quary.OrderBy(p => p.Price),
            "pDesc" => quary.OrderByDescending(p => p.Price),
            _ => quary.OrderBy(p => p.Name),
        };

        return await quary.ToListAsync();
    }




    public async Task<IReadOnlyList<string>> GetTypeAsync()
    {
        return await context.Products.Select(p => p.Type).Distinct().ToListAsync();
    }


    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveAllChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

}
