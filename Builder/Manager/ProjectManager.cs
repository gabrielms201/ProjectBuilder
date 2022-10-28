using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    internal class ProjectInfo
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
    }
    internal abstract class ProjectManager
    {
        public ProjectInfo Info { get; set; }
        protected string JsonPath = "projects.json";
        public List<ProjectInfo> Projects { get; set; }
        protected ProjectManager(ProjectInfo info)
        {
            Info = info;
            Projects = new();
        }

        public abstract void OnNewProjectCommand();
        public abstract void SaveProjectInfo();
        public abstract void LoadProjects();
        public abstract void ConfigureProjects();

    }
}
