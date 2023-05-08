﻿// <auto-generated />
using System;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DayPlanner.Backend.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Boards", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.BoardMember", b =>
                {
                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("BoardId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("BoardMembers", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("CreatorId");

                    b.ToTable("TaskItems", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaltHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.Board", b =>
                {
                    b.HasOne("DayPlanner.Backend.DataAccess.Entities.User", "Creator")
                        .WithMany("Boards")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.BoardMember", b =>
                {
                    b.HasOne("DayPlanner.Backend.DataAccess.Entities.Board", "Board")
                        .WithMany("BoardMemberships")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DayPlanner.Backend.DataAccess.Entities.User", "Member")
                        .WithMany("Memberships")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.TaskItem", b =>
                {
                    b.HasOne("DayPlanner.Backend.DataAccess.Entities.Board", "Board")
                        .WithMany("Tasks")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DayPlanner.Backend.DataAccess.Entities.User", "Creator")
                        .WithMany("Tasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.User", b =>
                {
                    b.HasOne("DayPlanner.Backend.DataAccess.Entities.Board", null)
                        .WithMany("BoardMembers")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.Board", b =>
                {
                    b.Navigation("BoardMembers");

                    b.Navigation("BoardMemberships");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DayPlanner.Backend.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("Memberships");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
