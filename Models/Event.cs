using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ISC567Web.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string EventLogo { get; set; }
        public bool Promotional { get; set; }
        public int? EventCategoryID { get; set; }
        public int? AdvertiserID { get; set; }


        public virtual EventCategory EventCategory { get; set; }
        public virtual Advertiser Advertiser { get; set; }
    }
    
   
}
