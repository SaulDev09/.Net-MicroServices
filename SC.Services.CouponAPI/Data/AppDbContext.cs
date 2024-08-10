using Microsoft.EntityFrameworkCore;
using SC.Services.CouponAPI.Models;

namespace SC.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(DbContextOptions options) : base(options)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "1ABCD",
                DiscountAmount = 2,
                MinAmount = 3,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "2ABCD",
                DiscountAmount = 3,
                MinAmount = 4,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                CouponCode = "3ABCD",
                DiscountAmount = 4,
                MinAmount = 5,
            });
        }
    }
}
