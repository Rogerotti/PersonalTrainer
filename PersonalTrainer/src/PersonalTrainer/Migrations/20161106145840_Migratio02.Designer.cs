using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Framework.DataBaseContext;

namespace PersonalTrainer.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20161106145840_Migratio02")]
    partial class Migratio02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Framework.Models.Database.DayFoodDiary", b =>
                {
                    b.Property<Guid>("DayId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("TotalCalories");

                    b.Property<decimal>("TotalCarbohydrates");

                    b.Property<decimal>("TotalFat");

                    b.Property<decimal>("TotalFibre");

                    b.Property<decimal>("TotalProteins");

                    b.Property<Guid>("UserId");

                    b.HasKey("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("DayFoodDiary");
                });

            modelBuilder.Entity("Framework.Models.Database.DiaryProduct", b =>
                {
                    b.Property<Guid>("DayId");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("DiaryProductId");

                    b.Property<int>("MealType");

                    b.Property<int>("Quantity");

                    b.HasKey("DayId", "ProductId");

                    b.HasIndex("DayId");

                    b.HasIndex("ProductId");

                    b.ToTable("DiaryProduct");
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

            modelBuilder.Entity("Framework.Models.Database.DayFoodDiary", b =>
                {
                    b.HasOne("Framework.Models.Database.User", "User")
                        .WithMany("DayFoodDiary")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Framework.Models.Database.DiaryProduct", b =>
                {
                    b.HasOne("Framework.Models.Database.DayFoodDiary", "Day")
                        .WithMany("DiaryProducts")
                        .HasForeignKey("DayId");

                    b.HasOne("Framework.Models.Database.Product", "Product")
                        .WithMany("DiaryProducts")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Framework.Models.Database.Product", b =>
                {
                    b.HasOne("Framework.Models.Database.User")
                        .WithMany("Products")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Framework.Models.Database.ProductDetails", b =>
                {
                    b.HasOne("Framework.Models.Database.Product", "Product")
                        .WithOne("ProductDetails")
                        .HasForeignKey("Framework.Models.Database.ProductDetails", "ProductId");
                });

            modelBuilder.Entity("Framework.Models.Database.UserDetails", b =>
                {
                    b.HasOne("Framework.Models.Database.User", "User")
                        .WithOne("UserDetails")
                        .HasForeignKey("Framework.Models.Database.UserDetails", "UserId");
                });
        }
    }
}
