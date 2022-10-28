using Builder.Exceptions.Manager;
using Builder.Manager.ProjectInformation;

namespace Builder.Manager.Factory
{
    internal class ManagerFactory : IManagerFactory
    {
        private IProjectInfo _projectInfo;


        public ManagerFactory(ProjectInfo projectInfo)
        {
            _projectInfo = projectInfo;
        }

        public ProjectManager MakeProjectManager()
        {
            switch (_projectInfo.ProjectType)
            {
                case ProjectType.CMAKE:
                    return new CMakeManager(_projectInfo);
                case ProjectType.GIT:
                    return new GitManager(_projectInfo);
                default:
                    throw new InvalidProjectTypeException();
            }
        }
    }
}
