using Store.Data.Enity;
using Store.Repository.Interfaces;
using Store.Service.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();

            IReadOnlyList<BrandTypeDetailsDto> MappedBrands = brands.Select(b => new BrandTypeDetailsDto
            {
                Id = b.Id,
                CreatedAt = b.CreatedAt,
                Name = b.Name,
            }).ToList();

            return MappedBrands;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();

            var MAppedProducts = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                PictureUrl = p.PictureUrl,
                CreatedAt = p.CreatedAt,
                BrandName = p.Brand.Name,
                TypeName = p.Type.Name,
            }).ToList();

            return MAppedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            var MappedTypes = types.Select(t => new BrandTypeDetailsDto
            {
                Id = t.Id,
                Name = t.Name,
                CreatedAt = t.CreatedAt

            }).ToList();

            return MappedTypes;
        }

        public async Task<ProductDto> GetProductBuIdAsync(int? id)
        {
            if (id is null) throw new Exception("Id Is Null");

            var product = await _unitOfWork.Repository<Product,int>().GetByIdAsync(id.Value);

            if (product == null) throw new Exception("Product Not Found");

            var MappedProduct = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                BrandName = product.Brand.Name,
                CreatedAt = product.CreatedAt,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                TypeName = product.Type.Name
            };

            return MappedProduct;
        }
    }
}
