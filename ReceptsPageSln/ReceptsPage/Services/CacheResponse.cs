using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ReceptsPage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Services
{
    public class CacheResponse
    {
        private ArticlePContetxt _content;
        private IMemoryCache _memoryCache;

        public CacheResponse(ArticlePContetxt content, IMemoryCache memoryCache)
        {
            _content = content;
            _memoryCache = memoryCache;
        }

        public async Task<IList<ArticleP>> GetArticlesFromCache(int id)
        {

            IList<ArticleP> articles = null;
            if (!_memoryCache.TryGetValue(id, out articles))
            {
                articles =await _content.Articles.OrderByDescending(x => x.DateAdded.Value).Include(x => x.SubCategory).Include(x => x.AppUser).Where(x => x.AdminConfirm == true).ToListAsync();
                _memoryCache.Set(id, articles, TimeSpan.FromDays(1));
                
            }
          
            return articles;

        }
    }
}
