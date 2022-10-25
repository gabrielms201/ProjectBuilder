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

        // TODO: Create an enum
        private ConcurrentDictionary<string, Option<string>> Options;
        // TODO: Create an enum
        private ConcurrentDictionary<string, Command> Commands;

        public CommandLineParser(string[] args)
        {
            _rootCommand = new();
            Args = args;
            ErrorParsing = false;
            Options = new();
            Commands = new();
        }

        public void ParseArguments()
        {
           
            // TODO: Move it to another method
            // Options:
            Options["ProjectType"] = new Option<string>(
                name: "-type",
                description: "CMAKE project");
            Options["ProjectType"].AddAlias("-t");
            // TODO: Move it to another method
            // Commands:
            Commands["NewCommand"] = new Command("new", "Creates a new project")
            {
                Options["ProjectType"]
            };
            // Command Handlers
            Commands["NewCommand"].SetHandler((projectType) =>
            {
                NewProject(projectType);
            }, Options["ProjectType"]);

            // Adding all commands
            foreach (var command in Commands)
            {
                if (command.Value != null)
                {
                    _rootCommand.Add(command.Value);
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
        public static void NewProject(string projectType)
        {
            Console.WriteLine("NEW PROJECT NAMED " + projectType);
        }
    }
}
