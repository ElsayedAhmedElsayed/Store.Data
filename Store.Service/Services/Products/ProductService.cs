using AutoMapper;
using Store.Data.Enity;
using Store.Repository.Interfaces;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();

            var MappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return MappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecification(input);

            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);

            var MappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            var countSpecs = new ProductWithCountSpecifications(input);

            var Count = await _unitOfWork.Repository<Product, int>().GetCountWithSpecification(countSpecs);

            return new PaginatedResultDto<ProductDto>(input.PageIndex, input.PageSize, Count, MappedProducts);
        }

  

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            var MappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);


            return MappedTypes;
        }

        public async Task<ProductDto> GetProductBuIdAsync(int? id)
        {
            if (id is null) throw new Exception("Id Is Null");

            var specs = new ProductWithSpecification(id);

            var product = await _unitOfWork.Repository<Product,int>().GetWithSpecificationByIdAsync(specs);

            if (product == null) throw new Exception("Product Not Found");

            var MappedProduct = _mapper.Map<ProductDto>(product);

            return MappedProduct;
        }
    }
}
