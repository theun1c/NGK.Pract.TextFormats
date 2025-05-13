using NGK.Pract.TextFormats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NGK.Pract.TextFormats.TextMethods
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
            try
            {
                string json = JsonSerializer.Serialize(products);
                File.WriteAllText(_path, json);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Product> ReadProducts()
        {
            try
            {
                if (File.Exists(_path))
                {
                    string json = File.ReadAllText(_path);
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);
                    return products;
                }
                else
                {
                    return new List<Product>();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
        }
    }
}
