using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Model
{
    public partial class Dependency
    {
        public string ChangeId { get; set; }
        public string Type { get; set; }
        public string Dependency1 { get; set; }
        public string DependencyId { get; set; }

        public virtual Change Change { get; set; }
        public virtual Change DependencyNavigation { get; set; }
    }
}
