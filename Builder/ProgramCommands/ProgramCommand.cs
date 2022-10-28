using Builder.Manager.ProjectInformation;
using System.CommandLine;

namespace Builder.ProgramCommands
{
    internal abstract class ProgramCommand
    {
        public Command? Content { get; protected set; }
        public IProjectInfo? ProjectInfo { get; protected set; }

        protected abstract Command LoadCommand();
    }
}
