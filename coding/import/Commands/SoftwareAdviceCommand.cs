using import.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import.Commands
{
    /// <summary>
    /// This class inherits from the Command class in the 
    /// System.CommandLine library. It calls the SoftwareAdviceLoader instance.
    /// </summary>

    internal sealed class SoftwareAdviceCommand : Command
    {
        private readonly ISoftwareAdviceLoader _loader;
        private readonly ILogger _logger;

        public SoftwareAdviceCommand(ISoftwareAdviceLoader loader, ILogger<SoftwareAdviceCommand> logger)
            : base(Strings.SoftwareAdviceCommand_Name, Strings.SoftwareAdviceCommand_Name)
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
