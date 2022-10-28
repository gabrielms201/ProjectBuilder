using Builder.Exceptions.Manager;

namespace Builder.Manager.Factory
{
    internal class ManagerFactory : IManagerFactory
    {
        private ProjectType ProjectType;
        private string ProjectName;
        private string ProjectDirectory;

        public ManagerFactory(string projectType, string projectName, string projectDirectory)
        {
            if (!Enum.TryParse(projectType, out ProjectType))
                ProjectType = ProjectType.INVALID_TYPE;
            ProjectName = projectName;
            ProjectDirectory = projectDirectory;
        }

        public ProjectManager MakeProjectManager()
        {
            ProjectInfo info = new(ProjectType, ProjectName, ProjectDirectory);
            switch (info.ProjectType)
            {
                case ProjectType.CMAKE:
                    return new CMakeManager(info);
                case ProjectType.GIT:
                    return new GitManager(info);
                default:
                    throw new InvalidProjectTypeException();
            }
        }
    }
}
