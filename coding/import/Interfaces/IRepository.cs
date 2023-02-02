using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Interfaces
{
    public interface IRepository
    {
        Task SaveAsync(IInventory inventory);
    }
}
