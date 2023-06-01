using MCProducts.Models;
using MCProducts.Models.Dto;

namespace MCProducts.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByName(string name);
        IEnumerable<Product> GetByCategory(string category);
        Product GetById(int id);
        void UpdateQuantity(int id, int quantity);
        void Update(ProductDto product);
        void Delete(int id);
    }
}
