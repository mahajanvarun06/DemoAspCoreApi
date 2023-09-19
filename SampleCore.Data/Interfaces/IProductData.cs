using SampleCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleCore.Data
{
    public interface IProductData
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);

        Task<int> PutProduct(int id, Product product);

        bool ProductsExists(int id);

        Task<Product> PostProduct(Product product);

        Task<int> DeleteProduct(int id);
    }
}
