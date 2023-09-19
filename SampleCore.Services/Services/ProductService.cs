using Microsoft.Extensions.Caching.Memory;
using SampleCore.Data;
using SampleCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleCore.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductData _productData;
        private IMemoryCache _cache;
        public ProductService(IProductData productData, IMemoryCache memoryCache)
        {
            _productData = productData;
            _cache = memoryCache;
        }

        /// <summary>
        /// Returns list of products.
        /// </summary>
        public async Task<IEnumerable<Product>> GetProducts()
        {
             return await _productData.GetProducts();
        }

        /// <summary>
        /// Get product by Id.
        /// </summary>
        public async Task<Product> GetProduct(int id)
        {

            var product = await _cache.GetOrCreateAsync("prodId_" + id, entry =>
            {
                return _productData.GetProduct(id);
            });

            return product;
        }

        /// <summary>
        /// Update product by Id.
        /// </summary>
        public async Task<int> PutProduct(int id, Product product)
        {
            var result = await _productData.PutProduct(id, product);
            if(result != 0)
            {
                if (!_cache.TryGetValue<string>("prodId_" + id, out _))
                {
                    _cache.Remove("prodId_" + id);
                }
            }
            return result;
        }

        /// <summary>
        /// To create product.
        /// </summary>
        public async Task<Product> PostProduct(Product product)
        {
            return await _productData.PostProduct(product);
        }

        /// <summary>
        /// To delete a product.
        /// </summary>
        public async Task<int> DeleteProduct(int id)
        {
            var result = await _productData.DeleteProduct(id);
            if (result != 0)
            {
                if (!_cache.TryGetValue<string>("prodId_" + id, out _))
                {
                    _cache.Remove("prodId_" + id);
                }
            }
            return result;
        }
    }
}
