using System.Text.Json.Serialization;

namespace Builder.Manager.ProjectInformation
{
    public interface IProjectInfo
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        ProjectType ProjectType { get; set; }
        string? ProjectName { get; set; }
        string? ProjectDirectory { get; set; }
    }
}
