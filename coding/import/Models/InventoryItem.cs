using import.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Models
{
    internal class InventoryItem : IInventoryItem
    {
        public string[] Categories { get; set; } = new string[0];
        public string Name { get; set; }
        public string Twitter { get; set; }

    }
}
