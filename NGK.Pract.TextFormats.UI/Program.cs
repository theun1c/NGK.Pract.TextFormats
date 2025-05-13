using NGK.Pract.TextFormats.Models;
using NGK.Pract.TextFormats.TextMethods;

namespace NGK.Pract.TextFormats.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<Product> products = new List<Product>();
            //products.Add(new Product{
            //    Id = 1,
            //    Name = "Test1",
            //    Price = 1,
            //    Count = 1,
            //    Category = "Test categ",
            //});
            //products.Add(new Product
            //{
            //    Id = 2,
            //    Name = "Test2",
            //    Price = 231,
            //    Count = 123,
            //    Category = "Test categ 2",
            //});
            MainMethod mainMethod = new MainMethod("products.txt");
            //mainMethod.WriteProduct(products);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ УПРАВЛЕНИЯ ===");
                Console.WriteLine("1. Вывести данные из файла");
                Console.WriteLine("2. Записать данные в файл");
                Console.WriteLine("3. Отсортировать по цене");
                Console.WriteLine("4. Сортировка продуктов");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                if (!int.TryParse(Console.ReadLine(), out int num))
                {
                    Console.WriteLine("Некорректный ввод");
                    Console.ReadLine();
                    continue;
                }

                switch (num) 
                {
                    case 1: ShowData(mainMethod); break;
                    case 2: WriteData(mainMethod); break;
                    case 3: SortData(mainMethod); break;
                    case 4:
                        {
                            Console.Write("введите строку: ");
                            string str = Console.ReadLine();
                            SearchData(mainMethod, str); break;
                        }
                    case 0: return;
                }
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
        static void ShowData(MainMethod mainMethod)
        {
            List<Product> products = mainMethod.ReadProducts();
            foreach (Product product in products) 
            {
                Console.WriteLine(product.ToString());
            }
        }

        static void WriteData(MainMethod mainMethod) 
        {
            Console.WriteLine("Введите код товара");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название товара");
            string name = Console.ReadLine();
            Console.WriteLine("Введите цену товара");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество товара");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите категорию товара");
            string category = Console.ReadLine();

            List<Product> products = new List<Product>();
            products.Add(new Product
            {
                Id = id,
                Name = name,
                Price = price,
                Count = count,
                Category = category,
            });
            mainMethod.WriteProduct(products);
        }

        static void SortData(MainMethod mainMethod) 
        {
            List<Product> products = mainMethod.ReadProducts();
            List<Product> sortedProducts = products.OrderByDescending(p => p.Price).ToList();
            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }

        static void SearchData(MainMethod mainMethod, string searchText)
        {
            var result = new List<Product>();
            List<Product> products = mainMethod.ReadProducts();
            foreach (var product in products)
            {
                // Проверяем, содержится ли подстрока в названии или категории
                if (product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    product.Category.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product);
                }
            }
            foreach( var product in result)
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}
