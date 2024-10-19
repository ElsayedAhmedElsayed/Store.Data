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
                       (!specs.TypeId.HasValue || pro.TypeId == specs.TypeId.Value)&& 
            (string.IsNullOrEmpty(specs.Search) || pro.Name.Trim().ToLower().Contains(specs.Search)))
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);

            ApplyPaignation(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);

            if (!string.IsNullOrEmpty(specs.Sort)) 
            {
                switch (specs.Sort) 
                {
                    case "PriceAsc":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(x=>x.Price);
                        break;
                    default:
                        AddOrderBy(x=>x.Name);
                        break;
                }
            }
        }

        public ProductWithSpecification(int? id) : base(prod => prod.Id == id) 
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }
    }
}
