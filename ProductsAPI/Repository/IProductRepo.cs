using ProductsAPI.Model;

namespace ProductsAPI.Repository
{
    public interface IProductRepo
    {
        public Product AddProduct(Product prdt);
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public List<Product> GetProductsByName(string name);
        public void DeleteProduct(int id);
        public Product UpdateProduct(int productId, Product product);
    }
}
