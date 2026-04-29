using lab28v2.Models;
using lab28v2.Repository;

class Program
{
    static async Task Main(string[] args)
    {
        var repo = new ProductRepository();

        var category1 = new Category { Id = 1, Name = "Electronics" };
        var category2 = new Category { Id = 2, Name = "Books" };

        repo.Add(new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200,
            Category = category1
        });

        repo.Add(new Product
        {
            Id = 2,
            Name = "C# Programming Book",
            Price = 40,
            Category = category2
        });

        await repo.SaveToFileAsync("products.json");

        Console.WriteLine("Data saved to JSON.");

        var repo2 = new ProductRepository();
        await repo2.LoadFromFileAsync("products.json");

        Console.WriteLine("Loaded products:");

        foreach (var p in repo2.GetAll())
        {
            Console.WriteLine($"{p.Id} - {p.Name} - {p.Price}$ - {p.Category.Name}");
        }
    }
}
