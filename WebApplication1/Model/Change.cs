using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Model
{
    public partial class Change
    {
        public Change()
        {
            DependencyChanges = new HashSet<Dependency>();
            DependencyDependencyNavigations = new HashSet<Dependency>();
            Tags = new HashSet<Tag>();
        }

        public string ChangeId { get; set; }
        public string ScriptHash { get; set; }
        public string Change1 { get; set; }
        public string Project { get; set; }
        public string Note { get; set; }
        public DateTime CommittedAt { get; set; }
        public string CommitterName { get; set; }
        public string CommitterEmail { get; set; }
        public DateTime PlannedAt { get; set; }
        public string PlannerName { get; set; }
        public string PlannerEmail { get; set; }

        public virtual Project ProjectNavigation { get; set; }
        public virtual ICollection<Dependency> DependencyChanges { get; set; }
        public virtual ICollection<Dependency> DependencyDependencyNavigations { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
