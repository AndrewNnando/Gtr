using import.Interfaces;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace import.Commands
{
    /// <summary>
    /// Base class for loaders which have a single argument of type FileInfo.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FileInfoArgumentCommand <K,T> : Command
        where K : Command
        where T : ILoader
        
    {
        protected readonly T _loader;
        protected readonly ILogger _logger;

        public FileInfoArgumentCommand(T loader, ILogger<K> logger,
            string name, string description)
            : base(name, description)
        {
            _loader = loader;
            _logger = logger;
            Initialise();
        }

        void Initialise()
        {
            var fileInfoArgument = new Argument<FileInfo>();
            fileInfoArgument.Name = "file";
            fileInfoArgument.Description = "File name of list to be imported.";

            AddArgument(fileInfoArgument);
            this.SetHandler((fileInfo) => CommandHandler(fileInfo), fileInfoArgument);
        }
        void CommandHandler(FileInfo fileInfo)
        {

            try
            {
                var contents = File.ReadAllText(fileInfo.FullName);
                _loader.Process(contents, Name, Description);
                // Output to console
                foreach (var item in _loader.Converted.Items)
                    Console.WriteLine($"importing: {item}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                Console.WriteLine($"Could not import file {fileInfo.FullName}.");

            }
        }
    }
}
