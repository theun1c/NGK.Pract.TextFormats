# NGK.Pract.TextFormats

## Основной функционал

Проект реализует систему управления продуктами с поддержкой различных текстовых форматов. Вот ключевые возможности:

Работа с форматами данных
- CSV/TXT (базовый формат)
- JSON (через System.Text.Json)
- XML (через XmlSerializer)

## Структура проекта

NGK.Pract.TextFormats/
├── NGK.Pract.TextFormats/          # Основной проект
│   ├── Models/                     # Модели данных
│   │   └── Product.cs              # Класс Product
│   ├── TextMethods/                # Реализации работы с форматами
│   │   ├── MainMethod.cs           # Базовый класс для CSV/TXT
│   │   ├── JSONMethod.cs           # Работа с JSON
│   │   └── XMLMethod.cs            # Работа с XML
│   └── Program.cs                  # Основная логика приложения
├── NGK.Pract.TextFormats.Tests/    # Тесты
│   └── ProductServiceTests.cs      # Юнит-тесты
└── README.md                       # Основная документация

## Основная DLL
Модель товаров:
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public string Category { get; set; }

    public override string ToString()
    {
        return $"id: {Id} name: {Name} count: {Count} price: {Price} category: {Category}";
    }
}
```

Класс для работы с JSON:
```csharp
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
```

Класс для работы с XML:
```csharp
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
```
## Проект UI
В данном проекте реализован пользовательский интерфейс. Для работы с DLL, необходимо указать ссылку на проект.
Главный метод проекта: 
```csharp
static void Main(string[] args)
{
    MainMethod mainMethod = new MainMethod("products.txt");

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
```
Метод для вывода данных из файла:
```csharp
static void ShowData(MainMethod mainMethod)
{
    List<Product> products = mainMethod.ReadProducts();
    foreach (Product product in products) 
    {
        Console.WriteLine(product.ToString());
    }
}
```
Метод для записи данных в файл:
```csharp
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
```
Метод для сортировки товаров по цене:
```csharp
static void SortData(MainMethod mainMethod) 
{
    List<Product> products = mainMethod.ReadProducts();
    List<Product> sortedProducts = products.OrderByDescending(p => p.Price).ToList();
    foreach (Product product in sortedProducts)
    {
        Console.WriteLine(product.ToString());
    }
}
```
Метод для проверки подстроки:
```csharp
static void SearchData(MainMethod mainMethod, string searchText)
{
    var result = new List<Product>();
    List<Product> products = mainMethod.ReadProducts();
    foreach (var product in products)
    {
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
```

## Проект с Unit тестами
Данный проект содержит в себе файл с юнит-тестами. Для корректной работы, необходимо добавить ссылку на DLL.
Юнит тест для проверки записи в файл (txt/csv):
```csharp
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
```
Юнит тест для проверки записи в файл (json):
```csharp
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
```
Юнит тест для проверки записи в файл (xml):
```csharp
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
```