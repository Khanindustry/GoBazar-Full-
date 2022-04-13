using Finalproject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<BrandsLogo> BrandsLogos { get; set; }
        public DbSet<Category> Categories { get; set; }    
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FlashDeal> FlashDeals { get; set; }
        public DbSet<FlashDeal2> FlashDeal2s{ get; set; }
        public DbSet<HomeImage1> HomeImage1s { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Socials> Socials{ get; set; }
        public DbSet<Specials> Specials { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
    }
}
