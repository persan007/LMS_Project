using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS_Project.Database_Access
{
    public class DbContextLayer : DbContext
    {
        // public DbSet<TYPE> TYPE { get; set; }

        public DbContextLayer() : base("DefaultConnection") {}
    }
}