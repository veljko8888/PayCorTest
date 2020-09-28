using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vProductAndDescription>()
                .HasKey(c => new { c.ProductID, c.CultureID });

            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasKey(c => new { c.PurchaseOrderID, c.PurchaseOrderDetailID });
        }

        public DbSet<vProductAndDescription> ViewProducts { get; set; }

        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
