using FreakyFashionService.CatalogService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionService.CatalogService.Data
{
    public class CatalogServiceContext : DbContext
    {
        public CatalogServiceContext(DbContextOptions<CatalogServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
