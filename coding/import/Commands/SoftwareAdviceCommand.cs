using import.Interfaces;
using Microsoft.Extensions.Logging;

namespace import.Commands
{
    /// <summary>
    /// This class inherits from the FileInfoArgumentCommand class in the 
    /// System.CommandLine library. It calls an instance of SoftwareAdviceLoader.
    /// </summary>
    internal sealed class SoftwareAdviceCommand 
        : FileInfoArgumentCommand<SoftwareAdviceCommand,ISoftwareAdviceLoader>
    {
        public SoftwareAdviceCommand(ISoftwareAdviceLoader loader, ILogger<SoftwareAdviceCommand> logger)
            : base(loader, logger, Strings.SoftwareAdviceCommand_Name, Strings.SoftwareAdviceCommand_Name)
        { }
    }
}
