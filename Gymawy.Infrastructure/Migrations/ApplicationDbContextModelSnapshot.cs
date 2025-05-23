﻿// <auto-generated />
using System;
using Gymawy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gymawy.Domain.Admins.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("StripeAccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Gymawy.Domain.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("DECIMAL(10,2)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("SessionId", "ParticipantId")
                        .IsUnique();

                    b.ToTable("bookings", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.GymTrainers.GymTrainer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GymId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.HasIndex("GymId", "TrainerId")
                        .IsUnique();

                    b.ToTable("gym_trainer", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Gyms.Gym", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxRooms")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("gyms", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Participants.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("participants", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Payments.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL(10,2)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("payments", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.RefreshTokens.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Gymawy.Domain.Rooms.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GymId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxDailySessions")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("rooms", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Sessions.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("SessionFee")
                        .HasColumnType("DECIMAL(10,2)");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("TrainerId");

                    b.ToTable("sessions", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("EndDate");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("StartDate");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("_maxGyms")
                        .HasColumnType("INT")
                        .HasColumnName("MaxGyms");

                    b.Property<decimal>("_price")
                        .HasColumnType("DECIMAL(10,2)")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.HasIndex("AdminId")
                        .IsUnique();

                    b.ToTable("subscriptions", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.TrainerInvitations.TrainerInvitaion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GymId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.HasIndex("TrainerId");

                    b.ToTable("TrainerInvitations", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Trainers.Trainer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("trainers", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailConfirmationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmailConfirmationCodeExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TrainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Gymawy.Domain.Admins.Admin", b =>
                {
                    b.HasOne("Gymawy.Domain.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("Gymawy.Domain.Admins.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gymawy.Domain.Bookings.Booking", b =>
                {
                    b.HasOne("Gymawy.Domain.Participants.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gymawy.Domain.Sessions.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Gymawy.Domain.GymTrainers.GymTrainer", b =>
                {
                    b.HasOne("Gymawy.Domain.Gyms.Gym", "Gym")
                        .WithMany("GymTrainers")
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gymawy.Domain.Trainers.Trainer", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gym");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Gymawy.Domain.Gyms.Gym", b =>
                {
                    b.HasOne("Gymawy.Domain.Subscriptions.Subscription", "Subscription")
                        .WithMany("Gyms")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Gymawy.Domain.Participants.Participant", b =>
                {
                    b.HasOne("Gymawy.Domain.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("Gymawy.Domain.Participants.Participant", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gymawy.Domain.Payments.Payment", b =>
                {
                    b.HasOne("Gymawy.Domain.Subscriptions.Subscription", "Subscription")
                        .WithMany("Payments")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Gymawy.Domain.RefreshTokens.RefreshToken", b =>
                {
                    b.HasOne("Gymawy.Domain.Users.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gymawy.Domain.Rooms.Room", b =>
                {
                    b.HasOne("Gymawy.Domain.Gyms.Gym", "Gym")
                        .WithMany("Rooms")
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gym");
                });

            modelBuilder.Entity("Gymawy.Domain.Sessions.Session", b =>
                {
                    b.HasOne("Gymawy.Domain.Rooms.Room", "Room")
                        .WithMany("Sessions")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gymawy.Domain.Trainers.Trainer", "Trainer")
                        .WithMany("Sessions")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Gymawy.Domain.Subscriptions.Subscription", b =>
                {
                    b.HasOne("Gymawy.Domain.Admins.Admin", "Admin")
                        .WithOne("Subscription")
                        .HasForeignKey("Gymawy.Domain.Subscriptions.Subscription", "AdminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Gymawy.Domain.TrainerInvitations.TrainerInvitaion", b =>
                {
                    b.HasOne("Gymawy.Domain.Gyms.Gym", "Gym")
                        .WithMany("TrainerInvitaions")
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gymawy.Domain.Trainers.Trainer", "Trainer")
                        .WithMany("TrainerInvitaions")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gym");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Gymawy.Domain.Trainers.Trainer", b =>
                {
                    b.HasOne("Gymawy.Domain.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("Gymawy.Domain.Trainers.Trainer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gymawy.Domain.Admins.Admin", b =>
                {
                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Gymawy.Domain.Gyms.Gym", b =>
                {
                    b.Navigation("GymTrainers");

                    b.Navigation("Rooms");

                    b.Navigation("TrainerInvitaions");
                });

            modelBuilder.Entity("Gymawy.Domain.Rooms.Room", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Gymawy.Domain.Subscriptions.Subscription", b =>
                {
                    b.Navigation("Gyms");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Gymawy.Domain.Trainers.Trainer", b =>
                {
                    b.Navigation("Sessions");

                    b.Navigation("TrainerInvitaions");
                });

            modelBuilder.Entity("Gymawy.Domain.Users.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
