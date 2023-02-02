using import.Interfaces;
using Microsoft.Extensions.Logging;

namespace import.Commands
{
    /// <summary>
    /// This class inherits from the FileInfoArgumentCommand class in the 
    /// System.CommandLine library. It calls an isntance of ICapterraLoader.
    /// </summary>
    internal sealed class CapterraCommand
         : FileInfoArgumentCommand<CapterraCommand, ICapterraLoader>
    {
        public CapterraCommand(ICapterraLoader loader, ILogger<CapterraCommand> logger)
            : base(loader, logger, Strings.CapterraCommand_Name, Strings.CapterraCommand_Name)
        { }
    }
}
