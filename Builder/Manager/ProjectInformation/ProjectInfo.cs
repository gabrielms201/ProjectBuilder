using System.Text.Json.Serialization;

namespace Builder.Manager.ProjectInformation
{
    public class ProjectInfo : IProjectInfo
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
}
