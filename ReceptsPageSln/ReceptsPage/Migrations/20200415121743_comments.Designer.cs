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
    [Migration("20200415121743_comments")]
    partial class comments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ReceptsPage.ModelIdentity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("ReceptsPage.ModelIdentity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("Birthdate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<byte[]>("PhotoUser");

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

            modelBuilder.Entity("ReceptsPage.Models.ArticleP", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AdminConfirm");

                    b.Property<int?>("AppUserId");

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

                    b.HasIndex("AppUserId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ReceptsPage.Models.BarArticleP", b =>
                {
                    b.Property<int>("BarArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppUserId");

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

                    b.HasIndex("AppUserId");

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
                        },
                        new
                        {
                            BarCategoryId = 12,
                            Name = "Այլ"
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
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Ընդհանուր"
                        });
                });

            modelBuilder.Entity("ReceptsPage.Models.Comments.ChildComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Text");

                    b.Property<int?>("appUserId");

                    b.Property<int?>("mainCommentId");

                    b.HasKey("Id");

                    b.HasIndex("appUserId");

                    b.HasIndex("mainCommentId");

                    b.ToTable("ChildComments");
                });

            modelBuilder.Entity("ReceptsPage.Models.Comments.MainComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Text");

                    b.Property<int?>("appUserId");

                    b.Property<int?>("articlePArticleId");

                    b.HasKey("Id");

                    b.HasIndex("appUserId");

                    b.HasIndex("articlePArticleId");

                    b.ToTable("MainComment");
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
                        },
                        new
                        {
                            SubCategoryId = 17,
                            CategoryId = 4,
                            Name = "Այլ"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReceptsPage.ModelIdentity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReceptsPage.Models.ArticleP", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser", "AppUser")
                        .WithMany("Articles")
                        .HasForeignKey("AppUserId");

                    b.HasOne("ReceptsPage.Models.SubCategory", "SubCategory")
                        .WithMany("Articles")
                        .HasForeignKey("SubCategoryId");
                });

            modelBuilder.Entity("ReceptsPage.Models.BarArticleP", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser")
                        .WithMany("BarArticles")
                        .HasForeignKey("AppUserId");

                    b.HasOne("ReceptsPage.Models.BarCategory", "BarCategory")
                        .WithMany("BarArticles")
                        .HasForeignKey("BarCategoryId");
                });

            modelBuilder.Entity("ReceptsPage.Models.Comments.ChildComment", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser", "appUser")
                        .WithMany("ChildComments")
                        .HasForeignKey("appUserId");

                    b.HasOne("ReceptsPage.Models.Comments.MainComment", "mainComment")
                        .WithMany("childComments")
                        .HasForeignKey("mainCommentId");
                });

            modelBuilder.Entity("ReceptsPage.Models.Comments.MainComment", b =>
                {
                    b.HasOne("ReceptsPage.ModelIdentity.AppUser", "appUser")
                        .WithMany("MainMomments")
                        .HasForeignKey("appUserId");

                    b.HasOne("ReceptsPage.Models.ArticleP", "articleP")
                        .WithMany()
                        .HasForeignKey("articlePArticleId");
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