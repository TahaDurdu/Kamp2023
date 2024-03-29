
internal class Program
{
    private static void Main(string[] args)
    {
        List<Category> catefories = new List<Category>
        {
            new Category{CategoryId=1 , CategoryName ="Bilgisayar"},
            new Category{CategoryId=2, CategoryName ="Telefon"}
        };

        List<Product> products = new List<Product>
        {
            new Product{ProductId=1 , CategoryId = 1 , ProductName="Acer Laptop" , QuantityPerUnit= "32 Gb Ram", UnitPrice = 10000, UnitsInStock=5},
            new Product{ProductId=2 , CategoryId = 1 , ProductName="Asus Laptop" , QuantityPerUnit= "16 Gb Ram", UnitPrice = 80000, UnitsInStock=3},
            new Product{ProductId=3 , CategoryId = 1 , ProductName="Hp Laptop" , QuantityPerUnit= "8 Gb Ram", UnitPrice = 60000, UnitsInStock=2},
            new Product{ProductId=4 , CategoryId = 2 , ProductName="Samsung Telefon" , QuantityPerUnit= "4 Gb Ram", UnitPrice = 50000, UnitsInStock=15},
            new Product{ProductId=5 , CategoryId = 2 , ProductName="Apple Telefon" , QuantityPerUnit= "4 Gb Ram", UnitPrice = 8000, UnitsInStock=0},
        };

        Console.WriteLine("Algoritmik ---------");

        foreach (var product in products)
        {
            if (product.UnitPrice>800 && product.UnitsInStock>3)
            {
                Console.WriteLine(product.ProductName);
            }
        }
        Console.WriteLine("Linqq-------------");

        var result = products.Where(p => p.UnitPrice > 800 && p.UnitsInStock > 3);

        foreach (var product in result)
        {
            Console.WriteLine(product.ProductName);
        }
        //Varmı Yokmu bool döner
       var result2 = products.Any(p => p.ProductName == "Acer Laptop");
        Console.WriteLine(result2);

        // O ürün varmı varsa getir detayına gitmek için kullanılır
        var result3 = products.Find(p => p.ProductId == 3);
        Console.WriteLine(result3.ProductName);



    }

}

class Product
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }

}

class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}