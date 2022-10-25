using Builder.CommandParser;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Builder
{
    class EntryPoint
    {
        public static int Main(string[] args)
        {
            CommandLineParser parser = new(args);
            parser.ParseArguments();
            if (parser.ErrorParsing)
                return 1;

            return 0;
        }

    }
}