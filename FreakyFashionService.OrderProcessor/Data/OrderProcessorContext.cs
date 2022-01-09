using FreakyFashionService.OrderProcessor.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFashionService.OrderProcessor.Data
{
    public class OrderProcessorContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=.\\MSSQLSERVER02;Database=Order;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasIndex(o => new { o.OrderIdentifier, o.Id })
                .IsUnique();
        }

        public DbSet<Order> Order { get; set; }
    }
}
