using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Model
{
    public partial class Project
    {
        public Project()
        {
            Changes = new HashSet<Change>();
            Events = new HashSet<Event>();
            Tags = new HashSet<Tag>();
        }

        public string Project1 { get; set; }
        public string Uri { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }

        public virtual ICollection<Change> Changes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
