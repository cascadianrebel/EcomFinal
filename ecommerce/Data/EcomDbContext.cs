using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Data
{
    public class EcomDbContext : DbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options)
        {

        }
        DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ID = 1, Sku = "MNAG0001", Name = "Cat Head Squirrel Feeder", Price = 14.99M, Description = "Make a squirrel who eats from this feeder look like it had a massive cat face", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/Cat-Head-Squirrel-Feeder_1600x.jpg?v=1520533809" },
                new Product { ID = 2, Sku = "MNAG0002", Name = "Squirrel in Underpants Mints", Price = 3.99M, Description = "A tin of mints featuring a squirrel in underpants. Keep your breathe nutty fresh.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/Squirrel-in-Underpants-Mints_1600x.jpg?v=1520533849" },
                new Product { ID = 3, Sku = "MNAG0003", Name = "Handisquirrel - Squirrel Finger Puppet", Price = 6.99M, Description = "Ever wanted your hand to be a squirrel? Now you can with these finger puppets. Includes four paws and a squirrel head.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725" },
                new Product { ID = 4, Sku = "MNAG0004", Name = "Handisquirrel - Squirrel Finger Puppet", Price = 6.99M, Description = "Ever wanted your hand to be a squirrel? Now you can with these finger puppets. Includes four paws and a squirrel head.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725" }
                );
        }
    }
}
