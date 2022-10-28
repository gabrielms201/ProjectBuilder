namespace Builder.Exceptions.Parser
{
    public class InvalidCommandArguments : Exception
    {
        public InvalidCommandArguments()
        {
        }

        public InvalidCommandArguments(string message)
            : base(message)
        {
        }

        public InvalidCommandArguments(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
