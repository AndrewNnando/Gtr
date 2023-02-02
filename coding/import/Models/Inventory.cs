using import.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Models
{
    internal class Inventory : IInventory
    {
        public Inventory(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<IInventoryItem>();
        }
        public string Description { get; set; }

        public int Id { get; set; }

        public List<IInventoryItem> Items { get; }

        public string Name { get; set; }
    }
}
