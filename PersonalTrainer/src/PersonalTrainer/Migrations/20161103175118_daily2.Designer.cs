using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Framework.DataBaseContext;

namespace PersonalTrainer.Migrations
{
    [DbContext(typeof(DailyFoodContext))]
    [Migration("20161103175118_daily2")]
    partial class daily2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Framework.Models.Database.DailyFood", b =>
                {
                    b.Property<Guid>("DailyFoodId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("TotalCalories");

                    b.Property<decimal>("TotalCarbohydrates");

                    b.Property<decimal>("TotalFat");

                    b.Property<decimal>("TotalFibre");

                    b.Property<decimal>("TotalProteins");

                    b.Property<Guid>("UserId");

                    b.HasKey("DailyFoodId");

                    b.HasIndex("UserId");

                    b.ToTable("DailyFood");
                });

            modelBuilder.Entity("Framework.Models.Database.DailyFoodProduct", b =>
                {
                    b.Property<Guid>("DailyFoodId");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("MealType");

                    b.Property<int>("Quantity");

                    b.HasKey("DailyFoodId", "ProductId");

                    b.HasIndex("DailyFoodId");

                    b.HasIndex("ProductId");

                    b.ToTable("DailyFoodProduct");
                });

            modelBuilder.Entity("Framework.Models.Database.Product", b =>
                {
                    b.Property<Guid>("ProductId");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ProductState");

                    b.Property<int>("ProductType");

                    b.Property<Guid>("UserId");

                    b.HasKey("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Framework.Models.Database.ProductDetails", b =>
                {
                    b.Property<Guid>("ProductId");

                    b.Property<decimal>("Calories");

                    b.Property<decimal>("Carbohydrates");

                    b.Property<decimal>("Fat");

                    b.Property<decimal>("Fibre");

                    b.Property<decimal>("Protein");

                    b.Property<int>("Quantity");

                    b.Property<int>("QuantityType");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Framework.Models.Database.User", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("HashCode")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Framework.Models.Database.UserDetails", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<int>("Age");

                    b.Property<int>("Gender");

                    b.Property<decimal>("Height");

                    b.Property<int>("HeightUnit");

                    b.Property<decimal>("Weight");

                    b.HasKey("UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Framework.Models.Database.DailyFood", b =>
                {
                    b.HasOne("Framework.Models.Database.User", "User")
                        .WithMany("DailyFood")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Framework.Models.Database.DailyFoodProduct", b =>
                {
                    b.HasOne("Framework.Models.Database.DailyFood", "DailyFood")
                        .WithMany("DailyFoodProducts")
                        .HasForeignKey("DailyFoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Framework.Models.Database.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Framework.Models.Database.Product", b =>
                {
                    b.HasOne("Framework.Models.Database.User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Framework.Models.Database.ProductDetails", b =>
                {
                    b.HasOne("Framework.Models.Database.Product", "Product")
                        .WithOne("ProductDetails")
                        .HasForeignKey("Framework.Models.Database.ProductDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Framework.Models.Database.UserDetails", b =>
                {
                    b.HasOne("Framework.Models.Database.User", "User")
                        .WithOne("UserDetails")
                        .HasForeignKey("Framework.Models.Database.UserDetails", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
