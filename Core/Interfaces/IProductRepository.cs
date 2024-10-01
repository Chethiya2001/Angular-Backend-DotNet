using Core.Entites;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types,string? sort);
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandNamesAsync();
      Task<IReadOnlyList<string>> GetTypeAsync();
    void AddProduct(Product product);
    void DeleteProduct(Product product);
    void UpdateProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveAllChangesAsync();
}
