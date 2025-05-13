using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NGK.Pract.TextFormats
{
    public class JSONMethod
    {
        private string _path;
        public JSONMethod(string path)
        {
            _path = path;
        }
        public void WriteProduct(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products);
            File.WriteAllText(_path, json);
        }
        public List<Product> ReadProducts() 
        {
            string json = File.ReadAllText(_path);
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);
            return products;
        }
    }
}
