using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Builder.Manager
{
    internal class CMakeManager : ProjectManager
    {
        public CMakeManager(ProjectInfo info) : base(info)
        {
            Projects.Add(info);
        }

        public override void ConfigureProjects()
        {
            File.Create(Info.ProjectDirectory + "\\.builder");
        }

        public override void LoadProjects()
        {
            if (!File.Exists(JsonPath))
            {
                File.Create(JsonPath);
                return;
            }
            string jsonString = File.ReadAllText(JsonPath);

            if (jsonString.Length == 0)
            {
                return;
            }
            var projects = JsonSerializer.Deserialize<List<ProjectInfo>>(jsonString);

            if (projects == null)
            {
                return;
            }
            foreach (var project in projects)
            {
                if (!Projects.Contains(project))
                    Projects.Add(project);
            }
        }

        public override void OnNewProjectCommand()
        {
            LoadProjects();
            if (File.Exists(Info.ProjectDirectory + "\\.builder"))
            {
                Console.WriteLine("Project Already exists:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Info);
                Console.ResetColor();
                return;
            }
            SaveProjectInfo();
            ConfigureProjects();
        }
        public override void SaveProjectInfo()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Projects, options);
            File.WriteAllText(JsonPath, json);
        }
    }
}
