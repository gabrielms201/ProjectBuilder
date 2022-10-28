using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Builder.Manager
{
    public enum ProjectType
    {
        CMAKE,
        GIT,
        INVALID_TYPE
    }

    interface IProjectInfo
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        ProjectType ProjectType { get; set; }
        string? ProjectName { get; set; }
        string? ProjectDirectory { get; set; }
    }
    internal class ProjectInfo : IProjectInfo
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectType ProjectType { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDirectory { get; set; }

        public ProjectInfo(ProjectType projectType, string? projectName, string? projectDirectory)
        {
            ProjectType = projectType;
            ProjectName = projectName;
            ProjectDirectory = projectDirectory;

        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as ProjectInfo);
        }
        public bool Equals(ProjectInfo? other)
        {
            if (other != null)
                if (other.ProjectName == ProjectName || other.ProjectDirectory == ProjectDirectory)
                    return true;
            return false;
        }

        public override string ToString()
        {
            return ProjectType.ToString() + ":" + ProjectName + ":" + ProjectDirectory;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    internal abstract class ProjectManager
    {
        public IProjectInfo CurrentProject { get; set; }
        private bool CurrentProjectAlreadyExists { get; set; }

        protected string JsonPath = System.Reflection.Assembly.GetExecutingAssembly().Location + "\\..\\projects.json";
        public LinkedList<IProjectInfo> Projects { get; set; }
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

            var projects = JsonSerializer.Deserialize<List<ProjectInfo>>(jsonString);

            if (projects == null)
                return;

            foreach (var project in projects)
            {
                if (Projects.Contains(project))
                {
                    CurrentProjectAlreadyExists = true;
                    return;
                }
                Projects.AddLast(project);
            }
        }
        public abstract void ConfigureProjects();
    }
}
