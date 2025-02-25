using ClassLibrary1.Models;
using Database.RepositoryObjects;
using FitnessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Contexts
{

    /// <summary>
    /// База данных еды приёмов пищи и всего вместе.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealFood> MealFoods { get; set; }
        public DbSet<User> Users { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //нужно сделать миграции.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealFood>()
           .HasKey(mf => new { mf.MealId, mf.FoodId }); // Составной ключ

            //  один ко многим между User и Meal
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(m => m.UserId);

            // один ко многим между Meal и MealFood
            modelBuilder.Entity<MealFood>()
                .HasOne(mf => mf.Meal)
                .WithMany(m => m.MealFoods)
                .HasForeignKey(mf => mf.MealId);

            // многие ко многим между MealFood и Food 
            modelBuilder.Entity<MealFood>()
                .HasOne(mf => mf.Food)
                .WithMany(f => f.MealFoods)
                .HasForeignKey(mf => mf.FoodId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
