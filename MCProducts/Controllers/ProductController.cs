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
                
                if (products.Count() == 0)
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

                if(products.Count() == 0)
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
    }
}
