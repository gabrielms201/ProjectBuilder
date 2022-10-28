namespace Builder.Manager
{
    internal class GitManager : ProjectManager
    {
        public GitManager(ProjectInfo info) : base(info)
        {
        }

        public override void ConfigureProjects()
        {
            throw new NotImplementedException();
        }

        public override void LoadProjects()
        {
            throw new NotImplementedException();
        }

        public override void OnNewProjectCommand()
        {
            Console.WriteLine(Info);
        }

        public override void SaveProjectInfo()
        {
            throw new NotImplementedException();
        }
    }
}