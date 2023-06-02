using MCProducts.Interfaces;
using MCProducts.Models;
using MCProducts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MCProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController()
        {
            var _context = new McproductsContext();

            _productRepository = new ProductRepository(_context);
        }


        [HttpGet("all")]
        public ActionResult GetAll()
        {
            try
            {
                var products = _productRepository.GetAll();

                if (IsEmpty(products))
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult GetByName(string name)
        {
            try
            {
                var products = _productRepository.GetByName(name);

                if (IsEmpty(products))
                {
                    return NotFound();
                }

                return Ok(_productRepository.GetByName(name));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("category/{category}")]
        public ActionResult GetByCategory(string category)
        {
            try
            {
                var products = _productRepository.GetByCategory(category);

                if (IsEmpty(products))
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var product = _productRepository.GetById(id);

                if (IsNull(product))
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static bool IsEmpty(IEnumerable<Product> products)
        {
            return !products.Any();
        }
        private static bool IsNull(Product product)
        {
            return product is null;
        }
    }
}
