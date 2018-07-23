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
        public DbSet<Product> Products { get; set; }

        public DbSet<BasketItem> BasketItemTable { get; set; }

        public DbSet<Basket> BasketTable { get; set; }

        public DbSet<Order> OrderTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ID = 1, Sku = "MNAG0001", Name = "Cat Head Squirrel Feeder", Price = 14.99M, Description = "Make a squirrel who eats from this feeder look like it had a massive cat face", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/Cat-Head-Squirrel-Feeder_1600x.jpg?v=1520533809" },
                new Product { ID = 2, Sku = "MNAG0002", Name = "Squirrel in Underpants Mints", Price = 3.99M, Description = "A tin of mints featuring a squirrel in underpants. Keep your breathe nutty fresh.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/Squirrel-in-Underpants-Mints_1600x.jpg?v=1520533849" },
                new Product { ID = 3, Sku = "MNAG0003", Name = "Handisquirrel - Squirrel Finger Puppet", Price = 6.99M, Description = "Ever wanted your hand to be a squirrel? Now you can with these finger puppets. Includes four paws and a squirrel head.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725" },
                new Product { ID = 4, Sku = "MNAG0004", Name = "Maggie - Squirrel Action Figure", Price = 9.95M, Description = "This hard vinyl 3-3/4” action figure will be with you through thick and thin. Maggie Squirrel is also a doctor and artist. She has devoted her life to healing the sick and doing paintings of Abraham Lincoln doing odd things like playing badminton and shopping for underpants. One of her paintings, Abraham Lincoln making a macaroni necklace for his mother, recently sold for over $5,000.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/maggie-squirrel-strange-friends-action-figure_1600x.jpg?v=1520535597" },
                new Product { ID = 5, Sku = "MNAG0005", Name = "Squirrels throwing shade", Price = 15.00M, Description = "This sunshade will not only protect and cool your car while blocking UV rays, it will also make it appear like it’s full of a squad of sassy squirrels. At 50\" x 27 - 1 / 2\", it’s big enough for most cars. It’s too big for a mini, too small for a truck. Includes two suction cups for simple installation. Folds for easy storage. Metalized polyester laminate.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/car-full-of-squirrels-auto-sunshade_1600x.jpg?v=1520533891" },
                new Product { ID = 6, Sku = "MNAG0006", Name = "Lady Squirrel Pin", Price = 7.99M, Description = "This pin is made of diecast metal and enamel and is 1-1/2” tall.", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/squirrel-in-red-dress-strange-pin_1600x.jpg?v=1520533794" },
                new Product { ID = 7, Sku = "MNAG0007", Name = "Underpants Squirrel-nament", Price = 6.99M, Description = "Every tree needs a squirrel in underpants. In fact, we think you'd be nuts not to have one! Made of glass, it's 4\" tall and includes string or can stand on its own", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725" },
                new Product { ID = 8, Sku = "MNAG0008", Name = "Happy Birthday, Squirrel Friend - Card", Price = 2.99M, Description = "It's a 5\" x 7\" card, with envelope, that expresses your honest emotions in a weird and measured way", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/Squirrel_Birthday_Card_1600x.jpg?v=1520535512" },
                new Product { ID = 9, Sku = "MNAG0009", Name = "Squirrel Shakers", Price = 5.99M, Description = "These ceramic Squirrel Salt and Pepper Shakers will amuse anyone who has ever fantasized about seasoning food with the disembodied heads of tree rats. ", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/squirrel_salt_and_pepper_shakers_1600x.jpg?v=1520534553" },
                new Product { ID = 10, Sku = "MNAG00010", Name = "Squirrel Mask", Price = 6.99M, Description = "Full-sized, adult latex Squirrel Mask. Perfect for all occasions (except dog sitting).", Image = "https://cdn.shopify.com/s/files/1/1365/2497/products/12293-SquirrelMask-Trees03-1_1600x.jpg?v=1520534463" }

                );
        }
    }
}
