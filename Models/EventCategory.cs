using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC567Web.Models
{
    public class EventCategory
    {
        public int EventCategoryID { get; set; }
        public string EventCategoryLogo { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
