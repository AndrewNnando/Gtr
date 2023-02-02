using import.Interfaces;
using import.Models;
using Microsoft.Extensions.Logging;
using System.Collections;
using YamlDotNet.Serialization;

namespace import.Loaders
{
    /// <summary>
    /// This class implements the Capterra loading process.
    /// it inherits the operations that must be implemented 
    /// from the BaseLoader class. The BaseLoader class contains the single
    /// public interface (Process) that contains the order that the operations 
    /// will be carried out.
    /// </summary>
    public class CapterraLoader : BaseLoader<CapterraLoader>, ICapterraLoader
    {
        public CapterraLoader(IRepository repository, ILogger<CapterraLoader> logger)
            : base(repository, logger)
        {
        }

        protected override void Load()
        {
            var deserializer = new Deserializer();
            var table = deserializer.Deserialize<List<Hashtable>>(new StringReader(fileContents));
            if (table == null)
                throw new Exception("The file contents could not be deserialized.");
            Loaded = table;
        }

        protected override void Convert()
        {
            var inventory = new Inventory(name,description);
            foreach (var item in (List<Hashtable>)Loaded)
            {
                inventory.Items.Add(Convert(item));
            }
            Converted = inventory;
        }

        public static IInventoryItem Convert(Hashtable table)
        {
            IInventoryItem item = new InventoryItem();
            foreach (DictionaryEntry entry in table)
            {
                switch (entry.Key)
                {
                    case "name":
                        item.Name = entry.Value?.ToString();
                        break;
                    case "twitter":
                        item.Twitter = entry.Value?.ToString();
                        break;
                    case "tags":
                        var tagList = ((string)entry.Value).Split(',');
                        item.Categories = tagList;
                        break;
                }
                //Console.WriteLine("- {0} = {1}", entry.Key, entry.Value);
            }
            return item;
        }

        protected override async void Save()
        {
            await SaveAsync(Converted);
        }
        async Task SaveAsync(IInventory output)
        {
            await _repository.SaveAsync(output);
        }
    }
}
