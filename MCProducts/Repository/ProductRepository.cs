using MCProducts.Interfaces;
using MCProducts.Models;
using MCProducts.Models.Dto;

namespace MCProducts.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly McproductsContext _database;

        public ProductRepository()
        {
            _database = new McproductsContext();
        }

        public ProductRepository(McproductsContext database)
        {
            _database = database;
        }


        public IEnumerable<Product> GetAll()
        {
            return _database.Products.ToList();
        }
        public IEnumerable<Product> GetByName(string name)
        {
            return _database.Products.Where(x => x.Title == name).ToList();
        }
        public IEnumerable<Product> GetByCategory(string category)
        {
            return _database.Products.Where(x => x.Category == category).ToList();
        }
        public Product GetById(int id)
        {
            return _database.Products.Where(x => x.Id == id).FirstOrDefault()!;
        }


        public void Update(ProductDto product)
        {
            _database.Products.Update(product);
        }
        public void UpdateQuantity(int id, int quantity)
        {
            Product product = _database.Products.Find(id)!;

            if (product != null)
            {
                product.Quantity = quantity;

                _database.Products.Update(product);
            }
        }


        public void Delete(int id)
        {
            Product product = _database.Products.Find(id)!;

            if (product != null)
            {
                _database.Products.Remove(product);
            }
        }
    }
}
