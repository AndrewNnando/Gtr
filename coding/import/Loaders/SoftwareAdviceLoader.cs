using import.Interfaces;
using import.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace import.Loaders
{
    /// <summary>
    /// This class implements the SoftwareAdvice loading process.
    /// it inherits the operations that must be implemented 
    /// from the BaseLoader class. The BaseLoader class contains the single
    /// public interface (Process) that contains the order that the operations 
    /// will be carried out.
    /// Load and Convert must be overridden.
    /// </summary>
    public class SoftwareAdviceLoader : BaseLoader<SoftwareAdviceLoader>, ISoftwareAdviceLoader
    {
        public SoftwareAdviceLoader(IRepository repository, ILogger<SoftwareAdviceLoader> logger)
            : base(repository, logger)
        {
        }
        protected override object Load()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            ProductList? products =
                 JsonSerializer.Deserialize<ProductList>(fileContents, options);
            if (products == null)
                throw new Exception("The file contents could not be deserialized.");
            return products;
        }


        protected override IInventory Convert()
        {
            var list = (ProductList)Loaded;
            var inventory = new Inventory(name, description);
            foreach (var item in list.Products)
            {
                inventory.Items.Add(Convert(item));
            }
            return inventory;
        }

         InventoryItem Convert(Product product)
        {
            var item = new InventoryItem
            {
                Name = product.Title,
                Categories = product.Categories,
                Twitter = product.Twitter
            };

            return item;
        }
    }
}
