using FinalProject.WebApi.Models.DomainModel.OrderAggregates;
using FinalProject.WebApi.Models.DomainModel.PersonAggregates;
using FinalProject.WebApi.Models.DomainModel.ProductAggregates;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.WebApi.Models
{
    public class ProjectDbContext : DbContext
    {
        #region [-ctors-]
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }
        protected ProjectDbContext()
        {
        }
        #endregion

        #region [-DbSets-]
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        #endregion

        #region [-OnModelCreating-]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderHeaderId, od.ProductId });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.OrderHeader)
                .WithMany(oh => oh.OrderDetails)
                .HasForeignKey(od => od.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<OrderHeader>()
                .HasOne(oh => oh.Buyer)
                .WithMany()
                .HasForeignKey(oh => oh.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHeader>()
                .HasOne(oh => oh.Seller)
                .WithMany()
                .HasForeignKey(oh => oh.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        #endregion
    }
}
