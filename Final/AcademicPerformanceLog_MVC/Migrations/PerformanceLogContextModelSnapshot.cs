﻿// <auto-generated />
using AcademicPerformanceLog_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcademicPerformanceLog_MVC.Migrations
{
    [DbContext(typeof(PerformanceLogContext))]
    partial class PerformanceLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademicPerformanceLog_MVC.Models.Discipline", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("AcademicPerformanceLog_MVC.Models.Performance", b =>
                {
                    b.Property<int>("DisciplineID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.HasKey("DisciplineID", "StudentID");

                    b.HasIndex("StudentID");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("AcademicPerformanceLog_MVC.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Score")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AcademicPerformanceLog_MVC.Models.Performance", b =>
                {
                    b.HasOne("AcademicPerformanceLog_MVC.Models.Discipline", null)
                        .WithMany("StudentsDisciplines")
                        .HasForeignKey("DisciplineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicPerformanceLog_MVC.Models.Student", null)
                        .WithMany("StudentsDisciplines")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AcademicPerformanceLog_MVC.Models.Discipline", b =>
                {
                    b.Navigation("StudentsDisciplines");
                });

            modelBuilder.Entity("AcademicPerformanceLog_MVC.Models.Student", b =>
                {
                    b.Navigation("StudentsDisciplines");
                });
#pragma warning restore 612, 618
        }
    }
}