using Builder.Exceptions.Manager;
using Builder.Manager.ProjectInformation;

namespace Builder.Manager
{
    internal class CMakeManager : ProjectManager
    {
        public CMakeManager(IProjectInfo info) : base(info)
        {
            if (info.ProjectName == null)
                throw new InvalidProjectNameException("Project Name is Null");
            Projects.Add(info.ProjectName, info);
        }

        public override void ConfigureProjects()
        {
            
        }
    }
}
