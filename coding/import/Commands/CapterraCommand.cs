using import.Interfaces;
using import.Models;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.CommandLine;
using System.Text;
using YamlDotNet.Serialization;

namespace import.Commands
{
    /// <summary>
    /// This class inherits from the Command class in the 
    /// System.CommandLine library. It calls the CapterraLoader instance.
    /// </summary>
    internal sealed class CapterraCommand : Command
    {
        private readonly ICapterraLoader _loader;
        private readonly ILogger _logger;

        public CapterraCommand(ICapterraLoader loader, ILogger<CapterraCommand> logger)
            : base(Strings.CapterraCommand_Name, Strings.CapterraCommand_Name)
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
            var contents = File.ReadAllText(fileInfo.FullName);
            _loader.Process(contents, Name, Description);

            // Output to console
            foreach (var item in _loader.Converted.Items)
                Console.WriteLine($"importing: {item}");
        }             
    }
}
