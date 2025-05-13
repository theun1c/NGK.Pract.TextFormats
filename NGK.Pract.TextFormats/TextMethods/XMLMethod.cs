using NGK.Pract.TextFormats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGK.Pract.TextFormats.TextMethods
{
    public class XMLMethod
    {
        private string _path;
        public XMLMethod(string path)
        {
            _path = path;
        }
        public void WriteProduct(List<Product> products)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                using (StreamWriter writer = new StreamWriter(_path))
                {
                    serializer.Serialize(writer, products);
                }
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
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                    using (StreamReader reader = new StreamReader(_path))
                    {
                        List<Product> products = (List<Product>)serializer.Deserialize(reader);
                        return products;
                    }
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
