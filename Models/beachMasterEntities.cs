using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace beachmaster.Models
{
    public class beachMasterEntities : DbContext
    {
        public DbSet<beach> beach { get; set; }
        public DbSet<review> review { get; set; }
    }
}