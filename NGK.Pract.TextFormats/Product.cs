//1.	Управление запасами: Считывание и обработка данных о товарах на складе (идентификатор, название, количество, цена, категория)
namespace NGK.Pract.TextFormats
{
    /// <summary>
    /// Product model for DLL
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public string Category { get; set; }
    }
}
