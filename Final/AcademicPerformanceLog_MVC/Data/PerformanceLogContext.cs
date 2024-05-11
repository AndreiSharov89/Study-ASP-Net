using AcademicPerformanceLog_MVC.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Cryptography;

namespace AcademicPerformanceLog_MVC.Data
{
    public class PerformanceLogContext : DbContext
    {
        public PerformanceLogContext(DbContextOptions<PerformanceLogContext> options) : base(options)
        { }

        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Performance> Performances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Disciplines)
                .WithMany(e => e.Students)
                .UsingEntity<Performance>();
        }

    }
}
