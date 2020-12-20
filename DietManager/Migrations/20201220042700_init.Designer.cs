﻿// <auto-generated />
using System;
using DietManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DietManager.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201220042700_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DietManager.Models.IngredientBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("REAL");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Fat")
                        .HasColumnType("REAL");

                    b.Property<float>("Kcal")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<float>("Protein")
                        .HasColumnType("REAL");

                    b.Property<float>("Saturated")
                        .HasColumnType("REAL");

                    b.Property<float>("Sugar")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IngredientBase");
                });

            modelBuilder.Entity("DietManager.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("DietManager.Models.Ingredient", b =>
                {
                    b.HasBaseType("DietManager.Models.IngredientBase");

                    b.Property<float>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int?>("MealId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("MealId");

                    b.ToTable("Ingredients");

                    b.HasDiscriminator().HasValue("Ingredient");
                });

            modelBuilder.Entity("DietManager.Models.Ingredient", b =>
                {
                    b.HasOne("DietManager.Models.Meal", "Meal")
                        .WithMany("Ingregients")
                        .HasForeignKey("MealId");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("DietManager.Models.Meal", b =>
                {
                    b.Navigation("Ingregients");
                });
#pragma warning restore 612, 618
        }
    }
}
