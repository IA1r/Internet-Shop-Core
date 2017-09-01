using Microsoft.EntityFrameworkCore;
using Core.Model;
using Core.Interface.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.Context
{
	public class InternetShopContext : IdentityDbContext<User>, IInternetShopContext
	{
		public InternetShopContext(DbContextOptions<InternetShopContext> option)
			: base(option) { }

		public virtual DbSet<Characteristic> Characteristics { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
		public virtual DbSet<ProductType> ProductTypes { get; set; }
		public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public virtual DbSet<CartContent> CartContents { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderContent> OrderContents { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Characteristic>().ToTable("Characteristic");
			modelBuilder.Entity<Product>().ToTable("Product");
			modelBuilder.Entity<ProductCharacteristic>().ToTable("ProductCharacteristic");
			modelBuilder.Entity<ProductType>().ToTable("ProductType");
			modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
			modelBuilder.Entity<CartContent>().ToTable("CartContent");
			modelBuilder.Entity<Order>().ToTable("Order");
			modelBuilder.Entity<OrderContent>().ToTable("OrderContent");

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
		{
			optionBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;attachdbfilename=D:\CSharp\InternetShop\InternetShop.mdf;Trusted_Connection=True;MultipleActiveResultSets=true");
		}
	}

	public class InternetShopContextFactory : IDbContextFactory<InternetShopContext>
	{
		public InternetShopContext Create(DbContextFactoryOptions options)
		{
			var optionsBuilder = new DbContextOptionsBuilder<InternetShopContext>();
			return new InternetShopContext(optionsBuilder.Options);
		}
	}
}
