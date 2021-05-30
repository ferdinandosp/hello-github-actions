using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Model
{
    public partial class Event
    {
        public string Event1 { get; set; }
        public string ChangeId { get; set; }
        public string Change { get; set; }
        public string Project { get; set; }
        public string Note { get; set; }
        public string[] Requires { get; set; }
        public string[] Conflicts { get; set; }
        public string[] Tags { get; set; }
        public DateTime CommittedAt { get; set; }
        public string CommitterName { get; set; }
        public string CommitterEmail { get; set; }
        public DateTime PlannedAt { get; set; }
        public string PlannerName { get; set; }
        public string PlannerEmail { get; set; }

        public virtual Project ProjectNavigation { get; set; }
    }
}
