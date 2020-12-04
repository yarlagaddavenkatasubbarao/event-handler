using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ISC567Web.Models;

namespace ISC567Web.DAL
{
    public class ISC567WebContext : DbContext
    {
        public ISC567WebContext(DbContextOptions<ISC567WebContext> options) : base(options) { }

        public DbSet<Advertiser> Advertisers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
    }
}
