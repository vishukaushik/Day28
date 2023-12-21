using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Model;
using ProductsAPI.Repository;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepo _productrepo;
        public ProductController(IProductRepo productrepo)
        {
            _productrepo = productrepo;
        }
        //Post Api to add products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (_productrepo.AddProduct(product)!=null)
            {
               
                return Created("api/product", product);
            }
            else
            {
                return BadRequest();
            }
        }
        //Get Api to fetch all products
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var result = _productrepo.GetAllProducts();
           return Ok(result);
        }
        //Get Api to fetch a product by id
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductById(int productId)
        {
            var product = _productrepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        //Get Api to fetch a product by name
        [HttpGet("byname/{productName}")]
        public ActionResult<IEnumerable<Product>> GetProductsByName(string productName)
        {
            var products = _productrepo.GetProductsByName(productName);
            return Ok(products);
        }
        //Put Api to update all products
        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, Product updatedProduct)
        {
            _productrepo.UpdateProduct(productId, updatedProduct);
            var product = _productrepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productrepo.DeleteProduct(productId);
            var product = _productrepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
