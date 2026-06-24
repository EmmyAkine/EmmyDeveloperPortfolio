using EmmyDeveloperPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace EmmyDeveloperPortfolio.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
