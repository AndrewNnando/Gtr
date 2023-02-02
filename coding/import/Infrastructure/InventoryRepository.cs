using import.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Infrastructure
{
    internal class InventoryRepository : IRepository
    {
        public async Task SaveAsync(IInventory inventoryList)
        {
            await Task.Delay(10);
        }

    }
}
