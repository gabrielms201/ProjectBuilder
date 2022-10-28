using Builder.CommandParser;
using Builder.Exceptions.Parser;

namespace Builder
{
    class EntryPoint
    {
        public static int Main(string[] args)
        {
            try
            {
                CommandLineParser parser = new(args);
                parser.ParseArguments();
                if (parser.ErrorParsing)
                    throw new InvalidCommandArguments();

                return 0;
            }
            catch (Exception e)
            {
                return 1;
            }

        }

    }
}