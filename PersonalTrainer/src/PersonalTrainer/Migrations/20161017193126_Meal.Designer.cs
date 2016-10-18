using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Framework.DataBaseContext;

namespace PersonalTrainer.Migrations
{
    [DbContext(typeof(MealContext))]
    [Migration("20161017193126_Meal")]
    partial class Meal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Framework.Models.Database.DailyFood", b =>
                {
                    b.Property<Guid>("DayId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("TotalCalories");

                    b.Property<int>("TotalCarbohydrates");

                    b.Property<int>("TotalFat");

                    b.Property<int>("TotalFibre");

                    b.Property<int>("TotalProteins");

                    b.Property<Guid>("UserId");

                    b.HasKey("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("DailyFood");
                });

            modelBuilder.Entity("Framework.Models.Database.Meal", b =>
                {
                    b.Property<Guid>("MealId");

                    b.Property<Guid?>("DailyFoodDayId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("MealType");

                    b.Property<Guid>("UserId");

                    b.HasKey("MealId");

                    b.HasIndex("DailyFoodDayId");

                    b.HasIndex("UserId");

                    b.ToTable("Meal");
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

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Framework.Models.Database.ProductDetails", b =>
                {
                    b.Property<Guid>("ProductId");

                    b.Property<int>("Calories");

                    b.Property<int>("Carbohydrates");

                    b.Property<int>("Fat");

                    b.Property<int>("Fibre");

                    b.Property<int>("Protein");

                    b.Property<int>("Quantity");

                    b.Property<int>("QuantityType");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Framework.Models.Database.ProductMeal", b =>
                {
                    b.Property<Guid>("MealId");

                    b.Property<Guid>("ProductId");

                    b.HasKey("MealId", "ProductId");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductMeal");
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
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Framework.Models.Database.Meal", b =>
                {
                    b.HasOne("Framework.Models.Database.DailyFood")
                        .WithMany("Meals")
                        .HasForeignKey("DailyFoodDayId");

                    b.HasOne("Framework.Models.Database.User", "User")
                        .WithMany("Meals")
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

            modelBuilder.Entity("Framework.Models.Database.ProductMeal", b =>
                {
                    b.HasOne("Framework.Models.Database.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Framework.Models.Database.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
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
