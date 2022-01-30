using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class AppDbContext : DbContext
    {
        //comment to test  for git
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
