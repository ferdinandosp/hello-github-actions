using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Model
{
    public partial class Tag
    {
        public string TagId { get; set; }
        public string Tag1 { get; set; }
        public string Project { get; set; }
        public string ChangeId { get; set; }
        public string Note { get; set; }
        public DateTime CommittedAt { get; set; }
        public string CommitterName { get; set; }
        public string CommitterEmail { get; set; }
        public DateTime PlannedAt { get; set; }
        public string PlannerName { get; set; }
        public string PlannerEmail { get; set; }

        public virtual Change Change { get; set; }
        public virtual Project ProjectNavigation { get; set; }
    }
}
