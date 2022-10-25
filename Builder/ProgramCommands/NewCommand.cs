using Builder.CommandParser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.CommandLine;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.ProgramCommands
{
    internal class NewCommand : ProgramCommand
    {
        internal enum NewCommandOptions
        {
            ProjectType,
            ProjectName,
            ProjectDirectory
        }
        
        public NewCommand()
        {
            Content = LoadCommand();
        }
        protected override Command LoadCommand()
        {
            var options = GenerateOptions();
            Command command = new Command("new", "Creates a new project")
            {
                options[NewCommandOptions.ProjectType],
                options[NewCommandOptions.ProjectName],
                options[NewCommandOptions.ProjectDirectory]
            };
            command.SetHandler((projectType, projectName, projectDirectory) =>
            {
                NewProject(projectType.ToLower(), projectName, projectDirectory);
            }, 
            options[NewCommandOptions.ProjectType], 
            options[NewCommandOptions.ProjectName],
            options[NewCommandOptions.ProjectDirectory]
            );
            return command;
        }

        private static ConcurrentDictionary<NewCommandOptions, Option<string>> GenerateOptions()
        {
            ConcurrentDictionary<NewCommandOptions, Option<string>> options = new();
            // ProjectType Option
            options[NewCommandOptions.ProjectType] = new Option<string>(
                name: "-type",
                description: "Project Type"
                );
            options[NewCommandOptions.ProjectType].AddAlias("-t");
            options[NewCommandOptions.ProjectType].IsRequired = true;

            // ProjectType Name
            options[NewCommandOptions.ProjectName] = new Option<string>(
                name: "-name",
                description: "Project Name"
                );
            options[NewCommandOptions.ProjectName].AddAlias("-n");
            options[NewCommandOptions.ProjectName].SetDefaultValue("MyProject");

            // ProjectDirectory
            options[NewCommandOptions.ProjectDirectory] = new Option<string>(
                name: "-directory",
                description: "Project Directory"
                );
            options[NewCommandOptions.ProjectDirectory].AddAlias("-d");
            options[NewCommandOptions.ProjectDirectory].SetDefaultValue(Environment.CurrentDirectory);

            return options;
        }

        public static void NewProject(string projectType, string projectName, string projectDirectory)
        {
            Console.WriteLine("NEW PROJECT NAMED " + projectType + " NAME: " + projectName + " DIR: " + projectDirectory);
        }

    }
}
