using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.Repozitories;

namespace ReceptsPage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt=>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ArticlePContetxt>().AddDefaultTokenProviders();  
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ArticlePContetxt>(options =>
            {
                //"Data Source=(localdb)/MSSQLLocalDB; Database=Articles; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"
                // "data source=DESKTOP-16VRELJ/SQLEXPRESS;initial catalog=Articles;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;"
                options.UseSqlServer(Configuration.GetConnectionString("MyConnection"));
            });
            


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IGetArticles, ArticlesRepozitory>();
            services.AddScoped<IBarArticles, BarArticlesRepozitory>(); 
            services.AddScoped<IArticleAndBar, ArticleAndBarRepozitory>();
            services.AddScoped<IGetComments, CommentRepozitory>();


            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "267845699502-93k5p9c36ghv8v001mv6j8djmuvl0vg0.apps.googleusercontent.com";
                options.ClientSecret = "FVPqqBgjmU_2LNemUbY8MJUZ";

            }).AddFacebook(options=> {
                options.AppId = "3237336539652232";
                options.AppSecret = "1eb970fe446f6b4c303bfc5ca5996ca0";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Articles}/{action=Index}/{id?}");
                });
            
          
        }
      
        }
    }
