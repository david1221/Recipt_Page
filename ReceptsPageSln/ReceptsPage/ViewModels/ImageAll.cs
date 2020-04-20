using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ViewModels
{
    public class ImageAll
    {
      public  byte[] image { get; set; }
        public int ArticleId { get; set; }
        public string category { get; set; }
        public string action { get; set; }
        public string Title { get; set; }
    }
}
