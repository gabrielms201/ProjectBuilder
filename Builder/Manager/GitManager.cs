namespace Builder.Manager
{
    internal class GitManager : ProjectManager
    {
        public GitManager(IProjectInfo info) : base(info)
        {
        }

        public override void ConfigureProjects()
        {
            throw new NotImplementedException();
        }
    }
}