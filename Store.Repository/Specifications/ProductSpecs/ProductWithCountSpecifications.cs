using Store.Data.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductWithCountSpecifications : BaseSpecification<Product>
    {
        public ProductWithCountSpecifications(ProductSpecification specs) :
            base(pro => (!specs.BrandId.HasValue || pro.BrandId == specs.BrandId.Value) &&
                       (!specs.TypeId.HasValue || pro.TypeId == specs.TypeId.Value)&& 
            (string.IsNullOrEmpty(specs.Search) || pro.Name.Trim().ToLower().Contains(specs.Search)))
        {

        }
    }
}
