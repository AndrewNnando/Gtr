using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Interfaces
{
    public interface IInventoryItem
    {
        string[] Categories { get; set; }
        string Name { get; set; }
        string Twitter { get; set; }
    }
}
