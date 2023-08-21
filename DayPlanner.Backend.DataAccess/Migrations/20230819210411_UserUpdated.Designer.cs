﻿// <auto-generated />
using System;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DayPlanner.Backend.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230819210411_UserUpdated")]
    partial class UserUpdated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DayPlanner.Backend.Domain.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Boards", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.BoardMember", b =>
                {
                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("BoardId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("BoardMembers", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.RecurringPattern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OccurencesNumber")
                        .HasColumnType("int");

                    b.Property<int>("RecurringType")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RecurringPatterns", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOverdue")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecurring")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentTaskId")
                        .HasColumnType("int");

                    b.Property<int?>("PerformerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PerformerId");

                    b.ToTable("TaskItems", (string)null);
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.User", b =>
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

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ResetPasswrodTokenExpiresAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("SaltHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("VerifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Email = "Dioneli@mail.ru1",
                            FirstName = "Madison",
                            LastName = "Walker",
                            PasswordHash = "x/5fpi8JiMGXxM4Re4fzlamU61mQQMGNR50wxtwCaHw=",
                            SaltHash = "mlJyHV/cYHAT2ErFkB8d5w==",
                            VerificationToken = "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                            VerifiedAt = new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Email = "D1!q2222@ru",
                            FirstName = "Sam",
                            LastName = "McGregor",
                            PasswordHash = "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=",
                            SaltHash = "FyQp6hr65+F7jI0btRXMLw==",
                            VerificationToken = "OCGOOxNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                            VerifiedAt = new DateTimeOffset(new DateTime(2020, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.Board", b =>
                {
                    b.HasOne("DayPlanner.Backend.Domain.User", "Creator")
                        .WithMany("Boards")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.BoardMember", b =>
                {
                    b.HasOne("DayPlanner.Backend.Domain.Board", "Board")
                        .WithMany("BoardMemberships")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DayPlanner.Backend.Domain.User", "Member")
                        .WithMany("Memberships")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.Notification", b =>
                {
                    b.HasOne("DayPlanner.Backend.Domain.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.TaskItem", b =>
                {
                    b.HasOne("DayPlanner.Backend.Domain.Board", "Board")
                        .WithMany("Tasks")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DayPlanner.Backend.Domain.User", "Creator")
                        .WithMany("Tasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DayPlanner.Backend.Domain.User", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId");

                    b.Navigation("Board");

                    b.Navigation("Creator");

                    b.Navigation("Performer");
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.User", b =>
                {
                    b.HasOne("DayPlanner.Backend.Domain.Board", null)
                        .WithMany("BoardMembers")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.Board", b =>
                {
                    b.Navigation("BoardMembers");

                    b.Navigation("BoardMemberships");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DayPlanner.Backend.Domain.User", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("Memberships");

                    b.Navigation("Notifications");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}