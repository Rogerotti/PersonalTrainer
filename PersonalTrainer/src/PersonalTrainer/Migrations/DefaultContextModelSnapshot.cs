﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Framework.DataBaseContext;

namespace PersonalTrainer.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Framework.Models.Database.DayFoodDiary", b =>
                {
                    b.Property<Guid>("DayId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("TotalCalories");

                    b.Property<decimal>("TotalCarbohydrates");

                    b.Property<decimal>("TotalFat");

                    b.Property<decimal>("TotalProteins");

                    b.Property<Guid>("UserId");

                    b.HasKey("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("DayFoodDiary");
                });

            modelBuilder.Entity("Framework.Models.Database.DiaryProduct", b =>
                {
                    b.Property<Guid>("DiaryProductId");

                    b.Property<Guid>("DayId");

                    b.Property<int>("MealType");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("DiaryProductId");

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

                    b.Property<decimal>("Protein");

                    b.Property<int>("Quantity");

                    b.Property<int>("QuantityType");

                    b.HasKey("ProductId");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Framework.Models.Database.User", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<bool>("Administrator");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("HashCode")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<int>("UserState");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Framework.Models.Database.UserDetails", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<int>("Age");

                    b.Property<int>("Gender");

                    b.Property<decimal>("Height");

                    b.Property<decimal>("Weight");

                    b.HasKey("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Framework.Models.Database.UserGoal", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<int>("Calories");

                    b.Property<int>("Carbohydrates");

                    b.Property<int>("Fat");

                    b.Property<int>("PercentageCarbs");

                    b.Property<int>("PercentageFat");

                    b.Property<int>("PercentageProtein");

                    b.Property<int>("Proteins");

                    b.HasKey("UserId");

                    b.ToTable("UserGoal");
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

            modelBuilder.Entity("Framework.Models.Database.UserGoal", b =>
                {
                    b.HasOne("Framework.Models.Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
