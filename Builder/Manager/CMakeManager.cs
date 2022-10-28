using Builder.Exceptions.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
