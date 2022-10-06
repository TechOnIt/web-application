﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iot.Infrastructure.Persistence.Context.Identity;

#nullable disable

namespace iot.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityContext))]
    partial class IdentityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("iot.Domain.Entities.Identity.LoginHistory", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginHistories");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ConfirmedEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("ConfirmedPhoneNumber")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBaned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LockOutDateTime")
                        .HasColumnType("datetime2");

                    b.Property<short>("MaxFailCount")
                        .HasColumnType("smallint");

                    b.Property<int>("OtpCode")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("DeviceType")
                        .HasColumnType("int");

                    b.Property<bool>("IsHigh")
                        .HasColumnType("bit");

                    b.Property<int>("Pin")
                        .HasColumnType("int");

                    b.Property<Guid>("PlaceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.SensorAggregate.PerformanceReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RecordDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SensorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("PerformanceReports");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.SensorAggregate.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SensorType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.StructureAggregate.Place", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StuctureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StuctureId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.StructureAggregate.Structure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Structures");
                });

            modelBuilder.Entity("iot.Domain.Entities.Secyrity.AesKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AesKeys");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dbc38109-1bc0-4995-8ca2-66b038747326"),
                            Key = "rdGKbxanYm933qo81LRtE7xRyz6A2m/5ub0j42y9sFA=",
                            Title = "DeviceKey"
                        },
                        new
                        {
                            Id = new Guid("10297d17-b232-441f-86f6-53454c464d75"),
                            Key = "mqSsdge5MVYeo0qZGhVlKB71gFQjGAVwyQwogq9VHkA=",
                            Title = "UserKey"
                        },
                        new
                        {
                            Id = new Guid("781c3310-c550-4baa-9cba-2bb9b4d4d622"),
                            Key = "h9Nw0cuUsJRKQpc5YPFK/5zB8f9lwpanvofle1w/iFE=",
                            Title = "SesnsorKey"
                        },
                        new
                        {
                            Id = new Guid("c3a8f274-3ebe-4374-81a3-9a108a67dd79"),
                            Key = "HSQg0lPLwR+/zNpb6tjJYh7xEuTl4X6dLqo8ddzZekM=",
                            Title = "ReportKey"
                        });
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.LoginHistory", b =>
                {
                    b.HasOne("iot.Domain.Entities.Identity.User", "User")
                        .WithMany("LoginHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("iot.Domain.ValueObjects.IPv4", "Ip", b1 =>
                        {
                            b1.Property<Guid>("LoginHistoryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte>("FirstOct")
                                .HasColumnType("tinyint");

                            b1.Property<byte>("FourthOct")
                                .HasColumnType("tinyint");

                            b1.Property<byte>("SecondOct")
                                .HasColumnType("tinyint");

                            b1.Property<byte>("ThirdOct")
                                .HasColumnType("tinyint");

                            b1.HasKey("LoginHistoryId");

                            b1.ToTable("LoginHistories");

                            b1.WithOwner()
                                .HasForeignKey("LoginHistoryId");
                        });

                    b.Navigation("Ip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.User", b =>
                {
                    b.OwnsOne("iot.Domain.ValueObjects.Concurrency", "ConcurrencyStamp", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("iot.Domain.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Surname")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("iot.Domain.ValueObjects.PasswordHash", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("ConcurrencyStamp");

                    b.Navigation("FullName");

                    b.Navigation("Password");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("iot.Domain.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iot.Domain.Entities.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.Device", b =>
                {
                    b.HasOne("iot.Domain.Entities.Product.StructureAggregate.Place", "Place")
                        .WithMany("Devices")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.SensorAggregate.PerformanceReport", b =>
                {
                    b.HasOne("iot.Domain.Entities.Product.SensorAggregate.Sensor", "Sensor")
                        .WithMany("Reports")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.StructureAggregate.Place", b =>
                {
                    b.HasOne("iot.Domain.Entities.Product.StructureAggregate.Structure", "Structure")
                        .WithMany("Places")
                        .HasForeignKey("StuctureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Structure");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.StructureAggregate.Structure", b =>
                {
                    b.HasOne("iot.Domain.Entities.Identity.User", null)
                        .WithMany("Structures")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("iot.Domain.ValueObjects.Concurrency", "ApiKey", b1 =>
                        {
                            b1.Property<Guid>("StructureId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("StructureId");

                            b1.ToTable("Structures");

                            b1.WithOwner()
                                .HasForeignKey("StructureId");
                        });

                    b.Navigation("ApiKey");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("iot.Domain.Entities.Identity.User", b =>
                {
                    b.Navigation("LoginHistories");

                    b.Navigation("Structures");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.SensorAggregate.Sensor", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.StructureAggregate.Place", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("iot.Domain.Entities.Product.StructureAggregate.Structure", b =>
                {
                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
