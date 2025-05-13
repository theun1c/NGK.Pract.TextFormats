using NGK.Pract.TextFormats.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NGK.Pract.TextFormats.TextMethods
{
    /// <summary>
    /// Main class
    /// CSV and TXT works with WriteProduct and ReadProducts
    /// </summary>
    public class MainMethod
    {
        private string _path;
        public MainMethod(string path)
        {
            _path = path;
        }

        public void WriteProduct(List<Product> products)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_path))
                {
                    foreach (Product product in products)
                    {
                        writer.WriteLine($"{product.Id},{product.Name},{product.Count},{product.Price},{product.Category}");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                if (File.Exists(_path)) 
                {
                    using (StreamReader reader = new StreamReader(_path))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 5)
                            {
                                products.Add(new Product
                                {
                                    Id = Convert.ToInt32(parts[0]),
                                    Name = parts[1],
                                    Count = Convert.ToInt32(parts[2]),
                                    Price = Convert.ToInt32(parts[3]),
                                    Category = parts[4]
                                });
                            }
                        }
                    }
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
