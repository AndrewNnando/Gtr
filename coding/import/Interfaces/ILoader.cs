using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Interfaces
{
    public interface ILoader
    {
        void Process();
        object Loaded { get;  }
        IInventory Converted { get; }
    }
}
