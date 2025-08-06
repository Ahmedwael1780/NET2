using Microsoft.EntityFrameworkCore;
using Net2.Models;

namespace Net2.Context
{
    
    
        public class ApplicationContext : DbContext
        {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=App;Trusted_Connection=True;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Configure relationships
                modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Seed data
                modelBuilder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic devices and accessories" },
                    new Category { CategoryId = 2, Name = "Clothing", Description = "Apparel and fashion items" },
                    new Category { CategoryId = 3, Name = "Books", Description = "Books and literature" }
                );
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Title = "Mobile",
                Price = 19.99m,
                Description = "This is a sample product",
                Quantity = 10,
                CategoryId = 1 // Make sure this category exists
            },
        new Product
        {
            ProductId = 2,
            Title = "Tshirt",
            Price = 29.99m,
            Description = "Another sample product",
            Quantity = 5,
            CategoryId = 2
        }
    );

        }
        }
}
