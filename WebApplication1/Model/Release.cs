using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Model
{
    public partial class Release
    {
        public float Version { get; set; }
        public DateTime InstalledAt { get; set; }
        public string InstallerName { get; set; }
        public string InstallerEmail { get; set; }
    }
}
