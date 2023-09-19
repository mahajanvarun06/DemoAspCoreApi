using SampleCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleCore.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);

        Task<int> PutProduct(int id, Product product);

        Task<Product> PostProduct(Product product);

        Task<int> DeleteProduct(int id);
    }
}
