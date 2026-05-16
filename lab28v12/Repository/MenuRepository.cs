using System.Text.Json;
using lab28v12.Models;

namespace lab28v12.Repository
{
    public class MenuRepository
    {
        private List<MenuItem> _items = new();

        public void Add(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.Add(item);
        }

        public List<MenuItem> GetAll()
        {
            return _items;
        }

        public MenuItem? GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public async Task SaveToFileAsync(string filename)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using FileStream fs = new FileStream(filename, FileMode.Create);
            await JsonSerializer.SerializeAsync(fs, _items, options);
        }

        public async Task LoadFromFileAsync(string filename)
        {
            if (!File.Exists(filename))
            {
                _items = new List<MenuItem>();
                return;
            }

            using FileStream fs = new FileStream(filename, FileMode.Open);

            var data = await JsonSerializer.DeserializeAsync<List<MenuItem>>(fs);

            _items = data ?? new List<MenuItem>();
        }
    }
}