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
            Projects.AddFirst(info);
        }

        public override void ConfigureProjects()
        {
            
        }
    }
}
