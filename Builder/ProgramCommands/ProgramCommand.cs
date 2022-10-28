using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.ProgramCommands
{
    internal abstract class ProgramCommand
    {
        public Command? Content { get; protected set; }

        protected abstract Command LoadCommand();
    }
}
