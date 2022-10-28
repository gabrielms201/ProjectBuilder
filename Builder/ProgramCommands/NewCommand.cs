using Builder.Manager;
using Builder.Manager.Factory;
using Builder.Manager.ProjectInformation;
using System.Collections.Concurrent;
using System.CommandLine;


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
                ProjectInfo projectInfo = (ProjectInfo) ProjectManager.GenerateProjectInfo(projectType, projectName, projectDirectory);
                NewProject(projectInfo);
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

        public static void NewProject(ProjectInfo info)
        {
            try
            {
                ManagerFactory factory = new(info);
                ProjectManager manager = factory.MakeProjectManager();
                manager.OnNewProjectCommand();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
            }

        }

    }
}
