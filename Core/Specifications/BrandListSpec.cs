using System.Linq.Expressions;
using Core.Entites;

namespace Core.Specifications;

public class BrandListSpec : BaseScecification<Product, string>
{
    public BrandListSpec()
    {
        AddSelect(x => x.Brand);
        ApplyDistint();
    }

}
