using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC567Web.Models
{
    public class Advertiser
    {
        public int AdvertiserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? CompanyID { get; set; }
        
        public ICollection<Event> Events { get; set; }
        public virtual Company Company { get; set; }
    }
}
