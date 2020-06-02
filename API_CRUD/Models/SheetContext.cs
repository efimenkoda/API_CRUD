using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Models
{
    public class SheetContext : DbContext
    {
        public DbSet<Sheet1> Sheet1s { get; set; }
        public DbSet<Sheet2> Sheet2s { get; set; }
        public SheetContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
