
using Microsoft.EntityFrameworkCore;
namespace DbOperationWithCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currencies>().HasData(
                new Currencies() { Id = 1, Title = "USD", Description = "United States Dollar" },
                new Currencies() { Id = 2, Title = "EUR", Description = "Euro" },
                new Currencies() { Id = 3, Title = "GBP", Description = "British Pound" },
                new Currencies() { Id = 4, Title = "INR", Description = "Indian INR" }
            );

            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = 1, Title = "English", Description = "English Language" },
               new Language() { Id = 2, Title = "French", Description = "French Language" },
               new Language() { Id = 3, Title = "Spanish", Description = "Spanish Language" },
               new Language() { Id = 4, Title = "Hindi", Description = "Hindi Language" }
               );
               
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<BookPrice> BookPrice { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}






// comand to create migration
//     add-migration addauthortable
//     comand to update database
//     update-database
