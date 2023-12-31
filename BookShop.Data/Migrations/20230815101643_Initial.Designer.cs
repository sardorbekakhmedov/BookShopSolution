﻿// <auto-generated />
using System;
using BookShop.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookShop.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230815101643_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookShop.Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uuid")
                        .HasColumnName("genre_id");

                    b.Property<int>("Isbn")
                        .HasColumnType("integer")
                        .HasColumnName("isbn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uuid")
                        .HasColumnName("publisher_id");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("GenreId")
                        .HasDatabaseName("ix_books_genre_id");

                    b.HasIndex("PublisherId")
                        .HasDatabaseName("ix_books_publisher_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_books_user_id");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<float>("Percentage")
                        .HasColumnType("real")
                        .HasColumnName("percentage");

                    b.Property<Guid?>("PublisherId")
                        .HasColumnType("uuid")
                        .HasColumnName("publisher_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_discounts");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_discounts_book_id");

                    b.ToTable("discounts", (string)null);
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("genre_name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_genres");

                    b.ToTable("genres", (string)null);
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_path");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_images_book_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_images_user_id");

                    b.ToTable("images", (string)null);
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("LogoPath")
                        .HasColumnType("text")
                        .HasColumnName("logo_path");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_publisher");

                    b.ToTable("publisher", (string)null);
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("82dde2f6-9825-4ddd-b228-bada1e5b02fe"),
                            CreatedDate = new DateTime(2023, 8, 15, 10, 16, 43, 190, DateTimeKind.Utc).AddTicks(7014),
                            Name = "SuperAdmin"
                        },
                        new
                        {
                            Id = new Guid("5555d14e-e5b3-4ca4-898f-0c5eba3f4912"),
                            CreatedDate = new DateTime(2023, 8, 15, 10, 16, 43, 190, DateTimeKind.Utc).AddTicks(7111),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("72e852f6-fe9a-437e-b402-57dd7692d8c1"),
                            CreatedDate = new DateTime(2023, 8, 15, 10, 16, 43, 190, DateTimeKind.Utc).AddTicks(7115),
                            Name = "Manager"
                        },
                        new
                        {
                            Id = new Guid("796eaa59-4026-476c-8b12-7c6b600b6caf"),
                            CreatedDate = new DateTime(2023, 8, 15, 10, 16, 43, 190, DateTimeKind.Utc).AddTicks(7118),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("BookShop.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Firstname")
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2824d5ff-26f4-4145-80d0-3f128c959b3d"),
                            CreatedDate = new DateTime(2023, 8, 15, 10, 16, 43, 190, DateTimeKind.Utc).AddTicks(7823),
                            Firstname = "Admin",
                            PasswordHash = "zxcvasdfqwer1234",
                            Username = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid")
                        .HasColumnName("roles_id");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId")
                        .HasName("pk_role_user");

                    b.HasIndex("UsersId")
                        .HasDatabaseName("ix_role_user_users_id");

                    b.ToTable("role_user", (string)null);
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Book", b =>
                {
                    b.HasOne("BookShop.Domain.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_genres_genre_id");

                    b.HasOne("BookShop.Domain.Entities.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_publisher_publisher_id");

                    b.HasOne("BookShop.Domain.Entities.User", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_users_user_id");

                    b.Navigation("Genre");

                    b.Navigation("Publisher");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Discount", b =>
                {
                    b.HasOne("BookShop.Domain.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_discounts_books_book_id");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Image", b =>
                {
                    b.HasOne("BookShop.Domain.Entities.Book", null)
                        .WithMany("BookImagePaths")
                        .HasForeignKey("BookId")
                        .HasConstraintName("fk_images_books_book_id");

                    b.HasOne("BookShop.Domain.Entities.User", null)
                        .WithMany("UserImages")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_images_users_user_id");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("BookShop.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_user_roles_roles_id");

                    b.HasOne("BookShop.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_user_users_users_id");
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Book", b =>
                {
                    b.Navigation("BookImagePaths");
                });

            modelBuilder.Entity("BookShop.Domain.Entities.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookShop.Domain.Entities.User", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("UserImages");
                });
#pragma warning restore 612, 618
        }
    }
}
