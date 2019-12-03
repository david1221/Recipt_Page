﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReceptsPage.Models;

namespace ReceptsPage.Migrations
{
    [DbContext(typeof(ArticlePContetxt))]
    [Migration("20191128170349__Init_V3")]
    partial class _Init_V3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ReceptsPage.Models.ArticleP", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateAdded");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IfFavorite");

                    b.Property<byte[]>("ImgGeneral");

                    b.Property<int?>("SubCategoryId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ArticleId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ReceptsPage.Models.BarArticleP", b =>
                {
                    b.Property<int>("BarArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BarCategoryId");

                    b.Property<DateTime?>("DateAdded");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<byte[]>("ImgGeneral");

                    b.Property<string>("Star");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BarArticleId");

                    b.HasIndex("BarCategoryId");

                    b.ToTable("BarArticles");
                });

            modelBuilder.Entity("ReceptsPage.Models.BarCategory", b =>
                {
                    b.Property<int>("BarCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("BarCategoryId");

                    b.ToTable("BarCategories");

                    b.HasData(
                        new
                        {
                            BarCategoryId = 1,
                            Name = "Ինչ եփել այսօր"
                        },
                        new
                        {
                            BarCategoryId = 2,
                            Name = "Տորթեր"
                        },
                        new
                        {
                            BarCategoryId = 3,
                            Name = "Թխվածքաբլիթներ"
                        },
                        new
                        {
                            BarCategoryId = 4,
                            Name = "Պիցաներ"
                        },
                        new
                        {
                            BarCategoryId = 5,
                            Name = "Աղցաններ"
                        },
                        new
                        {
                            BarCategoryId = 6,
                            Name = "Դիետաներ"
                        },
                        new
                        {
                            BarCategoryId = 7,
                            Name = "Առողջ սնունդ"
                        },
                        new
                        {
                            BarCategoryId = 8,
                            Name = "Օգտայկար Խորհուրդներ"
                        },
                        new
                        {
                            BarCategoryId = 9,
                            Name = "Նկարներ"
                        },
                        new
                        {
                            BarCategoryId = 10,
                            Name = "Վիդեոներ"
                        },
                        new
                        {
                            BarCategoryId = 11,
                            Name = "Ռեստորաններ"
                        });
                });

            modelBuilder.Entity("ReceptsPage.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Ազգային"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Տոնական"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Ամենօրյա"
                        });
                });

            modelBuilder.Entity("ReceptsPage.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");

                    b.HasData(
                        new
                        {
                            SubCategoryId = 1,
                            CategoryId = 1,
                            Name = "Հայկական խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 2,
                            CategoryId = 1,
                            Name = "Վրացական խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 3,
                            CategoryId = 1,
                            Name = "Իտալական խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 4,
                            CategoryId = 1,
                            Name = "Ֆրանսիական խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 5,
                            CategoryId = 1,
                            Name = "Արևելյան խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 6,
                            CategoryId = 1,
                            Name = "Չինական խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 7,
                            CategoryId = 1,
                            Name = "Մեքսիկական խոհանոց"
                        },
                        new
                        {
                            SubCategoryId = 8,
                            CategoryId = 2,
                            Name = "Ամանորյա"
                        },
                        new
                        {
                            SubCategoryId = 9,
                            CategoryId = 2,
                            Name = "Զատկի ուտեստներ"
                        },
                        new
                        {
                            SubCategoryId = 10,
                            CategoryId = 2,
                            Name = "Ծննդյան"
                        },
                        new
                        {
                            SubCategoryId = 11,
                            CategoryId = 2,
                            Name = "Պասի ուտեստներ"
                        },
                        new
                        {
                            SubCategoryId = 12,
                            CategoryId = 3,
                            Name = "Առաջին ուտեստ"
                        },
                        new
                        {
                            SubCategoryId = 13,
                            CategoryId = 3,
                            Name = "Տաք ճաշ"
                        },
                        new
                        {
                            SubCategoryId = 14,
                            CategoryId = 3,
                            Name = "Աղանդեր"
                        },
                        new
                        {
                            SubCategoryId = 15,
                            CategoryId = 3,
                            Name = "Մանկական կերակուր"
                        },
                        new
                        {
                            SubCategoryId = 16,
                            CategoryId = 3,
                            Name = "Նախաճաշ"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReceptsPage.Models.ArticleP", b =>
                {
                    b.HasOne("ReceptsPage.Models.SubCategory", "SubCategory")
                        .WithMany("Articles")
                        .HasForeignKey("SubCategoryId");
                });

            modelBuilder.Entity("ReceptsPage.Models.BarArticleP", b =>
                {
                    b.HasOne("ReceptsPage.Models.BarCategory", "BarCategory")
                        .WithMany("BarArticles")
                        .HasForeignKey("BarCategoryId");
                });

            modelBuilder.Entity("ReceptsPage.Models.SubCategory", b =>
                {
                    b.HasOne("ReceptsPage.Models.Category", "Category")
                        .WithMany("SubCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}