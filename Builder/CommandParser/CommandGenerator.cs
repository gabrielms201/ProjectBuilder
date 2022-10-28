using Builder.ProgramCommands;
using System.CommandLine;

namespace Builder.CommandParser
{

    internal class CommandGenerator
    {
        public static List<Command?> GenerateCommands()
        {
            List<Command?> commands = new();
            commands.Add(new NewCommand().Content);
            return commands;
        }
    }
}
