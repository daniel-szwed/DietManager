﻿// <auto-generated />
using System;
using ArbreSoft.DietManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArbreSoft.DietManager.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.NutritionFact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("KiloCalories")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Proteins")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SaturatedFats")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Sugars")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalCarbohydreates")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalFats")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NutritionFacts");

                    b.HasDiscriminator<string>("Type").HasValue("NutritionFact");
                });

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.Ingredient", b =>
                {
                    b.HasBaseType("ArbreSoft.DietManager.Domain.NutritionFact");

                    b.Property<Guid?>("NutritionFactId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasIndex("NutritionFactId");

                    b.HasDiscriminator().HasValue("Ingredient");
                });

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.Meal", b =>
                {
                    b.HasBaseType("ArbreSoft.DietManager.Domain.Ingredient");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Meal");
                });

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.DailyMenu", b =>
                {
                    b.HasBaseType("ArbreSoft.DietManager.Domain.Meal");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("DailyMenu");
                });

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.Diet", b =>
                {
                    b.HasBaseType("ArbreSoft.DietManager.Domain.DailyMenu");

                    b.HasDiscriminator().HasValue("Diet");
                });

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.Ingredient", b =>
                {
                    b.HasOne("ArbreSoft.DietManager.Domain.NutritionFact", null)
                        .WithMany("Childrens")
                        .HasForeignKey("NutritionFactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ArbreSoft.DietManager.Domain.NutritionFact", b =>
                {
                    b.Navigation("Childrens");
                });
#pragma warning restore 612, 618
        }
    }
}
