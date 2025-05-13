namespace NGK.Pract.TextFormats.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            products.Add(new Product{
                Id = 1,
                Name = "Test1",
                Price = 1,
                Count = 1,
                Category = "Test categ",
            });
            products.Add(new Product
            {
                Id = 2,
                Name = "Test2",
                Price = 231,
                Count = 123,
                Category = "Test categ 2",
            });
            MainMethod mainMethod = new MainMethod();
            mainMethod.WriteProduct(products);
            List<Product> newProd = new List<Product>();
            newProd = mainMethod.ReadProducts();
            foreach (Product product in newProd) 
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}
