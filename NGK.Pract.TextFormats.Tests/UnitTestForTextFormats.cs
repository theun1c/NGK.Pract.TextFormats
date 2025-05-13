using NGK.Pract.TextFormats.Models;
using NGK.Pract.TextFormats.TextMethods;

namespace NGK.Pract.TextFormats.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddProduct_ShouldReturnBasicString()
        {
            //arrange
            string path = "products.txt";
            var service = new MainMethod(path);
            var products = new List<Product> { new Product
            {
                Id = 1,
                Name = "Test",
                Price = 1,
                Count = 1,
                Category = "Test",
            } };

            //act
            service.WriteProduct(products);
            
            //assert
            Assert.True(File.Exists(path));
            var content = File.ReadAllText(path);
            Assert.Contains("1,Test,1,1,Test", content);
        }

        [Fact]
        public void AddProduct_ShouldReturnJSONString() 
        {
            //arrange
            string path = "products.json";
            var service = new JSONMethod(path);
            var products = new List<Product> { new Product
            {
                Id = 1,
                Name = "Test",
                Price = 1,
                Count = 1,
                Category = "Test",
            } };

            //act
            service.WriteProduct(products);

            //assert
            Assert.True(File.Exists(path));
            var content = File.ReadAllText(path);
            Assert.Contains("[{\"Id\":1,\"Name\":\"Test\",\"Count\":1,\"Price\":1,\"Category\":\"Test\"}]", content);
        }
        [Fact]
        public void AddProduct_ShouldReturnXMLString()
        {
            //arrange
            string path = "products.xml";
            var service = new XMLMethod(path);
            var products = new List<Product> { new Product
            {
                Id = 1,
                Name = "Test",
                Price = 1,
                Count = 1,
                Category = "Test",
            } };

            //act
            service.WriteProduct(products);

            //assert
            Assert.True(File.Exists(path));
            var content = File.ReadAllText(path);
            Assert.Contains($"{content}", content);
        }
    }
}