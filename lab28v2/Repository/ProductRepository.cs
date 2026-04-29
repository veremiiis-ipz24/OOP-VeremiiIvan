using System.Text.Json;
using lab28v2.Models;

namespace lab28v2.Repository
{
    public class ProductRepository
    {
        private List<Product> products = new();

        public void Add(Product product)
        {
            products.Add(product);
        }

        public List<Product> GetAll()
        {
            return products;
        }

        public Product GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public async Task SaveToFileAsync(string filename)
        {
            using FileStream fs = File.Create(filename);
            await JsonSerializer.SerializeAsync(fs, products, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        public async Task LoadFromFileAsync(string filename)
        {
            if (!File.Exists(filename))
                return;

            using FileStream fs = File.OpenRead(filename);

            var loaded = await JsonSerializer.DeserializeAsync<List<Product>>(fs);

            if (loaded != null)
                products = loaded;
        }
    }
}
