using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Interfaces
{
    public interface IInventory
    {
        string Description { get; }
        int Id { get; }
        List<IInventoryItem> Items { get; }
        string Name { get; }
    }
}
