using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.CommandLine;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Builder.CommandParser
{

    internal class CommandLineParser
    {
        private RootCommand _rootCommand { get; set; }
        public string[] Args { get;}
        public bool ErrorParsing { get; private set; }


        public CommandLineParser(string[] args)
        {
            _rootCommand = new();
            Args = args;
            ErrorParsing = false;
        }

        public void ParseArguments()
        {
           
            var commands = CommandGenerator.GenerateCommands();
            // Adding all commands
            foreach (var command in commands)
            {
                if (command != null)
                {
                    _rootCommand.Add(command);
                }
                else
                {
                    // TODO : Improve this exception
                    throw new Exception("Error parsing Commands");
                }
            }

            var task = _rootCommand.InvokeAsync(Args);
            ErrorParsing = Convert.ToBoolean(task.Result);
        }
    }
}
