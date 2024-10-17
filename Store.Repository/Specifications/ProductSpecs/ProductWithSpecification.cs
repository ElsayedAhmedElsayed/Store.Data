using Store.Data.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Enity;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductWithSpecification : BaseSpecification<Product>
    {
        public ProductWithSpecification(ProductSpecification specs):
            base(pro=> (!specs.BrandId.HasValue || pro.BrandId == specs.BrandId.Value) &&
                       (!specs.TypeId.HasValue || pro.TypeId == specs.TypeId.Value))
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }
    }
}
