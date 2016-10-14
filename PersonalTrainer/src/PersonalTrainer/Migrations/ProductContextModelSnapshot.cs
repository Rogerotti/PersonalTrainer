using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Framework.DataBaseContext;

namespace PersonalTrainer.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Framework.Models.Database.ProductDetails", b =>
                {
                    b.HasOne("Framework.Models.Database.Product", "Product")
                        .WithOne("ProductDetails")
                        .HasForeignKey("Framework.Models.Database.ProductDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
