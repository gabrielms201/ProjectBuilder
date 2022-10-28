using Builder.Manager.ProjectInformation;
using System.Text;
using System.Text.Json;

namespace Builder.Manager
{
    public enum ProjectType
    {
        CMAKE,
        GIT,
        INVALID_TYPE
    }

    internal abstract class ProjectManager
    {
        public IProjectInfo CurrentProject { get; set; }
        private bool CurrentProjectAlreadyExists { get; set; }

        protected string JsonPath = System.Reflection.Assembly.GetExecutingAssembly().Location + "\\..\\projects.json";
        public Dictionary<string, IProjectInfo> Projects { get; set; }
        protected ProjectManager(IProjectInfo info)
        {
            CurrentProject = info;
            Projects = new();
        }

        public void OnNewProjectCommand()
        {
            LoadProjects();
            if (CurrentProjectAlreadyExists)
            {
                Console.WriteLine("Project Already exists:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CurrentProject);
                Console.ResetColor();
                return;
            }
            SaveProjectInfo();
            ConfigureProjects();
        }
        public void SaveProjectInfo()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Projects, options);
            using (var stream = File.Open(JsonPath, FileMode.Open))
            {
                stream.Write(Encoding.UTF8.GetBytes(json));
            }
            FileStream builderFile = File.Create(CurrentProject.ProjectDirectory + "\\.builder");
            builderFile.Close();
        }
        public void LoadProjects()
        {
            if (!File.Exists(JsonPath))
            {
                FileStream projectsJson = File.Create(JsonPath);
                projectsJson.Close();
                return;
            }
            string jsonString = File.ReadAllText(JsonPath);

            if (jsonString.Length == 0)
                return;

            var projects = JsonSerializer.Deserialize<Dictionary<string, ProjectInfo>>(jsonString);

            if (projects == null)
                return;

            foreach (var project in projects)
            {
                if (Projects.ContainsKey(project.Key))
                {
                    CurrentProjectAlreadyExists = true;
                    return;
                }
                Projects.Add(project.Key, project.Value);
            }
        }
        public abstract void ConfigureProjects();

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
