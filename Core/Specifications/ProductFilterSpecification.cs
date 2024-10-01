using System.Linq.Expressions;
using Core.Entites;

namespace Core.Specifications
{
    public class ProductFilterSpecification : BaseScecification<Product>
    {
        public ProductFilterSpecification(ProductSpecParams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
                (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
                (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type)))
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            switch (specParams.Sort)
            {

                case "pAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "pDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}
