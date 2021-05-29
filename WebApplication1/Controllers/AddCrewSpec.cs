using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    public class AddCrewSpec
    {
        public string Name { get; set; }

        public string SeafarerId { get; set; }
        
        public string CreatedBy { get; set; }

        public Crew toCrew()
        {
            Crew crew = new Crew();
            crew.Name = Name;
            crew.SeafarerId = SeafarerId;
            crew.CreatedBy = CreatedBy;
            crew.LastUpdatedBy = CreatedBy;
            crew.CreatedOn = new DateTime();
            crew.LastUpdatedOn = new DateTime();

            return crew;
        }
    }
}
