using Builder.Manager;
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
        public IProjectInfo? ProjectInfo { get; protected set; }

        protected abstract Command LoadCommand();

        public static IProjectInfo GenerateProjectInfo(string projectType, string projectName, string projectDirectory)
        {
            ProjectType projectTypeAsEnum;
            if (!Enum.TryParse(projectType.ToUpper(), out projectTypeAsEnum))
                projectTypeAsEnum = ProjectType.INVALID_TYPE;
            ProjectInfo projectInfo = new(projectTypeAsEnum, projectName, projectDirectory);
            return projectInfo;
        }
    }
}
