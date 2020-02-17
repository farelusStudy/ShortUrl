using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
    }
}