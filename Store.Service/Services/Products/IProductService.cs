using Store.Repository.Specifications.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto> GetProductBuIdAsync(int? id);

        Task<PaginatedResultDto <ProductDto>> GetAllProductsAsync(ProductSpecification specs);


        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();

        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();

    }
}
