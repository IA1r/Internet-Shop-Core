using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data.Context;

namespace Data.Migrations
{
    [DbContext(typeof(InternetShopContext))]
    [Migration("20170819111304_HotFix1")]
    partial class HotFix1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Model.CartContent", b =>
                {
                    b.Property<int>("CartContentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductID");

                    b.Property<string>("ShoppingCartID");

                    b.HasKey("CartContentID");

                    b.HasIndex("ProductID");

                    b.HasIndex("ShoppingCartID");

                    b.ToTable("CartContent");
                });

            modelBuilder.Entity("Core.Model.Characteristic", b =>
                {
                    b.Property<int>("CharacteristicID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CharacteristicID");

                    b.ToTable("Characteristic");
                });

            modelBuilder.Entity("Core.Model.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("DeliveryAddress");

                    b.Property<string>("GuestID");

                    b.Property<bool>("IsApprove");

                    b.Property<string>("Phone");

                    b.Property<double>("TotalPrice");

                    b.Property<string>("UserID");

                    b.Property<string>("UserName");

                    b.HasKey("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Core.Model.OrderContent", b =>
                {
                    b.Property<int>("OrderContentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderID");

                    b.Property<int>("ProductID");

                    b.HasKey("OrderContentID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderContent");
                });

            modelBuilder.Entity("Core.Model.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductTypeID");

                    b.HasKey("ProductID");

                    b.HasIndex("ProductTypeID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Core.Model.ProductCharacteristic", b =>
                {
                    b.Property<int>("ProductCharacteristicID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacteristicID");

                    b.Property<int>("ProductID");

                    b.Property<string>("Value");

                    b.HasKey("ProductCharacteristicID");

                    b.HasIndex("CharacteristicID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductCharacteristic");
                });

            modelBuilder.Entity("Core.Model.ProductType", b =>
                {
                    b.Property<int>("ProductTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("ProductTypeID");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("Core.Model.ShoppingCart", b =>
                {
                    b.Property<string>("ShoppingCartID");

                    b.HasKey("ShoppingCartID");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("Core.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Core.Model.CartContent", b =>
                {
                    b.HasOne("Core.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Model.ShoppingCart", "ShoppingCart")
                        .WithMany("CartContents")
                        .HasForeignKey("ShoppingCartID");
                });

            modelBuilder.Entity("Core.Model.Order", b =>
                {
                    b.HasOne("Core.Model.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("Core.Model.OrderContent", b =>
                {
                    b.HasOne("Core.Model.Order", "Order")
                        .WithMany("OrderContents")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Model.Product", b =>
                {
                    b.HasOne("Core.Model.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Model.ProductCharacteristic", b =>
                {
                    b.HasOne("Core.Model.Characteristic", "Characteristic")
                        .WithMany("ProductCharacteristics")
                        .HasForeignKey("CharacteristicID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Model.Product", "Product")
                        .WithMany("ProductCharacteristics")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Core.Model.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Core.Model.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Model.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
