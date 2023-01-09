using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcDeneme.Models;

namespace MvcDeneme.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<MvcDeneme.Models.Category> Category { get; set; }
    }
}
