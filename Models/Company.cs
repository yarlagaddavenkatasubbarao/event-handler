using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC567Web.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public ICollection<Advertiser> Advertisers { get; set; }
    }
}
