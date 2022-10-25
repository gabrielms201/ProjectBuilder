using Builder.ProgramCommands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
