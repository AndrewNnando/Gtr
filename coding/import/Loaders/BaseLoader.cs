using import.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
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
        protected string fileContents;
        protected string name;
        protected string description;

        public object Loaded { get; protected set; }
        public IInventory Converted { get; protected set; }

        protected BaseLoader(IRepository repository, ILogger<T> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public void Process(string contents, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrWhiteSpace(contents))
                throw new ArgumentNullException("contents");

            this.fileContents = contents;
            this.name = name;
            this.description = description;

            LoadData();
            ConvertData();
            Save();
        }

        protected abstract object Load();
        protected abstract IInventory Convert();

        void LoadData()
        {
            Loaded = Load();
        }
        void ConvertData()
        {
            Converted = Convert();
        }

        async void Save()
        {
            await SaveAsync(Converted);
        }
        async Task SaveAsync(IInventory output)
        {
            await _repository.SaveAsync(output);
        }

    }
}
