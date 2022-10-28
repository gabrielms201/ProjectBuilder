using System.CommandLine;

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
            }

            var task = _rootCommand.InvokeAsync(Args);
            ErrorParsing = Convert.ToBoolean(task.Result);
        }
    }
}
