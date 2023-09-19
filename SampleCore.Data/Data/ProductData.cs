using Microsoft.EntityFrameworkCore;
using SampleCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCore.Data
{
    public class ProductData : IProductData
    {
        private readonly SampleCoreDbContext _context;

        public ProductData(SampleCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns list of products.
        /// </summary>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Get product by Id.
        /// </summary>
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <summary>
        /// Update product by Id.
        /// </summary>
        public async Task<int> PutProduct(int id, Product product)
        {
            int result = 0;
            _context.Entry(product).State = EntityState.Modified;
            var isProductExist = ProductsExists(id);

            if (isProductExist)
            {
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public bool ProductsExists(int id)
        {
            return _context.Products.Any(x => x.ProductId == id);
        }

        /// <summary>
        /// To create product.
        /// </summary>
        public async Task<Product> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// To delete a product.
        /// </summary>
        public async Task<int> DeleteProduct(int id)
        {
            int result = 0;
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                result = await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
