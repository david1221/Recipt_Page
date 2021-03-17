using Microsoft.EntityFrameworkCore;
using ReceptsPage.Interfaces;
using ReceptsPage.Models;
using ReceptsPage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Repozitories
{
    public class ArticleAndBarRepozitory : IArticleAndBar
    {

        private readonly ArticlePContetxt _articlePContetxt;
        public ArticleAndBarRepozitory(ArticlePContetxt articlePContetxt)
        {
            _articlePContetxt = articlePContetxt;
        }
        public async Task<IList<ArticleP>> ArticleImage()
        {
            return await _articlePContetxt.Articles
            .Where(x => x.ImgGeneral != null)

            .OrderBy(x => x.DateAdded)
            .ToListAsync();


        }

        public async Task<IList<BarArticleP>> BarArticleImage()
        {
            return await _articlePContetxt.BarArticles
                .Where(x => x.ImgGeneral != null)
                .Where(a => a.BarCategoryId != 10)
                .OrderBy(x => x.DateAdded)
                .ToListAsync();
        }
        public IEnumerable<ImageAll> ImagesAll()
        {
            var AllImageAll = new List<ImageAll>();
            
            var imagesA= ArticleImage();
            var imagesB = BarArticleImage();
            foreach (var item in imagesA.Result)
            {
               
                AllImageAll.Add(
                    new ImageAll { image = item.ImgGeneral, ArticleId = item.ArticleId ,category="Articles",action= "SinglePage",Title=item.Title });

            }
            foreach (var item in imagesB.Result)
            {
                
                AllImageAll.Add(
                    new ImageAll { image = item.ImgGeneral, ArticleId = item.BarArticleId,category="BarArticleP" ,action= "SinglePageBarArticleP" ,Title=item.Title});

            }

            return AllImageAll;
        }
        
    }
}
