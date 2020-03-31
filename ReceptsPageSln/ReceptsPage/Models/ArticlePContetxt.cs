using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReceptsPage.ModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    public class ArticlePContetxt : IdentityDbContext<AppUser,AppRole,int>
    {


        public ArticlePContetxt(DbContextOptions<ArticlePContetxt> options) : base(options) { }

        public DbSet<ArticleP> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<BarCategory> BarCategories { get; set; }
        public DbSet<BarArticleP> BarArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ArticleP>().Property(n => n.Title).IsRequired(true);
            modelBuilder.Entity<Category>().HasData(
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
                }

                );
           
            modelBuilder.Entity<SubCategory>().HasData(
               new { SubCategoryId = 1, Name = "Հայկական խոհանոց", CategoryId = 1 },
               new { SubCategoryId = 2, Name = "Վրացական խոհանոց", CategoryId = 1 },
               new { SubCategoryId = 3, Name = "Իտալական խոհանոց", CategoryId = 1 },
               new { SubCategoryId = 4, Name = "Ֆրանսիական խոհանոց", CategoryId = 1 },
               new { SubCategoryId = 5, Name = "Արևելյան խոհանոց", CategoryId = 1 },
               new { SubCategoryId = 6, Name = "Չինական խոհանոց", CategoryId = 1 },
               new { SubCategoryId = 7, Name = "Մեքսիկական խոհանոց", CategoryId = 1 },

               new { SubCategoryId = 8, Name = "Ամանորյա", CategoryId = 2 },
               new { SubCategoryId = 9, Name = "Զատկի ուտեստներ", CategoryId = 2 },
               new { SubCategoryId = 10, Name = "Ծննդյան", CategoryId = 2 },
               new { SubCategoryId = 11, Name = "Պասի ուտեստներ", CategoryId = 2 },

               new { SubCategoryId = 12, Name = "Առաջին ուտեստ", CategoryId = 3 },
               new { SubCategoryId = 13, Name = "Տաք ճաշ", CategoryId = 3 },
               new { SubCategoryId = 14, Name = "Աղանդեր", CategoryId = 3 },
               new { SubCategoryId = 15, Name = "Մանկական կերակուր", CategoryId = 3 },
               new { SubCategoryId = 16, Name = "Նախաճաշ", CategoryId = 3 },
               new { SubCategoryId = 17, Name = "Այլ", CategoryId = 4 }
            );

            modelBuilder.Entity<BarCategory>().HasData(
              new { BarCategoryId = 1, Name = "Ինչ եփել այսօր", },
              new { BarCategoryId = 2, Name = "Տորթեր", },
              new { BarCategoryId = 3, Name = "Թխվածքաբլիթներ", },
              new { BarCategoryId = 4, Name = "Պիցաներ", },
              new { BarCategoryId = 5, Name = "Աղցաններ", },
              new { BarCategoryId = 6, Name = "Դիետաներ", },
              new { BarCategoryId = 7, Name = "Առողջ սնունդ", },
              new { BarCategoryId = 8, Name = "Օգտայկար Խորհուրդներ", },
              new { BarCategoryId = 9, Name = "Նկարներ", },
              new { BarCategoryId = 10, Name = "Վիդեոներ", },
              new { BarCategoryId = 11, Name = "Ռեստորաններ", },
              new { BarCategoryId = 12, Name = "Այլ", }
            );

        }
    }
}


