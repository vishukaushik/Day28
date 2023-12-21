using ProductsAPI.Model;
using System.Text.Json;

namespace ProductsAPI.Repository
{
    public class ProductRepo : IProductRepo
    {
        //json file path
        private const string FilePath = "products.json";
        private List<Product> products; 

        public ProductRepo()
        {
            LoadProducts();
        }
        //to get data from json file
        private void LoadProducts()
        {
            //if products are available
            if (File.Exists(FilePath))
            {
                var jsonData = File.ReadAllText(FilePath);
                products = JsonSerializer.Deserialize<List<Product>>(jsonData);
            }
            //if no products available then create new
            else
            {
                products = new List<Product>();
            }
        }
        //to save prducts in json file after adding product through post
        private void SaveProducts()
        {
            var jsonData = JsonSerializer.Serialize(products);
            File.WriteAllText(FilePath, jsonData);
        }
        //to add products in the list and then saving them to json
        public Product AddProduct(Product prdt)
        {
            products.Add(prdt);
            SaveProducts();
            return prdt;
        }
        //to get all products at once
        public List<Product> GetAllProducts()
        {
            return products;
        }
        //to get products by id
        public Product GetProductById(int productId)
        {
            return products.Find(p => p.ProductId == productId);
        }
        //to get prducts by name
        public List<Product> GetProductsByName(string productName)
        {
            return products.FindAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }
        //to update prducts in list
        public Product UpdateProduct(int productId, Product updatedProduct)
        {
            int index = products.FindIndex(p => p.ProductId == productId);
            if (index != -1)
            {
                products[index] = updatedProduct;
                SaveProducts();
                return products[index];
            }
            return null;
        }
        //to remove product
        public void DeleteProduct(int productId)
        {
            products.RemoveAll(p => p.ProductId == productId);
            SaveProducts();
        }

    }
}
