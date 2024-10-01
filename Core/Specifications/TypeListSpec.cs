using Core.Entites;

namespace Core.Specifications;

public class TypeListSpec : BaseScecification<Product, string>
{

    public TypeListSpec()
    {
        AddSelect(x => x.Type);
        ApplyDistint();
    }

}
