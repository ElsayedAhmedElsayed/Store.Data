using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext storeDbContext ,ILoggerFactory loggerFactory)
        {
            try
            {
                
                
                if (storeDbContext.ProductBrands != null && !storeDbContext.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    if (brands is not null)
                    {
                        await storeDbContext.ProductBrands.AddRangeAsync(brands);
                    }
                }

                if (storeDbContext.ProductTypes != null && !storeDbContext.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    if (types is not null)
                    {
                        await storeDbContext.ProductTypes.AddRangeAsync(types);
                    }
                }


                if (storeDbContext.Products != null && !storeDbContext.Products.Any())
                {
                    var productData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products is not null)
                    {
                        await storeDbContext.Products.AddRangeAsync(products);
                    }
                }

                await storeDbContext.SaveChangesAsync();

            }
            catch (Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<StoreDbContext>();

                logger.LogError(ex.Message);
            }

        }
    }
}
