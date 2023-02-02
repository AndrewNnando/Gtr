using import.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Loaders
{
    /// <summary>
    /// This base class is responsible for defining the steps required
    /// to load data. Implementation will be left to the concrete classes.
    /// Repository and logging services are injected into the class via the 
    /// constructor.
    /// </summary>    
    public abstract class BaseLoader<T> : ILoader
    {
        protected readonly IRepository _repository;
        protected readonly ILogger<T> _logger;
        protected readonly string fileContents;
        protected readonly string name;
        protected readonly string description;
        
        public object Loaded { get ; protected set ; }
        public IInventory Converted { get; protected set; }

        protected BaseLoader(IRepository repository, ILogger<T> logger, 
            string contents,
            string name,
            string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrWhiteSpace(contents))
                throw new ArgumentNullException("contents");

            _repository = repository;
            _logger = logger;
            fileContents = contents;
            this.name = name;
            this.description = description;
        }
        public void Process()
        {
            
            Load();
            Convert();
            Save();
        }

        protected abstract void Load();
        protected abstract void Convert();
        protected abstract void Save();

    }
}
