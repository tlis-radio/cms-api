﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tlis.Cms.Infrastructure.Persistence;

#nullable disable

namespace Tlis.Cms.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CmsDbContext))]
    [Migration("20241012091307_ChangeImageUrlToFileName")]
    partial class ChangeImageUrlToFileName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cms")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Broadcast", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<string>("ExternalUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("external_url");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("image_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("ShowId")
                        .HasColumnType("uuid")
                        .HasColumnName("show_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id")
                        .HasName("pk_broadcast");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasDatabaseName("ix_broadcast_image_id");

                    b.HasIndex("ShowId")
                        .HasDatabaseName("ix_broadcast_show_id");

                    b.ToTable("broadcast", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Images.Crop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("image_id");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<int>("Width")
                        .HasColumnType("integer")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_crop");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_crop_image_id");

                    b.ToTable("crop", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Images.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<int>("Width")
                        .HasColumnType("integer")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_image");

                    b.ToTable("image", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.JoinTables.ShowsUsers", b =>
                {
                    b.Property<Guid>("ShowId")
                        .HasColumnType("uuid")
                        .HasColumnName("show_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("ShowId", "UserId")
                        .HasName("pk_shows_users");

                    b.HasIndex("ShowId")
                        .HasDatabaseName("ix_shows_users_show_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_shows_users_user_id");

                    b.ToTable("shows_users", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_membership");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_membership_id");

                    b.ToTable("membership", "cms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80126b05-9dab-4709-aa6a-39baa5bafe79"),
                            Status = "Active"
                        },
                        new
                        {
                            Id = new Guid("a7c0bea2-2812-40b6-9836-d4b5accae95a"),
                            Status = "Archive"
                        },
                        new
                        {
                            Id = new Guid("cfaeecff-d26b-44f2-bfa1-c80ab79983a9"),
                            Status = "Postponed"
                        });
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("external_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_roles_id");

                    b.ToTable("roles", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Show", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("ProfileImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("profile_image_id");

                    b.HasKey("Id")
                        .HasName("pk_show");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_show_name");

                    b.HasIndex("ProfileImageId")
                        .IsUnique()
                        .HasDatabaseName("ix_show_profile_image_id");

                    b.ToTable("show", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Abouth")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("abouth");

                    b.Property<bool>("CmsAdminAccess")
                        .HasColumnType("boolean")
                        .HasColumnName("cms_admin_access");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text")
                        .HasColumnName("external_id");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastname");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<bool>("PreferNicknameOverName")
                        .HasColumnType("boolean")
                        .HasColumnName("prefer_nickname_over_name");

                    b.Property<Guid?>("ProfileImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("profile_image_id");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_user_id");

                    b.HasIndex("ProfileImageId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_profile_image_id");

                    b.HasIndex("Firstname", "Lastname", "Nickname")
                        .IsUnique()
                        .HasDatabaseName("ix_user_firstname_lastname_nickname");

                    b.ToTable("user", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.UserMembershipHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("change_date");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("MembershipId")
                        .HasColumnType("uuid")
                        .HasColumnName("membership_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_membership_history");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_user_membership_history_id");

                    b.HasIndex("MembershipId")
                        .HasDatabaseName("ix_user_membership_history_membership_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_membership_history_user_id");

                    b.ToTable("user_membership_history", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.UserRoleHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("FunctionEndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("function_end_date");

                    b.Property<DateTime>("FunctionStartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("function_start_date");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_role_history");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_user_role_history_id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_history_role_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_role_history_user_id");

                    b.ToTable("user_role_history", "cms");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Broadcast", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Images.Image", "Image")
                        .WithOne()
                        .HasForeignKey("Tlis.Cms.Domain.Entities.Broadcast", "ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_broadcast_image_image_id");

                    b.HasOne("Tlis.Cms.Domain.Entities.Show", "Show")
                        .WithMany("Broadcasts")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_broadcast_show_show_id");

                    b.Navigation("Image");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Images.Crop", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Images.Image", null)
                        .WithMany("Crops")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_crop_image_image_id");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.JoinTables.ShowsUsers", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Show", null)
                        .WithMany("ShowsUsers")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_shows_users_show_show_id");

                    b.HasOne("Tlis.Cms.Domain.Entities.User", null)
                        .WithMany("ShowsUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_shows_users_user_user_id");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Show", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Images.Image", "ProfileImage")
                        .WithOne()
                        .HasForeignKey("Tlis.Cms.Domain.Entities.Show", "ProfileImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_show_image_profile_image_id");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.User", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Images.Image", "ProfileImage")
                        .WithOne()
                        .HasForeignKey("Tlis.Cms.Domain.Entities.User", "ProfileImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_user_image_profile_image_id");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.UserMembershipHistory", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Membership", "Membership")
                        .WithMany()
                        .HasForeignKey("MembershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_membership_history_membership_membership_id");

                    b.HasOne("Tlis.Cms.Domain.Entities.User", null)
                        .WithMany("MembershipHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_membership_history_user_user_id");

                    b.Navigation("Membership");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.UserRoleHistory", b =>
                {
                    b.HasOne("Tlis.Cms.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_history_roles_role_id");

                    b.HasOne("Tlis.Cms.Domain.Entities.User", "User")
                        .WithMany("RoleHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_history_user_user_id");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Images.Image", b =>
                {
                    b.Navigation("Crops");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.Show", b =>
                {
                    b.Navigation("Broadcasts");

                    b.Navigation("ShowsUsers");
                });

            modelBuilder.Entity("Tlis.Cms.Domain.Entities.User", b =>
                {
                    b.Navigation("MembershipHistory");

                    b.Navigation("RoleHistory");

                    b.Navigation("ShowsUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
