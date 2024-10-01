
using Core.Entites;
using Newtonsoft.Json;

namespace Infra.Data;

public class ContextSeeds
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infra/Data/Seed/products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsData);
            if (products == null) return;
            context.Products.AddRange(products!);
            await context.SaveChangesAsync();
        }
    }
}
