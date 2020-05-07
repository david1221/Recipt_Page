using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ViewModels
{
    public class VideoArticleViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IFormFile image { get; set; }
        public string YoutubeFrame { get; set; }

    }
}
